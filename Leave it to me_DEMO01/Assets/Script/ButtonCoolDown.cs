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
    /// ����U���s�ɰ�����s�\��@�}�l�C
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
