using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveInfoLoader
{

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
