using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;

public static class SaveSystem
{

    /// <summary>
    /// 儲存角色名稱、房間資訊
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="room"></param>
    public static void SavePlayer(PlayerData playerData, gameplay_RoomShift room)
    {
        NewFolder();

        BinaryFormatter formatter = new BinaryFormatter(); //新建一個轉檔工具

        string path = Application.persistentDataPath + "/saves/player.sleep"; //檔案儲存位置
        FileStream stream = new FileStream(path, FileMode.Create); //新建檔案處理物件，在path上新建檔案

        SaveData data = new SaveData(playerData, room); //新建Savedata物件，arg填入要存的資料

        formatter.Serialize(stream, data); //存檔魔法
        stream.Close();

        Debug.Log("SaveSystem存檔路徑測試：" + path);
    }

    public static SaveData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/saves/player.sleep"; //檔案儲存位置
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter(); //新建一個轉檔工具
            FileStream stream = new FileStream(path, FileMode.Open); //新建檔案處理物件，在path上開啟檔案

            SaveData data = formatter.Deserialize(stream) as SaveData; //使用轉檔工具，將Binary轉為SaveData檔，存入SaveData變數
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("檔案儲存：" + path + "內沒有任何存檔");
            return null;
        }
    }

    /// <summary>
    /// 檢查路境內是否存在saves資料夾，若無，創建一個
    /// </summary>
    public static void NewFolder()
    {
        string path = Application.persistentDataPath;

        if (!File.Exists(path))
        {
            string folderPath = Path.Combine(Application.persistentDataPath, "saves");
            Directory.CreateDirectory(folderPath);
        }
    }
}
