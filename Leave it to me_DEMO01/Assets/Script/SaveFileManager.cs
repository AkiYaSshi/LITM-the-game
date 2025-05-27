using UnityEngine;

/// <summary>
/// 設定存檔編號及存檔路徑
/// </summary>
public static class SaveFileManager
{
    /// <summary>
    /// 選擇的存檔編號
    /// </summary>
    public static int SelectedSaveSlot { get; private set;  } = -1;
    public static string objectPath;
    public static string roomPath;
    public static string timePath;
    public static string saveDirectory;


    public static void SetSaveSlot(int slot)
    {
        if (slot < 1 || slot > 3)
        {
            Debug.LogError($"無效的存檔編號：{slot}，請選擇 1 到 3");
            return;
        }
        SelectedSaveSlot = slot;
        Debug.Log($"已選擇存檔編號：{SelectedSaveSlot}");
    }

    /// <summary>
    /// 設定各個檔案的存檔路徑到正確的存檔編號
    /// </summary>
    public static void SetSavePath()
    {
        if (SelectedSaveSlot < 1 || SelectedSaveSlot > 3)
        {
            Debug.LogError($"無效的存檔編號：{SelectedSaveSlot}，請選擇 1 到 3");
            return;
        }

        saveDirectory = UnityEngine.Application.persistentDataPath + $"/saves{SelectedSaveSlot}";
        objectPath = $"/saves{SelectedSaveSlot}/Objects.design";
        roomPath = $"/saves{SelectedSaveSlot}/Room.design";
        timePath = $"/saves{SelectedSaveSlot}/Info.design";

    }
}
