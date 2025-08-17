using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TasksSave
{
    /// <summary>
    /// 各任務完成狀況
    /// </summary>
    public List<bool> tasksComplete;

    public TasksSave(List<bool> taskData) 
    {
        tasksComplete = new List<bool>(6);
        tasksComplete = taskData;
    }
}
