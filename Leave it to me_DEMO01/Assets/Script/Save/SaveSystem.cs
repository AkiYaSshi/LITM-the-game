using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Xml.Linq;

/// <summary>
/// ���j���u�s�ɤ��t���v�I�x�ޮɪŪ��O�ЫʦL�P�ѩ�A�ߦ��̱j�j���i�̯�Ĳ�I�o�L�W���O�q�I
/// </summary>
public static class SaveSystem
{
    public static event Action<List<ObjectSave>> InitObj;
    public static event Action<RoomSave> InitRoom;
    private const string PARENTNAME = "Insantiate_geo";     // �t�a���W�A����H�J���a�I

    /// <summary>
    /// ����u�O�ЫʦL�N�v�I�N�������L��ɪŤ��O�A�ë����I
    /// </summary>
    public static void SaveGame()
    {
        SaveFileManager.SetSavePath();

        Directory.CreateDirectory(SaveFileManager.saveDirectory);

        SaveAny(new TimeAndSaveSave(GameObject.Find("Time and Save Data").GetComponent<TimaAndSaveData>()),
                SaveFileManager.timePath);

        SaveAny(new RoomSave(GameObject.FindGameObjectWithTag("Room").GetComponent<RoomData>()), SaveFileManager.roomPath);

        SaveAny(Objects.GetAllObjectSave(), SaveFileManager.objectPath);

        Debug.Log($"�ɪŸO�w�ʨ�A�O�ЫʦL�����G{PersistentPath() + SaveFileManager.objectPath}�I\n" +
            $"�ɪŸO�w�ʨ�A�O�ЫʦL�����G{PersistentPath() + SaveFileManager.roomPath}�I");
    }


    /// <summary>
    /// �ѩ�u�ɪŤ���v�I����I�Ϊ��O�СA�N��������{��{�@�I
    /// </summary>
    public static void LoadGame()
    {
        //�]�w��e�ɮ׸��|
        SaveFileManager.SetSavePath();

        //���J�C������
        List<ObjectSave> saves = LoadObject();
        foreach (ObjectSave save in saves)
        {
            Debug.Log($"��ѧO�X: {save.objectId} \n" +
                $"�ɪŮy�� x: {save.position[0]}, y: {save.position[1]}, z: {save.position[2]}\n" +
                $"���ध�� x: {save.rotation[0]}, y: {save.rotation[1]}, z: {save.rotation[2]}, w: {save.rotation[3]}\n" +
                $"= = = = ��_�d = = = =");
        }

        //���J�ж�
        RoomSave room = LoadRoom();

        InitRoom?.Invoke(room);

        //�ͦ����W����B�T�w��V
        InitObj?.Invoke(saves);
    }
    #region S/L Object
    /// <summary>
    /// �I�i�u��ʦs�N�v�I�N����O�ʤJ�ɪŤ��O�A���ݩR�B���l��I
    /// </summary>
    /// <param name="obj">����X�A�Ӹ����ͪ��O�иH��</param>
    public static void SaveObject(List<ObjectSave> obj)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(PersistentPath() + SaveFileManager.objectPath, FileMode.Create);

        List<ObjectSave> save = new(obj);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    /// <summary>
    /// ����u���O�Сv�I�q�ɪŤ��O�������@�\��O�A���}��u�ۡI
    /// </summary>
    /// <returns>���O�СA�Y�ɪť���L�h��^��L</returns>
    public static List<ObjectSave> LoadObject()
    {
        if (File.Exists(PersistentPath() + SaveFileManager.objectPath))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(PersistentPath() + SaveFileManager.objectPath, FileMode.Open);

            List<ObjectSave> save = formatter.Deserialize(stream) as List<ObjectSave>;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError($"�ɪŸO����L�A��O�еL�q����G{PersistentPath() + SaveFileManager.objectPath}�I");
            return null;
        }
    }
    #endregion
    #region S/L Room
    public static void SaveRoom(RoomData room)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(PersistentPath() + SaveFileManager.roomPath, FileMode.Create);

        RoomSave save = new(room);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    public static RoomSave LoadRoom()
    {
        if (File.Exists(PersistentPath() + SaveFileManager.roomPath))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(PersistentPath() + SaveFileManager.roomPath, FileMode.Open);

            RoomSave save = formatter.Deserialize(stream) as RoomSave;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError($"�ɪŸO����L�A��O�еL�q����G{SaveFileManager.roomPath}�I");
            return null;
        }
    }
    #endregion
    #region Save
    public static void SaveAny<T>(T save, string savePath) 
    where T : class
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(PersistentPath() +  savePath, FileMode.Create);

        formatter.Serialize(stream, save);
        stream.Close();
    }
    #endregion
    //�p�G�n�s�W��h�s�������зs�W��h�禡�A�̫�s�^Save Game��Load Game��
    /// <summary>
    /// ���o�Ҧ��ɮ׸��|���@�P�e�Y
    /// </summary>
    /// <returns></returns>
    private static string PersistentPath()
    {
        return UnityEngine.Application.persistentDataPath;
    }
}