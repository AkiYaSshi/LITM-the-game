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

    // �p����w ID ������ƶq
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
