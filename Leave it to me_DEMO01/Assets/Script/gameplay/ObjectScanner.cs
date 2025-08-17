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
            return false;
        }

        ObjectRef[] objects = standard.GetComponentsInChildren<ObjectRef>();
        foreach (var obj in objects)
        {
            if (obj.objectData != null && obj.objectData.ID == id)
                return true;
        }
        return false;
    }

    // 計算指定 ID 的物件數量
    public static int ObjectCount(int id)
    {
        if (standard == null) {
            return 0; 
        }

        ObjectRef[] objects = standard.GetComponentsInChildren<ObjectRef>();
        int count = 0;
        foreach (var obj in objects)
        {
            if (obj.objectData != null && obj.objectData.ID == id)
                count++;
        }
        return count;
    }
}
