using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SwapDialogue : MonoBehaviour
{   
    [SerializeField]
    DialogueManager dialogueManager;

    [SerializeField]
    List<string> TeacherName = new();
    [SerializeField]
    List<string> president = new();
    [SerializeField]
    List<string> TeacherLastName = new();

    private void Awake()
    {
        foreach (var fullName in TeacherName)
        {
            if(fullName.Length == 3)
            {
                string lastName = fullName.Substring(1);
                TeacherLastName.Add(lastName);
            }
        }
    }

    private void Update()
    {
        foreach (var name in TeacherName)
        {
            if(PlayerData.Name == name)
            {
                dialogueManager.InvokeSpecialDialogue(0);
                gameObject.SetActive(false);
            }

        }
        foreach(var name in TeacherLastName)
        {
            if (PlayerData.Name == name)
            {
                dialogueManager.InvokeSpecialDialogue(0);
                gameObject.SetActive(false);
            }
        }
        foreach (var name in president)
        {
            if (PlayerData.Name == name)
            {
                dialogueManager.InvokeSpecialDialogue(1);
                gameObject.SetActive(false);
            }
        }
    }
}
