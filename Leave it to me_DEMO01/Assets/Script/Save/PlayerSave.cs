using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public string Name;
    public bool tutorial;

    public PlayerSave(string name, bool tutorial)
    {
        this.Name = name;
        this.tutorial = tutorial;
    }
}
