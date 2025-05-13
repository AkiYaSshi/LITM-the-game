using System;
using UnityEngine;


public class SavRoom : MonoBehaviour
{
    Quaternion rotation;
    string objectId;

    public string ObjectId => objectId;
    public SavRoom(Quaternion rotation, string objectId)
    {
        this.rotation = rotation;
        this.objectId = objectId;
    }


    #region memento

    /// <summary>
    /// �b�Ƨѿ��o�Ӥj���O�U�ЫرM��Room�x�s��ƥΪ��l��
    /// </summary>
    /// <returns></returns>
    public Memento Create()
    {
        return new MemRoom(objectId, rotation);
    }
    public void Restore(Memento mem)
    {
        if (mem is MemRoom memroom)
        {
            rotation = memroom.Rot();
        }
    }
    #endregion
}
public class MemRoom:Memento
{
    private Quaternion objRot;

    public MemRoom(string objectId, Quaternion objRot):base("Room", objectId)
    {
        this.objRot = objRot;
    }

    public Quaternion Rot()
    {
        return objRot;
    }
}
