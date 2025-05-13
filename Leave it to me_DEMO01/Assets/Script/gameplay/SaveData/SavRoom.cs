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
    /// 在備忘錄這個大類別下創建專為Room儲存資料用的子類
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
