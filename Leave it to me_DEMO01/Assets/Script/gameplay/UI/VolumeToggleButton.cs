using UnityEngine;
using UnityEngine.UI;

public class VolumeToggleButton : MonoBehaviour
{
    [Tooltip("按鈕元件")]
    [SerializeField] private Button toggleButton;

    [Tooltip("圖案 1 (預設圖案)")]
    [SerializeField] private Sprite sprite1;

    [Tooltip("圖案 0 (靜音圖案)")]
    [SerializeField] private Sprite sprite0;

    [Tooltip("按鈕的 Image 組件")]
    [SerializeField] private Image buttonImage;

    private bool isMuted = false; // 聲音是否靜音

    void Start()
    {
        if (toggleButton == null || buttonImage == null)
        {
            Debug.LogError("請在 Inspector 中正確綁定 Button 和 Image 組件");
            return;
        }

        // 設置預設圖案和音量
        buttonImage.sprite = sprite1;
        AudioListener.volume = 1.0f; // 預設音量為 1

        // 綁定按鈕點擊事件
        toggleButton.onClick.AddListener(ToggleVolume);
    }

    void ToggleVolume()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            buttonImage.sprite = sprite0; // 切換到圖案 0
            AudioListener.volume = 0.0f; // 靜音
            Debug.Log("聲音已靜音");
        }
        else
        {
            buttonImage.sprite = sprite1; // 切換回圖案 1
            AudioListener.volume = 1.0f; // 恢復音量
            Debug.Log("聲音已恢復");
        }
    }
}