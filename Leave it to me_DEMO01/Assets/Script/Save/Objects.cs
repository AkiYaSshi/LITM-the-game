using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提供場景中物件的狀態收集功能。
/// </summary>
public static class Objects
{
    private const string PARENTNAME = "Insantiate_geo";  // 父物件名稱，用於尋找場景中的物件

    /// <summary>
    /// 獲取場上所有物件的存檔資料。
    /// </summary>
    /// <returns>包含所有物件狀態的 ObjectSave 列表</returns>
    public static List<ObjectSave> GetAllObjectSave()
    {
        List<ObjectSave> objectSaves = new();
        foreach (Transform child in GameObject.Find(PARENTNAME).transform)
        {
            ObjectRef objectRef = child.GetComponent<ObjectRef>();
            ObjectSave save = new(objectRef);
            objectSaves.Add(save);
        }
        return objectSaves;
    }
}