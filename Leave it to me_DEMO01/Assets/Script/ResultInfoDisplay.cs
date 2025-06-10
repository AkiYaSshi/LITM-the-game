using System;
using TMPro;
using UnityEngine;

public class ResultInfoDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TextMeshProUGUI endTime;


    private void Start()
    {
        playerName.text = "����H�G";
        endTime.text = "�������G";

        playerName.text += NameManager.inGameName;
        endTime.text += DateTime.Now;
    }
}
