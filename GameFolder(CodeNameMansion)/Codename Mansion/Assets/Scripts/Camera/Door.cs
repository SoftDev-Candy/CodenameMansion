using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;
using FMOD.Studio;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class Door : ItemBase
{
    public Camera currentCamera;
    public Camera nextCamera;
    public Image fadeImage;
    public float fadeDuration = 1f;
    public Transform doorTransform;
    public Transform teleportPoint;
    public float openAngle = 90f;
    public float openCloseSpeed = 2f;
    public float doorStayOpenDuration = 2f;

    private bool isTransitioning = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    
    public bool isLocked = true; 
    public bool isFinalDoor = false; // Determines if this door triggers the win condition
    public GameObject victoryScreen; // UI screen for victory

    private EventInstance openDoorInstance;
    private EventInstance locks;
    private EventInstance closeDoorInstance;

    private void Start()
    {
        itemType = ItemType.INTERACT;
        if (doorTransform != null)
        {
            closedRotation = doorTransform.rotation;
            openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        }
        InitializeAudio();
    }

    public override void Interact()
    {
        if (!isLocked && !isFinalDoor)
        {
            if(!isTransitioning)
            {
                openDoorInstance.start();
                StartCoroutine(HandleInteraction());
            }
            
        }
        else
        {
            Debug.Log("What");
            locks.setParameterByName("HandleLock", 2);
            locks.start();
            GameManager.instance.ShowDialogue("It's locked");
        }
        
    }

    public override void DragAndDrop(ItemData item)
    {
        if (item.Item.ID == (int)droppableItem)
        {
            locks.setParameterByName("HandleLock", 0);
            locks.start();
            Inventory.Singleton.RemoveItem(item);
            isLocked = false; // Unlock the door
            Debug.Log("Door unlocked.");
            GameManager.instance.ShowDialogue("Door Unlocked.");

            if (isFinalDoor)
            {
                StartCoroutine(TriggerVictory());
            }
        }
        else
        {
            GameManager.instance.ShowDialogue("Cant use this here");
        }
    }

    public override void PickUp()
    {
    }

    private void InitializeAudio()
    {
        openDoorInstance = AudioManager.instance.CreateEventInstance(FMODEvents.instance.openDoor);
        closeDoorInstance = AudioManager.instance.CreateEventInstance(FMODEvents.instance.closeDoor);
        locks = AudioManager.instance.CreateEventInstance(FMODEvents.instance.locks);
    }

    private IEnumerator HandleInteraction()
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeToBlack());

        if (doorTransform != null)
        {
            yield return StartCoroutine(OpenCloseDoor(true));
        }

        TeleportPlayer();

        if (currentCamera != null)
        {   DisablingOtherCameras();
            currentCamera.gameObject.SetActive(false);
            GameManager.instance.mainCamera.gameObject.SetActive(false);
        }
        if (nextCamera != null)
        {
            nextCamera.gameObject.SetActive(true);
            GameManager.instance.mainCamera = nextCamera;
        }

        yield return StartCoroutine(FadeFromBlack());

        yield return new WaitForSeconds(doorStayOpenDuration);

        if (doorTransform != null)
        {
            yield return StartCoroutine(OpenCloseDoor(false));
        }

        isTransitioning = false;
    }

    private void TeleportPlayer()
    {
        GameObject player = GameManager.instance.player;
        if (player != null && teleportPoint != null)
        {
            player.GetComponent<NavMeshAgent>().Warp(teleportPoint.position);
        }
        else
        {
            Debug.LogWarning("Player or Teleport Point not set correctly.");
        }
    }

    private IEnumerator TriggerVictory()
    {
        Debug.Log("You Win! Displaying victory screen...");

        yield return StartCoroutine(FadeToBlack());

        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }

        yield return new WaitForSeconds(3f); // Show victory screen for 3 seconds

        SceneManager.LoadScene("CreditsScene"); // Load main menu
    }
        
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color color = fadeImage.color;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color color = fadeImage.color;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
    }

    private IEnumerator OpenCloseDoor(bool open)
    {
        Quaternion targetRotation = open ? openRotation : closedRotation;
        float timeElapsed = 0f;
        Quaternion initialRotation = doorTransform.rotation;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * openCloseSpeed;
            doorTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, timeElapsed);
            yield return null;
        }

        doorTransform.rotation = targetRotation;
    }
    void DisablingOtherCameras()
    {
        Camera[] cameras = FindObjectsByType<Camera>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }
    }
}
