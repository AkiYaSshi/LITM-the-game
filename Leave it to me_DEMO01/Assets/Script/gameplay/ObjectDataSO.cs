using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectDataSO : ScriptableObject
{
    public List<ObjectData> objectsData = new List<ObjectData>();
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
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public int ID { get; private set; }

    [field: SerializeField]
    public Vector3Int Size { get; private set; } = Vector3Int.one;

    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [field: SerializeField]
    public IsDisplay.FowardAxis FowardAxis { get; private set; }
}