using System;
using UnityEngine;

public class DisableCanvasOnClick : MonoBehaviour
{
    [SerializeField] private Canvas targetCanvas; // 指定要關閉的 Canvas
    [SerializeField]
    [Header("代號")]
    [Tooltip("隱藏清單內物件時，該目標的代號")]
    private string CallingText;

    /// <summary>
    /// 於特定面板隱藏時啟動的事件
    /// </summary>
    public static event Action<string> DCC_click;

    void Update()
    {
        if (targetCanvas.enabled)
        {
        // 偵測滑鼠左鍵或觸控點擊
        if (Input.GetMouseButtonDown(0))
        {
            if (targetCanvas != null)
            {
                targetCanvas.enabled = false;
                DCC_click?.Invoke(CallingText);
            }
            else
            {
                Debug.LogWarning("Target Canvas is not assigned!");
            }
        }
        }
    }
}
