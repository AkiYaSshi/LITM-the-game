using UnityEngine;

[System.Serializable]
public class RoomSave
{
    public float[] rotation;
    public RoomSave(RoomData roomData)
    {
        rotation = new float[4];
        rotation[0] = roomData.transform.rotation.x;
        rotation[1] = roomData.transform.rotation.y;
        rotation[2] = roomData.transform.rotation.z;
        rotation[3] = roomData.transform.rotation.w;
    }
}
