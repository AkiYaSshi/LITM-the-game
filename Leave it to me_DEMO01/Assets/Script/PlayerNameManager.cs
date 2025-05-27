using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    public PlayerData playerData;

    public TMP_InputField nameField;
    public Button sumitBtn;
    void Start()
    {
        playerData.playerName = "Player";
        playerData.InfoShow();
    }
    public void SaveName()
    {
        string inputName = nameField.text;
        if (string.IsNullOrEmpty(inputName))
        {
            playerData.playerName = inputName;
        }
        else
        {
            return;
        }
        playerData.InfoShow();
        SceneManager_script.ToScene(1);
    }
}
