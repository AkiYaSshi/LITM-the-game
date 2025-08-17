using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class TasksData
{
    public static List<bool> tasksComplete = new List<bool> { false, false, false, false, false, false };

    public static void SetCompletement(int ID)
    {
        tasksComplete[ID] = true;
    }

}
