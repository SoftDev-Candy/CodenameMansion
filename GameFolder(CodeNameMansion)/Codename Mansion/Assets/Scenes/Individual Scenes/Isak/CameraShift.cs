using UnityEngine;

public class CameraShift : MonoBehaviour
{
    private Camera lastCamera;
    [SerializeField] private GameObject shiftToCamera;
    [SerializeField] private GameObject defaultCamera;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lastCamera = GameManager.instance.mainCamera;
            shiftToCamera.SetActive(true);
            lastCamera.gameObject.SetActive(false);
            GameManager.instance.mainCamera = shiftToCamera.GetComponent<Camera>();
            Debug.Log("Player Entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            defaultCamera.SetActive(true);

            GameManager.instance.mainCamera = defaultCamera.GetComponent<Camera>();
            shiftToCamera.SetActive(false);
        }
    }
}
