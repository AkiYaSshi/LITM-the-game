using System;
using UnityEngine;

public class DisableCanvasOnClick : MonoBehaviour
{
    [SerializeField] private Canvas targetCanvas; // ���w�n������ Canvas
    [SerializeField]
    [Header("�N��")]
    [Tooltip("���òM�椺����ɡA�ӥؼЪ��N��")]
    private string CallingText;

    /// <summary>
    /// ��S�w���O���îɱҰʪ��ƥ�
    /// </summary>
    public static event Action<string> DCC_click;

    void Update()
    {
        if (targetCanvas.enabled)
        {
        // �����ƹ������Ĳ���I��
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
