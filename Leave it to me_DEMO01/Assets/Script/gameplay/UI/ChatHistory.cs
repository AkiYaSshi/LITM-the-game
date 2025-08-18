using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatHistory : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private ScrollRect scrollRect;


    void UpdateHistory(string name, string context)
    {
        GameObject newLog = Instantiate(messagePrefab, content);
        TextMeshProUGUI text = newLog.GetComponent<TextMeshProUGUI>();

        text.text = $"{name}¡G{context}";

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;

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
