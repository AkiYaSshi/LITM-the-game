using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Xml.Linq;

/// <summary>
/// 偉大的「存檔之聖殿」！掌管時空的記憶封印與解放，唯有最強大的勇者能觸碰這無上的力量！
/// </summary>
public static class SaveSystem
{
    public static event Action<List<ObjectSave>> InitObj;
    public static event Action<RoomSave> InitRoom;
    public static event Action<TasksSave> InitTasks;

    private const string PARENTNAME = "Insantiate_geo";     // 聖地之名，眾魂寄宿之地！

    /// <summary>
    /// 執行「記憶封印術」！將眾魂之姿刻印於時空之碑，永恆不滅！
    /// </summary>
    public static void SaveGame()
    {
        SaveFileManager.PrepareToFormal();

        SaveFileManager.SetSavePath();

        Directory.CreateDirectory(SaveFileManager.saveDirectory);

        SaveAny(new TimeAndSaveSave(GameObject.Find("Time and Save Data").GetComponent<TimaAndSaveData>()),
                SaveFileManager.timePath);

        SaveAny(new RoomSave(GameObject.FindGameObjectWithTag("Room").GetComponent<RoomData>()), SaveFileManager.roomPath);

        SaveAny(Objects.GetAllObjectSave(), SaveFileManager.objectPath);

        SaveAny(new PlayerSave(PlayerData.Name, PlayerData.needTutorial), SaveFileManager.playerPath);

        SaveAny(new TasksSave(TasksData.tasksComplete), SaveFileManager.tasksPath);

        Debug.Log($"{PersistentPath() + SaveFileManager.objectPath}");
    }


    /// <summary>
    /// 解放「時空之鎖」！喚醒沉睡的記憶，將眾魂之姿重現於現世！
    /// </summary>
    public static void LoadGame()
    {

        if (SaveFileManager.SaveSlot < 1 || SaveFileManager.SaveSlot > 3)
        {
            Debug.LogWarning("存檔路徑無效或是選擇新的遊戲");
            return;
        }

        //設定當前檔案路徑
        SaveFileManager.SetSavePath();

        //載入遊戲物件
        List<ObjectSave> saves = LoadAny<List<ObjectSave>>(SaveFileManager.objectPath);

        //載入玩家名稱
        PlayerSave player = LoadAny<PlayerSave>(SaveFileManager.playerPath);

        //載入房間
        RoomSave room = LoadAny<RoomSave>(SaveFileManager.roomPath);

        //載入任務完成狀態
        TasksSave tasks = LoadAny<TasksSave>(SaveFileManager.tasksPath);


        //生成場上物件、確定方向
        InitTasks?.Invoke(tasks);
        InitRoom?.Invoke(room);
        InitObj?.Invoke(saves);
    }
    /// <summary>
    /// 從指定路徑載入任意類型的存檔資料
    /// </summary>
    /// <typeparam name="T">要載入的資料類型，必須是引用類型</typeparam>
    /// <param name="savePath">存檔路徑</param>
    /// <returns>載入的資料，若無存檔則返回 null</returns>
    public static T LoadAny<T>(string savePath) where T : class
    {
        if (File.Exists(PersistentPath() + savePath))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(PersistentPath() + savePath, FileMode.Open);

            T save = formatter.Deserialize(stream) as T;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError($"路徑上沒有檔案：{savePath}");
            return null;
        }
    }

    /// <summary>
    /// 在指定路徑儲存任意類型的存檔資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="save">存檔資料類型</param>
    /// <param name="savePath">存檔路徑</param>
    public static void SaveAny<T>(T save, string savePath) 
    where T : class
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(PersistentPath() +  savePath, FileMode.Create);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    /// <summary>
    /// 取得所有檔案路徑的共同前墜
    /// </summary>
    /// <returns></returns>
    private static string PersistentPath()
    {
        return UnityEngine.Application.persistentDataPath;
    }
}