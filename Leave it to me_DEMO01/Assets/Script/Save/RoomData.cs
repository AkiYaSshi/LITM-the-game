using System;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public static event Action LoadRoom;
    public void restore(RoomSave roomSave)
    {
        gameObject.transform.rotation = new(roomSave.rotation[0],
                                            roomSave.rotation[1],
                                            roomSave.rotation[2],
                                            roomSave.rotation[3]);
        LoadRoom?.Invoke();
    }

    private void OnEnable()
    {
        SaveSystem.InitRoom += restore;
    }
    private void OnDisable()
    {
        SaveSystem.InitRoom -= restore;
    }
}
