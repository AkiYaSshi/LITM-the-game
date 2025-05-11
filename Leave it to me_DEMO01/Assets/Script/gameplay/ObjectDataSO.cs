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
/// �íѸ��<br />
/// Name: �W��<br />
/// ID: �íѱM��ID<br />
/// Size: �íѦb�������j�p<br />
/// Prefab: �íѼҫ�
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