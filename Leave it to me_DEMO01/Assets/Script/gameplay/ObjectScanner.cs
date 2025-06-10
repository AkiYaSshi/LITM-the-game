using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectScanner : MonoBehaviour
{

    [Tooltip("包含所有掃描物件的父物件")]
    [SerializeField] private GameObject instantiateGeo;

    public static GameObject standard;

    private void Start()
    {
        standard = instantiateGeo;
    }

    // 檢測指定 ID 的物件是否存在
    public static bool DetectObject(int id)
    {
        if (standard == null)
        {
            Debug.LogError("掃描基準父物件無效");
            return false;
        }

        ObjectRef[] objects = standard.GetComponentsInChildren<ObjectRef>();
        foreach (var obj in objects)
        {
            if (obj.objectData != null && obj.objectData.ID == id)
                return true;
        }
        Debug.LogError($"沒有找到相符的物件：{id}");
        return false;
    }

    // 計算指定 ID 的物件數量
    public static int ObjectCount(int id)
    {
        Debug.Log($"掃描物件數量，物件id：{id}");
        if (standard == null) {
            Debug.LogError("掃描基準父物件無效");
            return 0; 
        }

        ObjectRef[] objects = standard.GetComponentsInChildren<ObjectRef>();
        int count = 0;
        foreach (var obj in objects)
        {
            if (obj.objectData != null && obj.objectData.ID == id)
                count++;
        }
        Debug.Log($"id為{id}的物件共有：{count}個");
        return count;
    }
}
