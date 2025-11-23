using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;

    public Texture2D mouseCursor;

    public GameObject LetterAndControls;

 /*   public void Start()
    {
        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    */
    public void PlayGame()
    {
        LetterAndControls.SetActive(true);
        gameObject.SetActive(false);

    }
    public void OpenOptions()
    {
        Debug.Log("Options button clicked. Opening options menu...");
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

   

    public void ExitGame()
    {
        Debug.Log("Exit button clicked. Quitting application...");
        Application.Quit();
    }

   
}
