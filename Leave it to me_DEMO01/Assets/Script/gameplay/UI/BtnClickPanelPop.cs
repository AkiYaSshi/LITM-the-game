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
    #region 變數宣告
    [Header("面板與按鈕設置")]
    [Tooltip("顯示的目標面板物件（需有 Canvas 組件）")]
    [SerializeField]
    private GameObject target;

    [Tooltip("觸發面板的按鈕物件（需有 Button 組件）")]
    [SerializeField]
    private GameObject button;

    [Tooltip("是否始終顯示目標面板，忽略點擊切換")]
    [SerializeField]
    private bool alwaysShowTarget = false;

    [Header("隱藏物件管理")]
    [Tooltip("點擊按鈕後要隱藏的物件陣列")]
    [SerializeField]
    private GameObject[] hideWhileClick;

    [Header("動畫控制")]
    [Tooltip("觸發點擊後執行的動畫列表")]
    [SerializeField]
    private List<OnClickAnimation> AnimationList;

    [Header("輸入設置")]
    [Tooltip("用於觸發面板的輸入動作（例如鍵盤或手把按鍵）")]
    [SerializeField]
    private InputAction pressButton;
    #endregion

    // 其他私有變數（不會出現在 Inspector 中）
    private Button btn;
    private bool IsHiddenShow = true;

    void Start()
    {

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
