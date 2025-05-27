using UnityEngine;

/// <summary>
/// �]�w�s�ɽs���Φs�ɸ��|
/// </summary>
public static class SaveFileManager
{
    /// <summary>
    /// ��ܪ��s�ɽs��
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
            Debug.LogError($"�L�Ī��s�ɽs���G{slot}�A�п�� 1 �� 3");
            return;
        }
        SelectedSaveSlot = slot;
        Debug.Log($"�w��ܦs�ɽs���G{SelectedSaveSlot}");
    }

    /// <summary>
    /// �]�w�U���ɮת��s�ɸ��|�쥿�T���s�ɽs��
    /// </summary>
    public static void SetSavePath()
    {
        if (SelectedSaveSlot < 1 || SelectedSaveSlot > 3)
        {
            Debug.LogError($"�L�Ī��s�ɽs���G{SelectedSaveSlot}�A�п�� 1 �� 3");
            return;
        }

        saveDirectory = UnityEngine.Application.persistentDataPath + $"/saves{SelectedSaveSlot}";
        objectPath = $"/saves{SelectedSaveSlot}/Objects.design";
        roomPath = $"/saves{SelectedSaveSlot}/Room.design";
        timePath = $"/saves{SelectedSaveSlot}/Info.design";

    }
}
