using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LetterSkipScript : MonoBehaviour
{
    public GameObject[] Images;
    int currentImageIndex;
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        for (int i = 0; i < Images.Length; i++)
        {
            Images[i].SetActive(i == 0);
        }

    }

    void Update()
    {

    }


    public void OnContinueClick()
    {
        if (currentImageIndex + 1 == Images.Length)
        {
            SceneManager.LoadScene("Isak");
            return;
        }

        StartCoroutine(Transtition());

    }

    private IEnumerator Transtition()
    {
        Images[currentImageIndex].SetActive(false);
        yield return StartCoroutine(FadeToBlack());
        currentImageIndex++;
        yield return StartCoroutine(FadeFromBlack());
        Images[currentImageIndex].SetActive(true);
        yield return null;
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


}
