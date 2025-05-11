using UnityEngine;
using TMPro;

public class SaveBtnClick : MonoBehaviour
{
    public PlayerData playerData;
    public gameplay_RoomShift room;

    public TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = "Save";
    }
    public void SaveButton()
    {
        SaveSystem.SavePlayer(playerData, room);
        buttonText.text = "Save Done!";
        Invoke("status", 2f);
    }
    void status()
    {
        buttonText.text = "Save";
    }
}
