using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectDataSO : ScriptableObject
{
    public List<ObjectData> objectsData = new();
}
/// <summary>
/// 傢俱資料<br />
/// Name: 名稱<br />
/// ID: 傢俱專屬ID<br />
/// Size: 傢俱在場景中大小<br />
/// Prefab: 傢俱模型
/// </summary>
[Serializable]
public class ObjectData
{
    [Header("基本資訊")]
    [Tooltip("物件的名稱，用於識別")]
    [field: SerializeField]
    public string Name { get; private set; }

    [Tooltip("物件的唯一識別碼")]
    [field: SerializeField]
    public int ID { get; private set; }

    [Header("尺寸與實體")]
    [Tooltip("物件的大小（使用 Vector3Int 表示）")]
    [field: SerializeField]
    public Vector3Int Size { get; private set; } = Vector3Int.one;

    [Tooltip("物件的預製件，用於實例化")]
    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [Header("顯示設置")]
    [Tooltip("物件的前進軸方向（用於顯示控制）")]
    [field: SerializeField]
    public IsDisplay.FowardAxis FowardAxis { get; private set; }
}