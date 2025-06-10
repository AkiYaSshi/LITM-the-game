using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatHistory : MonoBehaviour
{
    [Tooltip("�Ω���ܤ�r������")]
    [SerializeField]
    private TextMeshProUGUI logText;

    private Stack<string> historyLog = new();

    private void Start()
    {
        logText.text = string.Empty;
    }

    void UpdateHistory(string name, string context)
    {
        logText.text = "";

        string history = $"{name}�G{context}\n";
        historyLog.Push(history);

        foreach (var item in historyLog)
        {
            logText.text += item;
        }
    }
    private void OnEnable()
    {
        DialogueStream.AddHistory += UpdateHistory;
    }
    private void OnDisable()
    {
        DialogueStream.AddHistory -= UpdateHistory;
    }
}
