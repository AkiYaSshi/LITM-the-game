using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string name;
    public int direction;

    public SaveData (PlayerData playerData, gameplay_RoomShift room)
    {
        name = playerData.playerName;
    }
}
