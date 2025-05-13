using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Build.Content;
using UnityEngine;

/// <summary>
/// �s��Ҧ����x�s��ƪ����O�A�H�ΥΩ�ϥθ�ƪ���k
/// </summary>
[System.Serializable]
public class SaveData
{
    /// <summary>
    /// ���P��memento���q�Ϊ����O�A�ΥH�x�s�@�ӭ�memento
    /// </summary>
    [System.Serializable]
    public class MementoData
    {
        public string Type;
        public string ObjectId;
        public string Data; //���PMemento�ǦC�Ƥ���s�o��
    }

    /// <summary>
    /// �Ҧ��Q�s�ɪ���/���A��List
    /// </summary>
    public List<MementoData> MementoList = new();

    /// <summary>
    /// �ھڬJ����memento�A�s�ؤ@��Memento Data�æs�iList��
    /// </summary>
    /// <param name="memento">�Q�x�s��memento</param>
    public void AddMemento(Memento memento)
    {
        MementoData data = new MementoData
        {
            Type = memento.Type,
            ObjectId = memento.ObjectId,
            Data = JsonUtility.ToJson(memento)
        };
        MementoList.Add(data);
    }

    /// <summary>
    /// �N�o�����O�����ǦC�ƫ�s����w�ɮ׸��|
    /// </summary>
    /// <param name="path">�ɮצW��</param>
    public void SaveToFile(string path)
    {
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path, json);
    }
    /// <summary>
    /// �N���|Ū���쪺json�ϧǦC�ƫ�^��
    /// </summary>
    /// <param name="path"></param>
    /// <returns>�s�J��Savedata��</returns>
    public static SaveData LoadFromFile(string path)
    {
        string json = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public Memento GetMemento(string type, string objectId)
    {
        var data = MementoList.Find(m => m.ObjectId == objectId && m.Type == type);
        if (data == null) return null;

        if(data.Type == "Object")
            return JsonUtility.FromJson<MemObject>(data.Data);
        else if (data.Type == "Room")
            return JsonUtility.FromJson<MemRoom>(data.Data);

        return null;
    }
}
