using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TasksSave
{
    /// <summary>
    /// �U���ȧ������p
    /// </summary>
    public List<bool> tasksComplete;

    public TasksSave(List<bool> taskData) 
    {
        tasksComplete = new List<bool>(6);
        tasksComplete = taskData;
    }
}
