using UnityEngine;

/// <summary>
/// �]�w���a��ƪ���
/// </summary>
public static class PlayerData
{
    public static string Name { get; private set; }

    public static void SetName(string name)
    {
        Name = name;
    }
}
