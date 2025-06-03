using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewBehavior : MonoBehaviour
{
    [Tooltip("�@�}�l�Y��ܪ�����")]
    [SerializeField]
    private GameObject originPanel;

    [Tooltip("����X�{������")]
    [SerializeField]
    private GameObject newPanel;

    [Tooltip("�ؼЫ��s")]
    [SerializeField]
    private Button Button;

    [Tooltip("���U��K�^���l���A������")]
    [SerializeField]
    private List<Button> buttons;

    private void Start()
    {
        UndoDisplay();
    }

    private void SwitchDisplay()
    {
        originPanel.SetActive(false);
        newPanel.SetActive(true);
        Button.enabled = false;
    }

    public void UndoDisplay()
    {
        originPanel.SetActive(true);
        newPanel.SetActive(false);
        Button.enabled = true;
    }
    private void OnEnable()
    {
        Button.onClick.AddListener(SwitchDisplay);
        foreach (var button in buttons)
        {
            button.onClick.AddListener(UndoDisplay);
        }
    }
    private void OnDisable()
    {
        Button.onClick.RemoveListener(SwitchDisplay);
        foreach (var button in buttons)
        {
            button.onClick.RemoveListener(UndoDisplay);
        }
    }
}
