using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ѳ��������󪺪��A�����\��C
/// </summary>
public static class Objects
{
    private const string PARENTNAME = "Insantiate_geo";  // ������W�١A�Ω�M�������������

    /// <summary>
    /// ������W�Ҧ����󪺦s�ɸ�ơC
    /// </summary>
    /// <returns>�]�t�Ҧ����󪬺A�� ObjectSave �C��</returns>
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