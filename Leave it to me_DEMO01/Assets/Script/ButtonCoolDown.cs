using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 管理按鈕的冷卻功能，限制按鈕在指定時間內無法再次點擊。
/// </summary>
public class ButtonCoolDown : MonoBehaviour
{
    [Header("按鈕設置")]
    [Tooltip("按鈕冷卻時間（單位：秒），決定按鈕無法點擊的持續時間")]
    [SerializeField] private float time;

    [Tooltip("目標按鈕的 GameObject，需包含 Button 組件")]
    [SerializeField] private GameObject button;

    // 私有變數，不顯示於 Inspector
    private Button btn;

    void Start()
    {
        btn = button.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    /// <summary>
    /// 當按下按鈕時停止按鈕功能一陣子。
    /// </summary>
    void OnButtonClick()
    {
        btn.interactable = false;
        Invoke("EnableButton", time);
    }

    void EnableButton()
    {
        btn.interactable = true;
    }
}