using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���o���a�b��J�ؤ���r�A�èM�w��ɦs�J��Ƥ�����
/// </summary>
public class NameManager : MonoBehaviour
{
    [Header("�ݭn������")]
    [Tooltip("�T�{�W�r�����s")]
    [SerializeField]
    private Button nameSumitBtn;

    [Tooltip("��J�W�r����")]
    [SerializeField]
    private TMP_InputField nameInput;

    [Tooltip("�w�]�W�r")]
    [SerializeField]
    private TMP_InputField defaultName;

    public static string inGameName { get;private set; }

    private void OnEnable()
    {
        nameSumitBtn.onClick.AddListener(SaveName);
    }

    private void SaveName()
    {
        if(string.IsNullOrWhiteSpace(nameInput.text))
        {
            PlayerData.SetName("�s��p�C");
            Debug.Log($"�]���w�]�W��");
            return;
        }
        inGameName = nameInput.text;
        PlayerData.SetName(nameInput.text);
        Debug.Log($"�W�q�w�]�w�G�i{nameInput.text}�j");
    }
}
