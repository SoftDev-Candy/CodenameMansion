using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TextPrompt : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    [SerializeField] int TextPromptShowTime, numbOfWordsInOneLine, minNumInNewLine;
    

    public void ShowDialogBox(string Dialogue)
    {
        StopAllCoroutines();
        DialogueText.text = ProcessDialog(Dialogue);
        StartCoroutine(ShowPrompt());
    }

    private IEnumerator ShowPrompt()
    {
        transform.localScale = Vector3.one;
        yield return new WaitForSeconds(TextPromptShowTime);
        transform.localScale = Vector3.zero;
    }

    private string ProcessDialog(string text)
    {
        List<string> words = new List<string>(text.Split(" "));

        if (words.Count >= numbOfWordsInOneLine + minNumInNewLine)
        {
            words.Insert(numbOfWordsInOneLine, "\n");
        }

        return string.Join(" ",words);
    }
}
