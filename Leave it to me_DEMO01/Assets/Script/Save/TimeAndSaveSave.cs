using UnityEngine;

[Tooltip("�ΨӧǦC�Ʈɶ���T�A�s���ɮת����O")]
[System.Serializable]
public class TimeAndSaveSave
{
    public string timeStamp;
    public TimeAndSaveSave(TimaAndSaveData timaAndSave)
    {
        this.timeStamp = timaAndSave.GetTimeStamp();
    }
}
