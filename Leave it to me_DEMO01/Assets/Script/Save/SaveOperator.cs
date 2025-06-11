using UnityEngine;

public class SaveOperator : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }
    public void LoadGame()
    {
        SaveSystem.LoadGame();
    }
    private void Start()
    {
        Invoke("LoadGame", 0.01f);
    }
}
