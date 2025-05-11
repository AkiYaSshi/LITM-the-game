using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;

public static class SaveSystem
{

    /// <summary>
    /// �x�s����W�١B�ж���T
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="room"></param>
    public static void SavePlayer(PlayerData playerData, gameplay_RoomShift room)
    {
        NewFolder();

        BinaryFormatter formatter = new BinaryFormatter(); //�s�ؤ@�����ɤu��

        string path = Application.persistentDataPath + "/saves/player.sleep"; //�ɮ��x�s��m
        FileStream stream = new FileStream(path, FileMode.Create); //�s���ɮ׳B�z����A�bpath�W�s���ɮ�

        SaveData data = new SaveData(playerData, room); //�s��Savedata����Aarg��J�n�s�����

        formatter.Serialize(stream, data); //�s���]�k
        stream.Close();

        Debug.Log("SaveSystem�s�ɸ��|���աG" + path);
    }

    public static SaveData LoadSaveData()
    {
        string path = Application.persistentDataPath + "/saves/player.sleep"; //�ɮ��x�s��m
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter(); //�s�ؤ@�����ɤu��
            FileStream stream = new FileStream(path, FileMode.Open); //�s���ɮ׳B�z����A�bpath�W�}���ɮ�

            SaveData data = formatter.Deserialize(stream) as SaveData; //�ϥ����ɤu��A�NBinary�ରSaveData�ɡA�s�JSaveData�ܼ�
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("�ɮ��x�s�G" + path + "���S������s��");
            return null;
        }
    }

    /// <summary>
    /// �ˬd���Ҥ��O�_�s�bsaves��Ƨ��A�Y�L�A�Ыؤ@��
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
