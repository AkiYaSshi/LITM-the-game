using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectScanner : MonoBehaviour
{

    [Tooltip("�]�t�Ҧ����y���󪺤�����")]
    [SerializeField] private GameObject instantiateGeo;

    public static GameObject standard;

    private void Start()
    {
        standard = instantiateGeo;
    }

    // �˴����w ID ������O�_�s�b
    public static bool DetectObject(int id)
    {
        if (standard == null)
        {
            Debug.LogError("���y��Ǥ�����L��");
            return false;
        }

        ObjectRef[] objects = standard.GetComponentsInChildren<ObjectRef>();
        foreach (var obj in objects)
        {
            if (obj.objectData != null && obj.objectData.ID == id)
                return true;
        }
        Debug.LogError($"�S�����۲Ū�����G{id}");
        return false;
    }

    // �p����w ID ������ƶq
    public static int ObjectCount(int id)
    {
        Debug.Log($"���y����ƶq�A����id�G{id}");
        if (standard == null) {
            Debug.LogError("���y��Ǥ�����L��");
            return 0; 
        }

        ObjectRef[] objects = standard.GetComponentsInChildren<ObjectRef>();
        int count = 0;
        foreach (var obj in objects)
        {
            if (obj.objectData != null && obj.objectData.ID == id)
                count++;
        }
        Debug.Log($"id��{id}������@���G{count}��");
        return count;
    }
}
