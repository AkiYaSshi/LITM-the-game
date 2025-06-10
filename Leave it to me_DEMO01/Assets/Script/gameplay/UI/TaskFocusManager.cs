using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskFocusManager : MonoBehaviour
{
    [Tooltip("同一組的物件清單")]
    [SerializeField] private List<PanelTarget> targetsList;

    [Tooltip("設定新的焦點按鈕")]
    public static event Action<int> setFocus;

    private float lastFocusTime; // 記錄最後一次呼叫時間
    private const float cooldownTime = 0.4f; // 冷卻時間（秒）

    private void Start()
    {
        foreach (var target in targetsList)
        {
            target.anim = target.button.GetComponent<OnClickAnimation>();
        }
    }

    public void SetFocus(int index)
    {
        // 檢查冷卻時間
        if (Time.time - lastFocusTime < cooldownTime)
            return;

        if (targetsList[index].anim.reverse) return; // focus已處於開啟狀態

        targetsList[index].anim.AnimationStart();
        SetAnimation(index);
        SetDisplayPanel(index);
        setFocus?.Invoke(index);

        lastFocusTime = Time.time; // 更新最後呼叫時間
    }

    private void SetAnimation(int index)
    {
        for (int i = 0; i < targetsList.Count; i++)
        {
            if (i != index)
            {
                if (targetsList[i].anim.reverse)
                {
                    targetsList[i].anim.AnimationStart();
                }
            }
        }
    }

    void SetDisplayPanel(int index)
    {
        for (int i = 0; i < targetsList.Count; i++)
        {
            targetsList[i].panel.GetComponent<Canvas>().enabled = i == index;
        }
    }
}

[System.Serializable]
public class PanelTarget
{
    [Tooltip("按鈕")]
    public GameObject button;

    [Tooltip("面板")]
    public GameObject panel;

    [Tooltip("按鈕點擊時附帶的動畫")]
    public OnClickAnimation anim;
}
