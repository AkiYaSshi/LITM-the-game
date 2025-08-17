using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// ���ѿ�ܦs��UI�W���s�ɸ�T
/// </summary>
public static class SaveInfoLoader
{
    /// <summary>
    /// �����x�s�ɶ�
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string LoadTime(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new(path, FileMode.Open);

            TimeAndSaveSave save = formatter.Deserialize(stream) as TimeAndSaveSave;
            stream.Close();
            return save.timeStamp;
        }
        Debug.LogError($"���ɮש|���Q�x�s: {path}");
        return string.Empty;
    }

    public static string LoadName(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new(path, FileMode.Open);

            PlayerSave save = formatter.Deserialize(stream) as PlayerSave;
            stream.Close();
            return save.Name;
        }
        Debug.LogError($"���ɮש|���Q�x�s: {path}");
        return string.Empty;
    }

    public static bool LoadTutorial(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new(path, FileMode.Open);

            PlayerSave save = formatter.Deserialize(stream) as PlayerSave;
            stream.Close();

            PlayerData.restore(save);

            return save.tutorial;
        }
        Debug.LogError($"���ɮש|���Q�x�s: {path}");
        return true;
    }

    /// <summary>
    /// �ˬd���w���|�O�_���s��
    /// </summary>
    /// <param name="pathnumber"></param>
    /// <returns></returns>
    public static bool IsSaveExisting(int pathnumber)
    {
        string path = Path.Combine(UnityEngine.Application.persistentDataPath, $"saves{pathnumber}");

        return Directory.Exists(path);
    }
}
