using UnityEngine;

/// <summary>
/// �]�w���a��ƪ���
/// </summary>
public static class PlayerData
{
    public static string Name { get; private set; }
    public static bool needTutorial { get; private set; }

    public static void SetName(string name)
    {
        Name = name;
    }

    public static void SetTutorial(bool tutorial)
    {
        needTutorial = tutorial;
    }

    public static void restore(PlayerSave save)
    {
        Name = save.Name;
        needTutorial = save.tutorial;
    }
}
