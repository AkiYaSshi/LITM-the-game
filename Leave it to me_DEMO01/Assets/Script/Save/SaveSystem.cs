using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// 負責遊戲的存檔和讀檔行為，提供存儲和加載場景物件狀態的功能。
/// </summary>
public static class SaveSystem
{
    private static string savePath = "/saves/user.design";  // 存檔檔案路徑，位於 persistentDataPath 下
    private const string PARENTNAME = "Insantiate_geo";     // 父物件名稱，用於尋找場景中的物件

    /// <summary>
    /// 將當前遊戲狀態保存到檔案。
    /// </summary>
    public static void SaveGame()
    {
        SaveObject(Objects.GetAllObjectSave());
        Debug.Log($"已將檔案儲存至：{Application.persistentDataPath + savePath}");
    }

    /// <summary>
    /// 從檔案讀取存檔資料，並將狀態套用到遊戲場景內的物件。
    /// </summary>
    public static void LoadGame()
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(Application.persistentDataPath + savePath, FileMode.Open);

        List<ObjectSave> saves = formatter.Deserialize(stream) as List<ObjectSave>;
        stream.Close();
        foreach (ObjectSave save in saves)
        {
            Debug.Log($"物件ID為: {save.objectId} \n" +
                $"物件位置 x: {save.position[0]}, y: {save.position[1]}, z: {save.position[2]}\n" +
                $"物件旋轉 x: {save.rotation[0]}, y: {save.rotation[1]}, z: {save.rotation[2]}, w: {save.rotation[3]}\n" +
                $"= = = = = = = = = = = = = = = = = = = = = = = = = =");
        }
    }

    /// <summary>
    /// 將物件狀態列表序列化並保存到檔案。
    /// </summary>
    /// <param name="obj">包含所有物件狀態的列表</param>
    public static void SaveObject(List<ObjectSave> obj)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(Application.persistentDataPath + savePath, FileMode.Create);

        List<ObjectSave> save = new(obj);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    /// <summary>
    /// 從檔案讀取單一物件的存檔資料。
    /// </summary>
    /// <returns>讀取的 ObjectSave 物件，若無存檔則返回 null</returns>
    public static ObjectSave LoadObject()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(Application.persistentDataPath + savePath, FileMode.Open);

            ObjectSave save = formatter.Deserialize(stream) as ObjectSave;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError($"沒有任何存檔：{savePath}");
            return null;
        }
    }
}