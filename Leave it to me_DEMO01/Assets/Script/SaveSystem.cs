using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// 存檔、讀檔行為
/// </summary>
public class SaveSystem : MonoBehaviour
{
    private List<SavRoom> rooms = new();
    private ObjectInitialize objectInit = new();

    private const string PARENTNAME = "Insantiate_geo";

    private string savePath = "/saves/user.json";
    public void AddRoom(SavRoom room) => rooms.Add(room);

    /// <summary>
    /// 在空的save data內丟入物件並存檔
    /// </summary>
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        foreach (var room in rooms)
            saveData.AddMemento(room.Create());

        //取得放置所有生成物件的父物件
        GameObject allInsantiate = GameObject.Find(PARENTNAME);
        //父物件下所有子物件呼叫儲存資料
        foreach(Transform child in allInsantiate.transform)
        {
            SavGameObject savGameObject = child.GetComponent<SavGameObject>();
            saveData.AddMemento(savGameObject.Create());
        }

        saveData.SaveToFile(Application.persistentDataPath +  savePath);
        Debug.Log($"已將檔案儲存至：{Application.persistentDataPath + savePath}");
    }

    /// <summary>
    /// 從檔案內讀取Save Data，將資料套用到遊戲場景內
    /// </summary>
    public void LoadGame()
    {
        SaveData saveData = SaveData.LoadFromFile(Application.persistentDataPath + savePath);

        //還原房間旋轉狀態
        foreach (var room in rooms)
        {
            Memento memento = saveData.GetMemento(room.ObjectId, "Room");
            if(memento !=null) room.Restore(memento);
        }

        if (saveData == null) return;

        
        foreach(var mementoData in saveData.MementoList)
        {
            if(mementoData.Type == "Object")
            {
                //根據存檔重新生成物件
                objectInit.CreateObject<SavGameObject>("Object");

                //還原物件狀態：物件id、位置、旋轉等等
                Memento memento = saveData.GetMemento(mementoData.ObjectId, "Object");
                if(memento != null)
                {
                    object obj = objectInit.GetObject(memento.ObjectId);
                    if ((obj is SavGameObject savGame))
                        savGame.Restore(memento);
                }
            }
        }


    }
}
