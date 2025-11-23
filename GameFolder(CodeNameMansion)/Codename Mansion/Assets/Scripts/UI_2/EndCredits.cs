using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndCredits : MonoBehaviour
{
    public RectTransform creditsContainer;
    public float scrollSpeed = 50f;
    public float endDelay = 5f;
    public string mainMenuSceneName = "MainMenu";

    public float startY;
    public float endY;

    void Start()
    {
        startY = creditsContainer.anchoredPosition.y;
        endY = startY + 2000;
        StartCoroutine(ScrollCredits());
    }

    IEnumerator ScrollCredits()
    {
        Debug.Log(creditsContainer.anchoredPosition.y);
        while (creditsContainer.anchoredPosition.y < endY)
        {
            creditsContainer.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(endDelay);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}