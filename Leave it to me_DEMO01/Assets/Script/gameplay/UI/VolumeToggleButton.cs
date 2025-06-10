using UnityEngine;
using UnityEngine.UI;

public class VolumeToggleButton : MonoBehaviour
{
    [Tooltip("���s����")]
    [SerializeField] private Button toggleButton;

    [Tooltip("�Ϯ� 1 (�w�]�Ϯ�)")]
    [SerializeField] private Sprite sprite1;

    [Tooltip("�Ϯ� 0 (�R���Ϯ�)")]
    [SerializeField] private Sprite sprite0;

    [Tooltip("���s�� Image �ե�")]
    [SerializeField] private Image buttonImage;

    private bool isMuted = false; // �n���O�_�R��

    void Start()
    {
        if (toggleButton == null || buttonImage == null)
        {
            Debug.LogError("�Цb Inspector �����T�j�w Button �M Image �ե�");
            return;
        }

        // �]�m�w�]�ϮשM���q
        buttonImage.sprite = sprite1;
        AudioListener.volume = 1.0f; // �w�]���q�� 1

        // �j�w���s�I���ƥ�
        toggleButton.onClick.AddListener(ToggleVolume);
    }

    void ToggleVolume()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            buttonImage.sprite = sprite0; // ������Ϯ� 0
            AudioListener.volume = 0.0f; // �R��
            Debug.Log("�n���w�R��");
        }
        else
        {
            buttonImage.sprite = sprite1; // �����^�Ϯ� 1
            AudioListener.volume = 1.0f; // ��_���q
            Debug.Log("�n���w��_");
        }
    }
}