using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// �t�d�C�����s�ɩMŪ�ɦ欰�A���Ѧs�x�M�[���������󪬺A���\��C
/// </summary>
public static class SaveSystem
{
    private static string savePath = "/saves/user.design";  // �s���ɮ׸��|�A��� persistentDataPath �U
    private const string PARENTNAME = "Insantiate_geo";     // ������W�١A�Ω�M�������������

    /// <summary>
    /// �N��e�C�����A�O�s���ɮסC
    /// </summary>
    public static void SaveGame()
    {
        SaveObject(Objects.GetAllObjectSave());
        Debug.Log($"�w�N�ɮ��x�s�ܡG{Application.persistentDataPath + savePath}");
    }

    /// <summary>
    /// �q�ɮ�Ū���s�ɸ�ơA�ñN���A�M�Ψ�C��������������C
    /// </summary>
    public static void LoadGame()
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(Application.persistentDataPath + savePath, FileMode.Open);

        List<ObjectSave> saves = formatter.Deserialize(stream) as List<ObjectSave>;
        stream.Close();
        foreach (ObjectSave save in saves)
        {
            Debug.Log($"����ID��: {save.objectId} \n" +
                $"�����m x: {save.position[0]}, y: {save.position[1]}, z: {save.position[2]}\n" +
                $"������� x: {save.rotation[0]}, y: {save.rotation[1]}, z: {save.rotation[2]}, w: {save.rotation[3]}\n" +
                $"= = = = = = = = = = = = = = = = = = = = = = = = = =");
        }
    }

    /// <summary>
    /// �N���󪬺A�C��ǦC�ƨëO�s���ɮסC
    /// </summary>
    /// <param name="obj">�]�t�Ҧ����󪬺A���C��</param>
    public static void SaveObject(List<ObjectSave> obj)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(Application.persistentDataPath + savePath, FileMode.Create);

        List<ObjectSave> save = new(obj);

        formatter.Serialize(stream, save);
        stream.Close();
    }

    /// <summary>
    /// �q�ɮ�Ū����@���󪺦s�ɸ�ơC
    /// </summary>
    /// <returns>Ū���� ObjectSave ����A�Y�L�s�ɫh��^ null</returns>
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
            Debug.LogError($"�S������s�ɡG{savePath}");
            return null;
        }
    }
}