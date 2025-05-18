using System.Collections.Generic;
using UnityEngine;

public class ObjectInitialize
{
    private Dictionary<string, object> objectMap = new();
    private int idCounter = 0;
    private ObjectRef objectRef = new();

    public string CreateObjectId()
    {
        return $"obj_{idCounter++}";
    }

    /// <summary>
    /// 生成不在場景上的物件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public T CreateObject<T>(string type) where T : class
    {
        string objectId = CreateObjectId();
        T obj = null;

        if (typeof(T) == typeof(SavGameObject))
        {
            obj = new SavGameObject(objectId, Vector3.zero, Quaternion.Euler(0, 0, 0), null) as T;
        }
        else if(typeof(T) == typeof(SavRoom))
        {
            obj = new SavRoom(Quaternion.Euler(0, 0, 0), objectId) as T;
        }

        if (obj != null)
        {
            objectMap[objectId] = obj;
        }
        return obj;
    }
    public object GetObject(string objectId)
    {
        return objectMap.ContainsKey(objectId) ? objectMap[objectId] : null;
    }
}
