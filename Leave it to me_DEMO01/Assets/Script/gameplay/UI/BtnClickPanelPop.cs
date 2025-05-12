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
    [SerializeField] GameObject target;
    [SerializeField] GameObject button;
    [SerializeField] GameObject[] hideWhileClick;
    [SerializeField] bool alwaysShowTarget = false;
    [SerializeField]
    private List<OnClickAnimation> AnimationList;
    private Button btn;

    [SerializeField]
    private InputAction pressButton;

    /// <summary>
    /// �����ê����O�O�_���
    /// </summary>
    private bool IsHiddenShow = true;

    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);

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
