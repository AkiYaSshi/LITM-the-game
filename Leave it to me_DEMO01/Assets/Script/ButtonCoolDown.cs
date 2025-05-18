using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �޲z���s���N�o�\��A������s�b���w�ɶ����L�k�A���I���C
/// </summary>
public class ButtonCoolDown : MonoBehaviour
{
    [Header("���s�]�m")]
    [Tooltip("���s�N�o�ɶ��]���G��^�A�M�w���s�L�k�I��������ɶ�")]
    [SerializeField] private float time;

    [Tooltip("�ؼЫ��s�� GameObject�A�ݥ]�t Button �ե�")]
    [SerializeField] private GameObject button;

    // �p���ܼơA����ܩ� Inspector
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