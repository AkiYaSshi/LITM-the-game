using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// UnityUI ���U�������X���O<br /><br />
/// <c>target</c>�G��ܪ��ϼh����<br />
/// <c>button</c>�G���s�]�ݦs�bButton Component�^<br />
/// <c>hideWhileClic</c>k�G���U�����ê���]��^
/// </summary>
public class BtnClickPanelPop : MonoBehaviour
{
    #region �ܼƫŧi
    [Header("���O�P���s�]�m")]
    [Tooltip("��ܪ��ؼЭ��O����]�ݦ� Canvas �ե�^")]
    [SerializeField]
    private GameObject target;

    [Tooltip("Ĳ�o���O�����s����]�ݦ� Button �ե�^")]
    [SerializeField]
    private GameObject button;

    [Tooltip("�O�_�l����ܥؼЭ��O�A�����I������")]
    [SerializeField]
    private bool alwaysShowTarget = false;

    [Header("���ê���޲z")]
    [Tooltip("�I�����s��n���ê�����}�C")]
    [SerializeField]
    private GameObject[] hideWhileClick;

    [Header("�ʵe����")]
    [Tooltip("Ĳ�o�I������檺�ʵe�C��")]
    [SerializeField]
    private List<OnClickAnimation> AnimationList;

    [Header("��J�]�m")]
    [Tooltip("�Ω�Ĳ�o���O����J�ʧ@�]�Ҧp��L�Τ�����^")]
    [SerializeField]
    private InputAction pressButton;
    #endregion

    // ��L�p���ܼơ]���|�X�{�b Inspector ���^
    private Button btn;
    private bool IsHiddenShow = true;

    void Start()
    {

    }
    
    /// <summary>
    /// ���s�I����A��ܭ��O������List������
    /// </summary>
    void OnButtonClick()
    {
        if (!alwaysShowTarget)
        {
            target.GetComponent<Canvas>().enabled = !target.GetComponent<Canvas>().enabled;
        }

        if (hideWhileClick?.Length > 0) //�T�{���òM�椺������
        {
            SwitchVisible();

        }
        if (AnimationList != null)
        {
            foreach (var onClickAnimation in AnimationList)
            {
                onClickAnimation.AnimationStart();
            }
        }
    }
    private void OnKeyClick(InputAction.CallbackContext context)
    {
        OnButtonClick();
    }

    /// <summary>
    /// ����List������
    /// </summary>
    void SwitchVisible()
    {
        if (hideWhileClick?.Length > 0) 
        { 
            foreach (GameObject obj in hideWhileClick)
            {
                obj?.SetActive(!obj.activeSelf);
            }
            IsHiddenShow = !IsHiddenShow;
        }
    }
    private void OnEnable()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
        pressButton?.Enable();
        pressButton.performed += OnKeyClick;
        HideListAfterButtonClick.ShowHidden += SwitchVisible;
    }


    private void OnDisable()
    {
        pressButton?.Disable();
        pressButton.performed -= OnKeyClick;
        HideListAfterButtonClick.ShowHidden -= SwitchVisible;
        btn.onClick?.RemoveListener(OnButtonClick);
    }
}
