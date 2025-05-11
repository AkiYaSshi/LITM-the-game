using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData", menuName = "playerData")]
public class PlayerData : ScriptableObject
{
    
    public string playerName = "Player";

    public void InfoShow()
    {
        Debug.Log(playerName);
    }
}
