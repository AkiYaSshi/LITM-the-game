using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    [SerializeField]
    private List<OnClickAnimation> AnimationList;
    private Button btn;
    /// <summary>
    /// 需隱藏的面板是否顯示
    /// </summary>
    private bool IsHiddenShow;

    void Start()
    {
        btn = button.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);

        IsHiddenShow = target.GetComponent<Canvas>().enabled;

    }
    
    /// <summary>
    /// 按鈕點擊後，顯示面板並隱藏List內物件
    /// </summary>
    void OnButtonClick()
    {
        target.GetComponent<Canvas>().enabled = !target.GetComponent<Canvas>().enabled;

        if (hideWhileClick?.Length > 0) //確認隱藏清單內有物件
        {
            foreach (GameObject obj in hideWhileClick)
            {
                obj.SetActive(false);
            }

            if (IsHiddenShow == false)//代表此按鈕是展開面板狀態，再按一次按鈕回到初始狀態
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
    /// 面板隱藏後顯示List內物件
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
