using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UnityUI ���U�������X���O<br /><br />
/// <c>target</c>�G��ܪ��ϼh����<br />
/// <c>button</c>�G���s�]�ݦs�bButton Component�^<br />
/// <c>hideWhileClic</c>k�G���U�����ê���]��^
/// </summary>
public class BtnClickPanelPop : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject button;
    [SerializeField] GameObject[] hideWhileClick;
    [SerializeField]
    private List<OnClickAnimation> AnimationList;
    private Button btn;
    /// <summary>
    /// �����ê����O�O�_���
    /// </summary>
    private bool IsHiddenShow;

    void Start()
    {
        btn = button.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);

        IsHiddenShow = target.GetComponent<Canvas>().enabled;

    }
    
    /// <summary>
    /// ���s�I����A��ܭ��O������List������
    /// </summary>
    void OnButtonClick()
    {
        target.GetComponent<Canvas>().enabled = !target.GetComponent<Canvas>().enabled;

        if (hideWhileClick?.Length > 0) //�T�{���òM�椺������
        {
            foreach (GameObject obj in hideWhileClick)
            {
                obj.SetActive(false);
            }

            if (IsHiddenShow == false)//�N�����s�O�i�}���O���A�A�A���@�����s�^���l���A
            {
                ShowHidden();
            }
        }
        if (AnimationList != null)
        {
            foreach (var onClickAnimation in AnimationList)
            {
                onClickAnimation.AnimationStart();
            }
        }
    }

    /// <summary>
    /// ���O���ë����List������
    /// </summary>
    void ShowHidden()
    {
        if (hideWhileClick?.Length > 0) 
        { 
            foreach (GameObject obj in hideWhileClick)
            {
                obj?.SetActive(true);
            }
        }
    }
    private void OnEnable()
    {
        HideListAfterButtonClick.ShowHidden += ShowHidden;
    }

    private void OnDisable()
    {
        HideListAfterButtonClick.ShowHidden -= ShowHidden;
    }
}
