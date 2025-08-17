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
    public static event Action<TasksSave> InitTasks;

    private const string PARENTNAME = "Insantiate_geo";     // �t�a���W�A����H�J���a�I

    /// <summary>
    /// ����u�O�ЫʦL�N�v�I�N�������L��ɪŤ��O�A�ë����I
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
    /// �ѩ�u�ɪŤ���v�I����I�Ϊ��O�СA�N��������{��{�@�I
    /// </summary>
    public static void LoadGame()
    {

        if (SaveFileManager.SaveSlot < 1 || SaveFileManager.SaveSlot > 3)
        {
            Debug.LogWarning("�s�ɸ��|�L�ĩάO��ܷs���C��");
            return;
        }

        //�]�w��e�ɮ׸��|
        SaveFileManager.SetSavePath();

        //���J�C������
        List<ObjectSave> saves = LoadAny<List<ObjectSave>>(SaveFileManager.objectPath);

        //���J���a�W��
        PlayerSave player = LoadAny<PlayerSave>(SaveFileManager.playerPath);

        //���J�ж�
        RoomSave room = LoadAny<RoomSave>(SaveFileManager.roomPath);

        //���J���ȧ������A
        TasksSave tasks = LoadAny<TasksSave>(SaveFileManager.tasksPath);


        //�ͦ����W����B�T�w��V
        InitTasks?.Invoke(tasks);
        InitRoom?.Invoke(room);
        InitObj?.Invoke(saves);
    }
    /// <summary>
    /// �q���w���|���J���N�������s�ɸ��
    /// </summary>
    /// <typeparam name="T">�n���J����������A�����O�ޥ�����</typeparam>
    /// <param name="savePath">�s�ɸ��|</param>
    /// <returns>���J����ơA�Y�L�s�ɫh��^ null</returns>
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
            Debug.LogError($"���|�W�S���ɮסG{savePath}");
            return null;
        }
    }

    /// <summary>
    /// �b���w���|�x�s���N�������s�ɸ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="save">�s�ɸ������</param>
    /// <param name="savePath">�s�ɸ��|</param>
    public static void SaveAny<T>(T save, string savePath) 
    where T : class
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(PersistentPath() +  savePath, FileMode.Create);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    /// <summary>
    /// ���o�Ҧ��ɮ׸��|���@�P�e�Y
    /// </summary>
    /// <returns></returns>
    private static string PersistentPath()
    {
        return UnityEngine.Application.persistentDataPath;
    }
}