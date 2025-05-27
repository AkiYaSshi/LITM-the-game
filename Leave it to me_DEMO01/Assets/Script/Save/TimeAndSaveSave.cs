using UnityEngine;

[Tooltip("用來序列化時間資訊，存為檔案的類別")]
[System.Serializable]
public class TimeAndSaveSave
{
    public string timeStamp;
    public TimeAndSaveSave(TimaAndSaveData timaAndSave)
    {
        this.timeStamp = timaAndSave.GetTimeStamp();
    }
}
