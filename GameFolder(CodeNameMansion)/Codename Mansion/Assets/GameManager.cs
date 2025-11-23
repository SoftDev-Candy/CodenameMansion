using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject EnemyPrefab;
    public Camera mainCamera;
    public TextPrompt DialogueParent;
    public bool isItemMenuOn;
    private bool isPaused = false;
    [SerializeField] private Transform respawnPoint;
    public bool IsPaused => isPaused; // Expose the private isPaused variable
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        isItemMenuOn = false;
    }

    void Start()
    {
    }

    public void SpawnEnemy(GameObject parent)
    {
        GameObject enemy = Instantiate(EnemyPrefab, parent.transform.position, Quaternion.identity, parent.transform);
        if(respawnPoint.position != Vector3.zero)
        {
            enemy.GetComponent<Enemy_Movement>().startVec3 = respawnPoint.position;
        }
    }

    public void ShowDialogue(string Dialogue)
    {
        DialogueParent.ShowDialogBox(Dialogue);
    }

}
