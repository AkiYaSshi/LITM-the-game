using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class menuBtnClick : MonoBehaviour
{
    public GameObject loadBtn;
    public GameObject newBtn;
    public GameObject sumitPlayerName;

    public PlayerData playerData;
    //public gameplay_RoomShift room;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool hasSaveFiles = DetactSaves();
        loadBtn.SetActive(hasSaveFiles);

    }
    public void LoadExistingGame() //載入檔案
    {
        SceneManager_script.ToScene(1);
    }

    private bool DetactSaves() //檢查是否有存檔過
    {
        string path = Application.persistentDataPath + "/saves";
        return Directory.Exists(path) && Directory.EnumerateFiles(path, "player*").Any();
    }

}
