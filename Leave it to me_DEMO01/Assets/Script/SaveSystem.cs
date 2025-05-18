using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// �s�ɡBŪ�ɦ欰
/// </summary>
public class SaveSystem : MonoBehaviour
{
    private List<SavRoom> rooms = new();
    private ObjectInitialize objectInit = new();

    private const string PARENTNAME = "Insantiate_geo";

    private string savePath = "/saves/user.json";
    public void AddRoom(SavRoom room) => rooms.Add(room);

    /// <summary>
    /// �b�Ū�save data����J����æs��
    /// </summary>
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        foreach (var room in rooms)
            saveData.AddMemento(room.Create());

        //���o��m�Ҧ��ͦ����󪺤�����
        GameObject allInsantiate = GameObject.Find(PARENTNAME);
        //������U�Ҧ��l����I�s�x�s���
        foreach(Transform child in allInsantiate.transform)
        {
            SavGameObject savGameObject = child.GetComponent<SavGameObject>();
            saveData.AddMemento(savGameObject.Create());
        }

        saveData.SaveToFile(Application.persistentDataPath +  savePath);
        Debug.Log($"�w�N�ɮ��x�s�ܡG{Application.persistentDataPath + savePath}");
    }

    /// <summary>
    /// �q�ɮפ�Ū��Save Data�A�N��ƮM�Ψ�C��������
    /// </summary>
    public void LoadGame()
    {
        SaveData saveData = SaveData.LoadFromFile(Application.persistentDataPath + savePath);

        //�٭�ж����બ�A
        foreach (var room in rooms)
        {
            Memento memento = saveData.GetMemento(room.ObjectId, "Room");
            if(memento !=null) room.Restore(memento);
        }

        if (saveData == null) return;

        
        foreach(var mementoData in saveData.MementoList)
        {
            if(mementoData.Type == "Object")
            {
                //�ھڦs�ɭ��s�ͦ�����
                objectInit.CreateObject<SavGameObject>("Object");

                //�٭쪫�󪬺A�G����id�B��m�B���൥��
                Memento memento = saveData.GetMemento(mementoData.ObjectId, "Object");
                if(memento != null)
                {
                    object obj = objectInit.GetObject(memento.ObjectId);
                    if ((obj is SavGameObject savGame))
                        savGame.Restore(memento);
                }
            }
        }


    }
}
