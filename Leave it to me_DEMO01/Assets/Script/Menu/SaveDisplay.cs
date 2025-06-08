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
    private TextMeshProUGUI playerName;
    [Tooltip("�W���s�ɮɶ���r")]
    [SerializeField]
    private TextMeshProUGUI time;

    private void Start()
    {
        bool notEmpty =  SetEnable($"/saves{SaveSlot}/Info.design");

        if (notEmpty)
        {
            playerName.text = GetPlayerName($"/saves{SaveSlot}/Player.design");
            time.text = GetFileTIme($"/saves{SaveSlot}/Info.design");
        }
        else
        {
            playerName.text = string.Empty;
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

    /// <summary>
    /// �qSave Info Loader���o���a�W��
    /// </summary>
    /// <param name="path"></param>
    private string GetPlayerName(string path)
    {
        path = UnityEngine.Application.persistentDataPath + path;

        return SaveInfoLoader.LoadName(path);
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
