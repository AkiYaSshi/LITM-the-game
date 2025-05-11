using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DetactCollision: MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask = 1 << 8; //不可重疊的圖層

    private static readonly float boxSize = GridMovement.unit * 0.5f;
    private Vector3 size = new Vector3(boxSize, boxSize, boxSize);

    private ObjectData data;



    /// <summary>
    /// 檢查指定座標是否有 Collider
    /// </summary>
    /// <param name="pos">中心點座標</param>
    private bool ObjectTransformer_CheckCollision(Vector3 pos)
    {
        List<Vector3> points = CalObjectPoint(pos);
        foreach (var point in points)
        {
            Collider[] hitColliders = Physics.OverlapBox(point, size, Quaternion.Euler(0, 0, 0), layerMask);
            return hitColliders.Length > 0;
        }
        return false;
    }
    /// <summary>
    /// 計算物件內在網格上點的世界座標
    /// </summary>
    /// <returns>座標值</returns>
    /// <param name="gridpos">中心點座標</param>
    private List<Vector3> CalObjectPoint(Vector3 gridpos)
    {
        List<Vector3> point = new(); //物件內每個單位的中心點
        for (int i = 0; i < data.Size.x; i++)
        {
            for (int j = 0; j < data.Size.y; j++)
            {
                for(int k = 0; k < data.Size.z; k++)
                {
                    //得到物件內各單位距離中心點的偏移
                    Vector3 childpoint = new(i * GridMovement.unit, 
                                             j * GridMovement.unit, 
                                             k * GridMovement.unit);

                    //將單位內中心點加入物件的旋轉
                    Vector3 pointWithRotate = gameObject.transform.rotation * childpoint;

                    //將中心點加上偏移，得到世界座標
                    Vector3 pointToWorld = gridpos + pointWithRotate;

                    point.Add(pointToWorld);
                }
            }
        }
        return point;
    }
    private void Start()
    {
        //取得選取物件的大小
        data = gameObject.GetComponent<ObjectRef>().objectData;
    }
    private void OnEnable()
    {
        ObjectTransformer.CheckCollision += ObjectTransformer_CheckCollision;
    }
    private void OnDisable()
    {
        ObjectTransformer.CheckCollision -= ObjectTransformer_CheckCollision;
    }
}
