using UnityEngine;

/// <summary>
/// 設定存檔編號及存檔路徑
/// </summary>
public static class SaveFileManager
{
    /// <summary>
    /// 選擇的存檔編號
    /// </summary>
    public static int SaveSlot { get; private set;  } = -1;
    /// <summary>
    /// 選擇存檔編號，但是尚未準備存檔
    /// </summary>
    public static int PrepareSlot { get; private set; } = -1;
    public static string objectPath;
    public static string roomPath;
    public static string timePath;
    public static string playerPath;
    public static string saveDirectory;
    public static string tasksPath;


    public static void SetSaveSlot(int slot)
    {
        SaveSlot = slot;
        Debug.Log($"已選擇存檔編號：{SaveSlot}");
    }

    public static void SetPrepareSlot(int slot)
    {
        if (slot < 1 || slot > 3)
        {
            Debug.LogError($"無效的存檔編號：{slot}，請選擇 1 到 3");
            return;
        }
        PrepareSlot = slot;
        Debug.Log($"已預備存檔編號：{PrepareSlot}");
    }

    public static void PrepareToFormal()
    {
        SaveSlot = PrepareSlot;
    }

    /// <summary>
    /// 設定各個檔案的存檔路徑到正確的存檔編號
    /// </summary>
    public static void SetSavePath()
    {
        if (SaveSlot < 1 || SaveSlot > 3)
        {
            Debug.LogError($"無效的存檔編號：{SaveSlot}，請選擇 1 到 3");
            return;
        }

        saveDirectory = UnityEngine.Application.persistentDataPath + $"/saves{SaveSlot}";
        objectPath = $"/saves{SaveSlot}/Objects.design";
        roomPath = $"/saves{SaveSlot}/Room.design";
        timePath = $"/saves{SaveSlot}/Info.design";
        playerPath = $"/saves{SaveSlot}/Player.design";
        tasksPath = $"/saves{SaveSlot}/Tasks.design";

    }
}
