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
/// �íѸ��<br />
/// Name: �W��<br />
/// ID: �íѱM��ID<br />
/// Size: �íѦb�������j�p<br />
/// Prefab: �íѼҫ�
/// </summary>
[Serializable]
public class ObjectData
{
    [Header("�򥻸�T")]
    [Tooltip("���󪺦W�١A�Ω��ѧO")]
    [field: SerializeField]
    public string Name { get; private set; }

    [Tooltip("���󪺰ߤ@�ѧO�X")]
    [field: SerializeField]
    public int ID { get; private set; }

    [Header("�ؤo�P����")]
    [Tooltip("���󪺤j�p�]�ϥ� Vector3Int ��ܡ^")]
    [field: SerializeField]
    public Vector3Int Size { get; private set; } = Vector3Int.one;

    [Tooltip("���󪺹w�s��A�Ω��Ҥ�")]
    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [Header("��ܳ]�m")]
    [Tooltip("���󪺫e�i�b��V�]�Ω���ܱ���^")]
    [field: SerializeField]
    public IsDisplay.FowardAxis FowardAxis { get; private set; }
}