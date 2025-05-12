using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// UnityUI 按下按鍵後跳出面板<br /><br />
/// <c>target</c>：顯示的圖層物件<br />
/// <c>button</c>：按鈕（需存在Button Component）<br />
/// <c>hideWhileClic</c>k：按下後隱藏物件（選）
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
    /// 需隱藏的面板是否顯示
    /// </summary>
    private bool IsHiddenShow = true;

    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);

    }
    
    /// <summary>
    /// 按鈕點擊後，顯示面板並隱藏List內物件
    /// </summary>
    void OnButtonClick()
    {
        if (!alwaysShowTarget)
        {
            target.GetComponent<Canvas>().enabled = !target.GetComponent<Canvas>().enabled;
        }

        if (hideWhileClick?.Length > 0) //確認隱藏清單內有物件
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
    /// 切換List內物件
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
