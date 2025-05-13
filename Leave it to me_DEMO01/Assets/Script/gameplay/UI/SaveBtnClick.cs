using UnityEngine;
using TMPro;
using System;

public class SaveBtnClick : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = "Save";
    }

    /// <summary>
    /// «ö¤USave«ö¶s
    /// </summary>
    public void SaveButton()
    {
        buttonText.text = "Save Done!";
        Invoke("status", 2f);
    }
    void status()
    {
        buttonText.text = "Save";
    }
}
