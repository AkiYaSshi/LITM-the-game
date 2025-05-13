using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Build.Content;
using UnityEngine;

/// <summary>
/// 存放所有需儲存資料的類別，以及用於使用資料的方法
/// </summary>
[System.Serializable]
public class SaveData
{
    /// <summary>
    /// 不同種memento間通用的類別，用以儲存一個個memento
    /// </summary>
    [System.Serializable]
    public class MementoData
    {
        public string Type;
        public string ObjectId;
        public string Data; //不同Memento序列化之後存這裡
    }

    /// <summary>
    /// 所有被存檔物件/狀態的List
    /// </summary>
    public List<MementoData> MementoList = new();

    /// <summary>
    /// 根據既有的memento，新建一個Memento Data並存進List內
    /// </summary>
    /// <param name="memento">被儲存的memento</param>
    public void AddMemento(Memento memento)
    {
        MementoData data = new MementoData
        {
            Type = memento.Type,
            ObjectId = memento.ObjectId,
            Data = JsonUtility.ToJson(memento)
        };
        MementoList.Add(data);
    }

    /// <summary>
    /// 將這個類別本身序列化後存到指定檔案路徑
    /// </summary>
    /// <param name="path">檔案名稱</param>
    public void SaveToFile(string path)
    {
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path, json);
    }
    /// <summary>
    /// 將路徑讀取到的json反序列化後回傳
    /// </summary>
    /// <param name="path"></param>
    /// <returns>存入的Savedata檔</returns>
    public static SaveData LoadFromFile(string path)
    {
        string json = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public Memento GetMemento(string type, string objectId)
    {
        var data = MementoList.Find(m => m.ObjectId == objectId && m.Type == type);
        if (data == null) return null;

        if(data.Type == "Object")
            return JsonUtility.FromJson<MemObject>(data.Data);
        else if (data.Type == "Room")
            return JsonUtility.FromJson<MemRoom>(data.Data);

        return null;
    }
}
