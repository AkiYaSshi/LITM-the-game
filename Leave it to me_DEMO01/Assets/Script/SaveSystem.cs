using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;
using System.Collections.Generic;

public class SaveSystem
{
    private List<SavGameObject> objects = new();
    private List<SavRoom> rooms = new();
    private string savePath = "/saves/user.json";

    public void AddObject(SavGameObject obj) => objects.Add(obj);
    public void AddRoom(SavRoom room) => rooms.Add(room);

    /// <summary>
    /// 在空的save data內丟入物件並存檔
    /// </summary>
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        foreach (var obj in objects)
            saveData.AddMemento(obj.Create());
        foreach (var room in rooms)
            saveData.AddMemento(room.Create());

        saveData.SaveToFile(Application.persistentDataPath +  savePath);
    }

    public void LoadGame()
    {
        SaveData saveData = SaveData.LoadFromFile(Application.persistentDataPath + savePath);
        foreach (var obj in objects)
        {
            Memento memento = saveData.GetMemento(obj.ObjectId, "Object");
            if(memento != null) obj.Restore(memento);
        }
        foreach (var room in rooms)
        {
            Memento memento = saveData.GetMemento(room.ObjectId, "Room");
            if(memento !=null) room.Restore(memento);
        }
    }
}
