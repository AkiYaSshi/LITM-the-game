using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueStream : MonoBehaviour
{
    [Header("簡單版設置")]
    [Tooltip("簡單版對話面板物件，包含頭像和文字顯示區域")]
    [SerializeField] private GameObject simpleDialoguePanel;
    [Tooltip("簡單版NPC頭像顯示區域，若留空則透明度設為0")]
    [SerializeField] private Image simpleNpcIcon;
    [Tooltip("簡單版對話文字顯示區域，文字會自動滾動")]
    [SerializeField] private TMP_Text simpleDialogueText;
    [Tooltip("簡單版對話泡泡圖案，控制顯示與否")]
    [SerializeField] private GameObject simpleBubble;

    [Header("複雜版設置")]
    [Tooltip("複雜版對話面板物件，包含頭像、名稱和文字顯示區域")]
    [SerializeField] private GameObject complexDialoguePanel;
    [Tooltip("複雜版NPC頭像顯示區域，若留空則透明度設為0")]
    [SerializeField] private Image complexNpcIcon;
    [Tooltip("複雜版NPC名稱顯示區域，若留空則不顯示名稱")]
    [SerializeField] private TMP_Text complexNpcName;
    [Tooltip("複雜版對話文字顯示區域，文字會自動滾動")]
    [SerializeField] private TMP_Text complexDialogueText;
    [Tooltip("複雜版對話框的三角形提示，閃爍表示可繼續")]
    [SerializeField] private GameObject arrowIndicator;

    [Header("共用設置")]
    [Tooltip("文字滾動速度，數值越小滾動越快")]
    [SerializeField] private float textSpeed = 0.05f;
    [Tooltip("對話資料列表，包含頭像、名稱和內容")]
    [SerializeField] private List<DialogueData> dialogueList;
    [Tooltip("是否使用複雜版顯示模式，勾選則啟用")]
    [SerializeField] private bool isComplexMode = false;
    [Tooltip("對話結束時觸發的事件")]
    [SerializeField] private UnityEvent onDialogueEnd;

    private int currentIndex = 0;
    private Coroutine typingCoroutine;

    void Awake()
    {
        simpleDialoguePanel.SetActive(false);
        complexDialoguePanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void StartDialogue()
    {
        simpleDialoguePanel.SetActive(!isComplexMode);
        complexDialoguePanel.SetActive(isComplexMode);

        currentIndex = 0;
        ShowNextDialogue();
        StartCoroutine(BlinkArrow());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (typingCoroutine != null)
            {
                Debug.LogWarning("提早結束次對話");

                StopCoroutine(typingCoroutine);

                if (isComplexMode) complexDialogueText.text = dialogueList[currentIndex].dialogueText;
                else simpleDialogueText.text = dialogueList[currentIndex].dialogueText;

                typingCoroutine = null;
            }
            else if (currentIndex < dialogueList.Count - 1)
            {
                currentIndex++;
                ShowNextDialogue();
            }
            else
            {
                Debug.Log("此對話串已結束");
                EndDialogue();
            }
        }
    }

    void ShowNextDialogue()
    {
        if (currentIndex >= dialogueList.Count) 
        {
            Debug.LogError("提供的index超過對話清單");
            return; 
        }

        DialogueData data = dialogueList[currentIndex];
        if (isComplexMode)
        {
            complexNpcIcon.sprite = data.npcIcon;
            complexNpcName.text = data.npcName == "System" ? "" : data.npcName;
            SetIconTransparency(complexNpcIcon, data.npcIcon != null);
            complexDialogueText.color = data.npcName == "System" ? Color.red : Color.black;
            typingCoroutine = StartCoroutine(TypeText(complexDialogueText, data.dialogueText));
            arrowIndicator.SetActive(true);
        }
        else
        {
            simpleNpcIcon.sprite = data.npcIcon;
            SetIconTransparency(simpleNpcIcon, data.npcIcon != null);
            simpleDialogueText.color = data.npcName == "System" ? Color.red : Color.black;
            typingCoroutine = StartCoroutine(TypeText(simpleDialogueText, data.dialogueText));
            simpleBubble.SetActive(true);
        }
    }

    void SetIconTransparency(Image icon, bool hasIcon)
    {
        Color color = icon.color;
        color.a = hasIcon ? 1f : 0f;
        icon.color = color;
    }

    IEnumerator TypeText(TMP_Text textField, string text)
    {
        textField.text = "";
        foreach (char c in text)
        {
            textField.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        typingCoroutine = null;
    }
    IEnumerator BlinkArrow()
    {
        while (complexDialoguePanel.activeSelf)
        {
            arrowIndicator.SetActive(!arrowIndicator.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void EndDialogue()
    {
        if (isComplexMode) complexDialoguePanel.SetActive(false);
        else simpleDialoguePanel.SetActive(false);
        currentIndex = 0;
        onDialogueEnd?.Invoke();
        this.gameObject.SetActive(false);
    }
}