using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<DialogueStream> streams;

    [Tooltip("對話串的目錄")]
    private int index = 0;
    private void Start()
    {
        streams[index]?.gameObject.SetActive(true);
        streams[index]?.StartDialogue();
        index++;
    }
    /// <summary>
    /// 開始下一個對話串
    /// </summary>
    public void NextDialogue()
    {
        if (index == streams.Count)
        {
            Debug.LogError("沒有更多的對話了");
            return;
        }
        streams[index]?.gameObject.SetActive(true);
        streams[index]?.StartDialogue();
        index++;
    }
}

