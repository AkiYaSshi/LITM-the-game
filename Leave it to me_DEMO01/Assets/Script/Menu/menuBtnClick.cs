using System.IO;
using UnityEngine;
using System.Linq;

public class menuBtnClick : MonoBehaviour
{
    public GameObject loadBtn;
    public GameObject newBtn;
    public GameObject sumitPlayerName;

    public PlayerData playerData;
    //public gameplay_RoomShift room;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool hasSaveFiles = DetactSaves();
        loadBtn.SetActive(hasSaveFiles);

    }
    public void LoadExistingGame() //���J�ɮ�
    {
        SaveData data = SaveSystem.LoadSaveData();

        playerData.playerName = data.name;
        //room.m_DIRECTION = (gameplay_RoomShift.DIRECTION)data.direction;

        SceneManager_script.ToScene(1);
        playerData.InfoShow();
    }

    private bool DetactSaves() //�ˬd�O�_���s�ɹL
    {
        string path = Application.persistentDataPath + "/saves";
        return Directory.Exists(path) && Directory.EnumerateFiles(path, "player*").Any();
    }

}
