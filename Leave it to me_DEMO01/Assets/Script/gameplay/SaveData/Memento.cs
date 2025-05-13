using UnityEngine;

/// <summary>
/// �Ƨѿ�����H��
/// </summary>
[System.Serializable]
public abstract class Memento
{
    public string Type { get; protected set; } // �ѧO Memento ����
    public string ObjectId { get; protected set; } // ����ߤ@�ѧO�X

    protected Memento(string type, string objectId )
    {
        Type = type;
        ObjectId = objectId;
    }
}
