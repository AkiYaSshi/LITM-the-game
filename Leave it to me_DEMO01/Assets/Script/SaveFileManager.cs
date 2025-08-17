using UnityEngine;

/// <summary>
/// �]�w�s�ɽs���Φs�ɸ��|
/// </summary>
public static class SaveFileManager
{
    /// <summary>
    /// ��ܪ��s�ɽs��
    /// </summary>
    public static int SaveSlot { get; private set;  } = -1;
    /// <summary>
    /// ��ܦs�ɽs���A���O�|���ǳƦs��
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
        Debug.Log($"�w��ܦs�ɽs���G{SaveSlot}");
    }

    public static void SetPrepareSlot(int slot)
    {
        if (slot < 1 || slot > 3)
        {
            Debug.LogError($"�L�Ī��s�ɽs���G{slot}�A�п�� 1 �� 3");
            return;
        }
        PrepareSlot = slot;
        Debug.Log($"�w�w�Ʀs�ɽs���G{PrepareSlot}");
    }

    public static void PrepareToFormal()
    {
        SaveSlot = PrepareSlot;
    }

    /// <summary>
    /// �]�w�U���ɮת��s�ɸ��|�쥿�T���s�ɽs��
    /// </summary>
    public static void SetSavePath()
    {
        if (SaveSlot < 1 || SaveSlot > 3)
        {
            Debug.LogError($"�L�Ī��s�ɽs���G{SaveSlot}�A�п�� 1 �� 3");
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
