using UnityEngine;
using UnityEngine.UI;

public class ButtonCoolDown : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] GameObject button;

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
