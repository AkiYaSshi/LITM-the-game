using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInitialize : MonoBehaviour
{
    [SerializeField]
    private SummonObjectManager summon;
    [SerializeField]
    GameObject Parent;
    [SerializeField]
    RoomData roomData;

    /// <summary>
    /// 依據傳入的ObjectSave List，使用Summon Object Manager生成物件，最後將物件移至定點
    /// </summary>
    public void BuildLoad(List<ObjectSave> objects)
    {
        ClearAll();

        foreach (ObjectSave obj in objects)
        {
            GameObject newObject = summon.SummonObject(obj.objectId);
            newObject.transform.position = new(obj.position[0], obj.position[1], obj.position[2]);
            newObject.transform.rotation = new(obj.rotation[0], obj.rotation[1], obj.rotation[2], obj.rotation[3]);
        }
    }

    private void ClearAll()
    {
        foreach(Transform transform in Parent.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnEnable()
    {
        SaveSystem.InitObj += BuildLoad;
    }
    private void OnDisable()
    {
        SaveSystem.InitObj -= BuildLoad;
    }
}
