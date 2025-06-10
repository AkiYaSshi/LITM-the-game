using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 取得玩家在輸入框內文字，並決定何時存入資料中的類
/// </summary>
public class NameManager : MonoBehaviour
{
    [Header("需要的物件")]
    [Tooltip("確認名字的按鈕")]
    [SerializeField]
    private Button nameSumitBtn;

    [Tooltip("輸入名字的框")]
    [SerializeField]
    private TMP_InputField nameInput;

    [Tooltip("預設名字")]
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
            PlayerData.SetName("新手小七");
            Debug.Log($"設為預設名稱");
            return;
        }
        inGameName = nameInput.text;
        PlayerData.SetName(nameInput.text);
        Debug.Log($"名義已設定：【{nameInput.text}】");
    }
}
