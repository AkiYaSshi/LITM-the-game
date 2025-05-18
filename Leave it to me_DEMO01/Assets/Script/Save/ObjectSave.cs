using UnityEngine;

/// <summary>
/// 儲存物件的存檔資料，包含 ID、位置和旋轉資訊。
/// </summary>
[System.Serializable]
public class ObjectSave
{
    public int objectId;        // 物件的唯一識別碼
    public float[] position;    // 物件的位置座標 (x, y, z)
    public float[] rotation;    // 物件的旋轉四元數 (x, y, z, w)

    /// <summary>
    /// 從 ObjectRef 構造 ObjectSave 物件。
    /// </summary>
    /// <param name="objectRef">包含物件資料的 ObjectRef 組件</param>
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