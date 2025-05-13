using UnityEditor.Rendering;
using UnityEngine;


/// <summary>
/// 創建與還原Object的備忘錄
/// </summary>
public class SavGameObject : MonoBehaviour
{
    private string objectId;
    Vector3 position;
    Quaternion rotation;
    ObjectRef objectRef;

    public string ObjectId => objectId;
    public SavGameObject(string objectId, Vector3 position, Quaternion rotation, ObjectRef objectRef)
    {
        this.objectId = objectId;
        this.position = position;
        this.rotation = rotation;
        this.objectRef = objectRef;
    }
    #region Memento originator

    /// <summary>
    /// 在備忘錄這個大類別下創建專為Object儲存資料用的子類
    /// </summary>
    /// <returns></returns>
    public Memento Create()
    {
        return new MemObject(objectId, position, rotation, objectRef);
    }

    public void Restore(Memento mem)
    {
        if (mem is MemObject memObject)
        {
            position = memObject.Pos();
            rotation = memObject.Rot();
            objectRef = memObject.ObjectRef();
        }
    }
    #endregion

}
/// <summary>
/// 儲存物件狀態
/// </summary>
public class MemObject: Memento
{
    public Vector3 objPos {  get; private set; }
    public Quaternion objRot { get; private set; }
    public ObjectRef objRef { get; private set; }

    public MemObject(string objectId, Vector3 objPos, Quaternion objRot, ObjectRef objectRef)
        :base("Object", objectId)
    {
        this.objPos = objPos;
        this.objRot = objRot;
        this.objRef = objectRef;
    }

    public Vector3 Pos()
    {
        return objPos;
    }

    public Quaternion Rot()
    {
        return objRot;
    }
    public ObjectRef ObjectRef()
    {
        return objRef;
    }
}

