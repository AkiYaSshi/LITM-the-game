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
        Debug.LogError($"此檔案尚未被儲存: {path}");
        return string.Empty;
    }
}
