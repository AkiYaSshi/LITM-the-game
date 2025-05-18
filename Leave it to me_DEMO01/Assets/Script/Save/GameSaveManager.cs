using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }
    public void LoadGame()
    {
        SaveSystem.LoadGame();
    }
}
