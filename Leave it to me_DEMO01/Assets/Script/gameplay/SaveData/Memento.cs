using UnityEngine;

/// <summary>
/// 備忘錄的抽象類
/// </summary>
[System.Serializable]
public abstract class Memento
{
    public string Type { get; protected set; } // 識別 Memento 類型
    public string ObjectId { get; protected set; } // 物件唯一識別碼

    protected Memento(string type, string objectId )
    {
        Type = type;
        ObjectId = objectId;
    }
}
