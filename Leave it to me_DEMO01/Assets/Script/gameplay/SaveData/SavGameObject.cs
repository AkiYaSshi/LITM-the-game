using UnityEditor.Rendering;
using UnityEngine;


/// <summary>
/// �ЫػP�٭�Object���Ƨѿ�
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
    /// �b�Ƨѿ��o�Ӥj���O�U�ЫرM��Object�x�s��ƥΪ��l��
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
/// �x�s���󪬺A
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

