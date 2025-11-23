using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton pattern for easy access
    public GameObject pauseMenu; // Assign this in the Inspector
  [SerializeField] private bool isPaused = false;
 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                GameManager.instance.isItemMenuOn = false;
            }
            else
            {
                PauseGame();
                GameManager.instance.isItemMenuOn = true;
            }
        }
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
            GameManager.instance.isItemMenuOn = false;
        }
        else
        {
            PauseGame();
            GameManager.instance.isItemMenuOn = true;
        }
    }

    public void PauseGame()
    {
        Debug.Log("Calling Pause Game...");

        StateMachine.Singleton.SwitchState<PausedState>(); // Switch to PausedState
       isPaused = true;
    }

    public void ResumeGame()
    {
        StateMachine.Singleton.SwitchState<PlayingState>(); // Switch back to PlayingState
        isPaused = false;
    }

    // **Return to Main Menu**
    public void LoadMainMenu()
    {
        Debug.Log("Opening Main Menu...");
        Time.timeScale = 1f; // Ensure time is reset
        SceneManager.LoadScene("MainMenu"); // Change "MainMenu" to your actual scene name
    }

    // **Quit Game**
    public void QuitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
