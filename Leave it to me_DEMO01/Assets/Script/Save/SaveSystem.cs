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

    private static string objectPath = "/saves/Objects.design";  // 時空封印的秘徑，深藏於混沌之中！
    private static string roomPath = "/saves/Room.design";
    private const string PARENTNAME = "Insantiate_geo";     // 聖地之名，眾魂寄宿之地！

    /// <summary>
    /// 執行「記憶封印術」！將眾魂之姿刻印於時空之碑，永恆不滅！
    /// </summary>
    public static void SaveGame()
    {
        Directory.CreateDirectory("/saves");

        SaveObject(Objects.GetAllObjectSave());
        SaveRoom(GameObject.FindGameObjectWithTag("Room").GetComponent<RoomData>());
        Debug.Log($"時空碑已銘刻，記憶封印完成：{UnityEngine.Application.persistentDataPath + objectPath}！\n" +
            $"時空碑已銘刻，記憶封印完成：{UnityEngine.Application.persistentDataPath + roomPath}！");
    }

    /// <summary>
    /// 解放「時空之鎖」！喚醒沉睡的記憶，將眾魂之姿重現於現世！
    /// </summary>
    public static void LoadGame()
    {
        List<ObjectSave> saves = LoadObject();
        foreach (ObjectSave save in saves)
        {
            Debug.Log($"魂之識別碼: {save.objectId} \n" +
                $"時空座標 x: {save.position[0]}, y: {save.position[1]}, z: {save.position[2]}\n" +
                $"旋轉之秘 x: {save.rotation[0]}, y: {save.rotation[1]}, z: {save.rotation[2]}, w: {save.rotation[3]}\n" +
                $"= = = = 魂之復甦 = = = =");
        }

        RoomSave room = LoadRoom();

        InitRoom?.Invoke(room);

        //生成場上物件、確定方向
        InitObj?.Invoke(saves);
    }
    #region S/L Object
    /// <summary>
    /// 施展「魂之封存術」！將眾魂之力封入時空之碑，等待命運的召喚！
    /// </summary>
    /// <param name="obj">魂之集合，承載眾生的記憶碎片</param>
    public static void SaveObject(List<ObjectSave> obj)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(UnityEngine.Application.persistentDataPath + objectPath, FileMode.Create);

        List<ObjectSave> save = new(obj);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    /// <summary>
    /// 喚醒「單魂之記憶」！從時空之碑中提取一縷魂之力，揭開其真相！
    /// </summary>
    /// <returns>單魂之記憶，若時空未刻印則返回虛無</returns>
    public static List<ObjectSave> LoadObject()
    {
        if (File.Exists(UnityEngine.Application.persistentDataPath + objectPath))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(UnityEngine.Application.persistentDataPath + objectPath, FileMode.Open);

            List<ObjectSave> save = formatter.Deserialize(stream) as List<ObjectSave>;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError($"時空碑未刻印，魂之記憶無從喚醒：{objectPath}！");
            return null;
        }
    }
    #endregion
    #region S/L Room
    public static void SaveRoom(RoomData room)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(UnityEngine.Application.persistentDataPath + roomPath, FileMode.Create);

        RoomSave save = new(room);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    public static RoomSave LoadRoom()
    {
        if (File.Exists(UnityEngine.Application.persistentDataPath + roomPath))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(UnityEngine.Application.persistentDataPath + roomPath, FileMode.Open);

            RoomSave save = formatter.Deserialize(stream) as RoomSave;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError($"時空碑未刻印，魂之記憶無從喚醒：{roomPath}！");
            return null;
        }
    }
    #endregion
    //如果要新增更多存檔類型請新增更多函式，最後連回Save Game跟Load Game中
}