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
        playerName.text = "執行人：";
        endTime.text = "執行日期：";

        playerName.text += NameManager.inGameName;
        endTime.text += DateTime.Now;
    }
}
