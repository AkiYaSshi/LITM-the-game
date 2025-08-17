using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]
    [Header("教學關對話")]
    private List<DialogueStream> dialogue = new();

    [SerializeField]
    private bool playTutorial;

    private void Start()
    {
        playTutorial = true;
        StartCoroutine(StartTutorial());
    }
    public void EndTutorial()
    {
        PlayerData.SetTutorial(false);
    }
    public IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(0.2f);

        playTutorial = SaveInfoLoader.LoadTutorial(Application.persistentDataPath + SaveFileManager.playerPath);

        if (playTutorial)
        {
            dialogue[0].gameObject.SetActive(true);
            dialogue[0].StartDialogue();
        }
    }
}
