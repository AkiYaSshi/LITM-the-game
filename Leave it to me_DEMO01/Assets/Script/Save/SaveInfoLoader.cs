using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 提供選擇存檔UI上的存檔資訊
/// </summary>
public static class SaveInfoLoader
{
    /// <summary>
    /// 提供儲存時間
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
        Debug.LogError($"此檔案尚未被儲存: {path}");
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
        Debug.LogError($"此檔案尚未被儲存: {path}");
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
        Debug.LogError($"此檔案尚未被儲存: {path}");
        return true;
    }

    /// <summary>
    /// 檢查指定路徑是否有存檔
    /// </summary>
    /// <param name="pathnumber"></param>
    /// <returns></returns>
    public static bool IsSaveExisting(int pathnumber)
    {
        string path = Path.Combine(UnityEngine.Application.persistentDataPath, $"saves{pathnumber}");

        return Directory.Exists(path);
    }
}
