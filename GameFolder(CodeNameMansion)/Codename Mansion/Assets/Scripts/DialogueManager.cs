using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Singleton;
    public Dictionary<string, string> dialogueDatabase = new Dictionary<string, string>();

    private void Start()
    {
        Singleton = this;
        GetDialoguesFromFile("itemsTextPrompts");
    }

    void GetDialoguesFromFile(string FileName)
    {
        TextAsset txtFile = Resources.Load<TextAsset>(FileName);
        if (txtFile == null)
        {
            Debug.Log("No CSV found!");
            return;
        }
        Debug.Log(txtFile.text);
        string[] lines = txtFile.text.Split('\n');
        Debug.Log(lines);
        foreach (string line in lines)
        {
            string[] fields = line.Split('|'); 
            string name = fields[0];
            string dialogue = fields[1];

            dialogueDatabase[name] = dialogue;
        }


    }

    public string GetDialogueFromName(string Name)
    {
        return dialogueDatabase[Name];
    }
    
}
