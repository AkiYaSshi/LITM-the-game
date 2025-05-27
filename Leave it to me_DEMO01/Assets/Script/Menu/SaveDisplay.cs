using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveDisplay : MonoBehaviour
{
    [Tooltip("按鈕代表的存檔編號")]
    [SerializeField]
    private int SaveSlot;

    [Header("顯示文字")]
    [Tooltip("關卡階段文字")]
    [SerializeField]
    private TextMeshProUGUI stage;
    [Tooltip("上次存檔時間文字")]
    [SerializeField]
    private TextMeshProUGUI time;

    private void Start()
    {
        bool notEmpty =  SetEnable($"/saves{SaveSlot}/Info.design");

        if (notEmpty)
        {
            stage.text = string.Empty;
            time.text = GetFileTIme($"/saves{SaveSlot}/Info.design");
        }
        else
        {
            stage.text = string.Empty;
            time.text = string.Empty;
        }
    }
    /// <summary>
    /// 從Save Info Loader取得上次存檔時間
    /// </summary>
    /// <param name="path"></param>
    private string GetFileTIme(string path)
    {
        path = UnityEngine.Application.persistentDataPath + path;

        return SaveInfoLoader.LoadTime(path);

    }

    private bool SetEnable(string path)
    {
        path = UnityEngine.Application.persistentDataPath + path;
        if (!File.Exists(path))
        {
            gameObject.GetComponent<Button>().interactable = false;
            return false;
        }
        return true;
    }
}
