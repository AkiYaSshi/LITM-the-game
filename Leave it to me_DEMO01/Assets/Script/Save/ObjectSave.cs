using UnityEngine;

/// <summary>
/// �x�s���󪺦s�ɸ�ơA�]�t ID�B��m�M�����T�C
/// </summary>
[System.Serializable]
public class ObjectSave
{
    public int objectId;        // ���󪺰ߤ@�ѧO�X
    public float[] position;    // ���󪺦�m�y�� (x, y, z)
    public float[] rotation;    // ���󪺱���|���� (x, y, z, w)

    /// <summary>
    /// �q ObjectRef �c�y ObjectSave ����C
    /// </summary>
    /// <param name="objectRef">�]�t�����ƪ� ObjectRef �ե�</param>
    public ObjectSave(ObjectRef objectRef)
    {
        objectId = objectRef.objectData.ID;

        position = new float[3];
        position[0] = objectRef.transform.position.x;
        position[1] = objectRef.transform.position.y;
        position[2] = objectRef.transform.position.z;

        rotation = new float[4];
        rotation[0] = objectRef.transform.rotation.x;
        rotation[1] = objectRef.transform.rotation.y;
        rotation[2] = objectRef.transform.rotation.z;
        rotation[3] = objectRef.transform.rotation.w;
    }
}