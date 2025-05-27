using UnityEngine;
using System;
using System.Globalization;

[Tooltip("���o�ɶ���T�����O")]
public class TimaAndSaveData : MonoBehaviour
{
    public string timeStamp;

    string format;
    CultureInfo culture;

    public string GetTimeStamp()
    {
        SetFormat();
        return timeStamp = DateTime.Now.ToString(format, culture);
    }
    public void SetFormat()
    {
        string[] result = new string[2];

        culture = CultureInfo.GetCultureInfo("en-US");
        format = "MMM dd HH:mm:ss, yyyy";
    }
}
