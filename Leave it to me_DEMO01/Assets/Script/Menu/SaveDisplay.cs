using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveDisplay : MonoBehaviour
{
    [Tooltip("���s�N���s�ɽs��")]
    [SerializeField]
    private int SaveSlot;

    [Header("��ܤ�r")]
    [Tooltip("���d���q��r")]
    [SerializeField]
    private TextMeshProUGUI stage;
    [Tooltip("�W���s�ɮɶ���r")]
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
    /// �qSave Info Loader���o�W���s�ɮɶ�
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
