using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueStream : MonoBehaviour
{
    [Header("²�檩�]�m")]
    [Tooltip("²�檩��ܭ��O����A�]�t�Y���M��r��ܰϰ�")]
    [SerializeField] private GameObject simpleDialoguePanel;
    [Tooltip("²�檩NPC�Y����ܰϰ�A�Y�d�ūh�z���׳]��0")]
    [SerializeField] private Image simpleNpcIcon;
    [Tooltip("²�檩��ܤ�r��ܰϰ�A��r�|�۰ʺu��")]
    [SerializeField] private TMP_Text simpleDialogueText;
    [Tooltip("²�檩��ܪw�w�ϮסA������ܻP�_")]
    [SerializeField] private GameObject simpleBubble;

    [Header("�������]�m")]
    [Tooltip("��������ܭ��O����A�]�t�Y���B�W�٩M��r��ܰϰ�")]
    [SerializeField] private GameObject complexDialoguePanel;
    [Tooltip("������NPC�Y����ܰϰ�A�Y�d�ūh�z���׳]��0")]
    [SerializeField] private Image complexNpcIcon;
    [Tooltip("������NPC�W����ܰϰ�A�Y�d�ūh����ܦW��")]
    [SerializeField] private TMP_Text complexNpcName;
    [Tooltip("��������ܤ�r��ܰϰ�A��r�|�۰ʺu��")]
    [SerializeField] private TMP_Text complexDialogueText;
    [Tooltip("��������ܮت��T���δ��ܡA�{�{��ܥi�~��")]
    [SerializeField] private GameObject arrowIndicator;

    [Header("�@�γ]�m")]
    [Tooltip("��r�u�ʳt�סA�ƭȶV�p�u�ʶV��")]
    [SerializeField] private float textSpeed = 0.05f;
    [Tooltip("��ܸ�ƦC��A�]�t�Y���B�W�٩M���e")]
    [SerializeField] private List<DialogueData> dialogueList;
    [Tooltip("�O�_�ϥν�������ܼҦ��A�Ŀ�h�ҥ�")]
    [SerializeField] private bool isComplexMode = false;
    [Tooltip("��ܵ�����Ĳ�o���ƥ�")]
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
                Debug.LogWarning("�������������");

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
                Debug.Log("����ܦ�w����");
                EndDialogue();
            }
        }
    }

    void ShowNextDialogue()
    {
        if (currentIndex >= dialogueList.Count) 
        {
            Debug.LogError("���Ѫ�index�W�L��ܲM��");
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