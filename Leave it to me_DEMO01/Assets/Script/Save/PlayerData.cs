using UnityEngine;

/// <summary>
/// 設定玩家資料的類
/// </summary>
public static class PlayerData
{
    public static string Name { get; private set; }

    public static void SetName(string name)
    {
        Name = name;
    }
}
