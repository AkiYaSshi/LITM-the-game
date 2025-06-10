using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<DialogueStream> streams;

    [Tooltip("��ܦꪺ�ؿ�")]
    private int index = 0;
    private void Start()
    {
        streams[index]?.gameObject.SetActive(true);
        streams[index]?.StartDialogue();
        index++;
    }
    /// <summary>
    /// �}�l�U�@�ӹ�ܦ�
    /// </summary>
    public void NextDialogue()
    {
        if (index == streams.Count)
        {
            Debug.LogError("�S����h����ܤF");
            return;
        }
        streams[index]?.gameObject.SetActive(true);
        streams[index]?.StartDialogue();
        index++;
    }
}

