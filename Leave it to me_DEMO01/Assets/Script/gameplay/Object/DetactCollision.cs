using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UnityEngine;

public class DetactCollision: MonoBehaviour
{
    [SerializeField] private Color pointColor = Color.red; // 點的顏色
    private List<Vector3> pointToMark = new();

    [SerializeField]
    private string LapTag = "Lapping";

    [SerializeField]
    private LayerMask layerMask = 1 << 8; //不可重疊的圖層: As Grid&Insantiate

    private static readonly float boxSize = GridMovement.unit * 0.5f;
    private Vector3 size = new Vector3(boxSize, boxSize, boxSize);


    private ObjectData data;



    /// <summary>
    /// 檢查指定座標是否有 Collider
    /// </summary>
    /// <param name="pos">中心點座標</param>
    public bool ObjectTransformer_CheckCollision(Vector3 pos, Quaternion? quaternion = null)
    {
        if (gameObject.CompareTag("Focus"))
        {
            //如果物件設定為可重疊，直接回傳false
            if(gameObject.GetComponent<ObjectRef>().objectData.noCollision)
                return false;

            List<Vector3> points = CalObjectPoint(pos, quaternion);
            foreach (var point in points)
            {
                Collider[] hitColliders = Physics.OverlapBox(point, size, Quaternion.Euler(0, 0, 0), layerMask);

                List<Collider> hasCollider = new();

                foreach (var hit in hitColliders)
                {
                    if (!hit.transform.parent.CompareTag(LapTag))
                    {
                        hasCollider.Add(hit);
                        UnityEngine.Debug.Log(hit.transform.gameObject.name);
                    }
                }
                if(hasCollider.Count > 0) //沒有任何point碰到碰撞箱才可回傳False
                {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// 計算物件內在網格上點的世界座標
    /// </summary>
    /// <returns>座標值</returns>
    /// <param name="gridpos">中心點座標</param>
    /// <param name="quaternion">物件旋轉量(用於ObjectTransFormer.Rotate)</param>
    private List<Vector3> CalObjectPoint(Vector3 gridpos, Quaternion? quaternion = null)
    {
        List<Vector3> pointInObj = new(); //物件內每個單位的中心點
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

                    if(quaternion.HasValue)pointWithRotate = quaternion.Value * pointWithRotate;


                    //將中心點加上偏移，得到世界座標
                    Vector3 pointToWorld = gridpos + pointWithRotate;
                    pointInObj.Add(pointToWorld);

                }
            }
        }
        pointToMark = pointInObj;
        return pointInObj;
    }
    private void Start()
    {
        //取得選取物件的大小
        data = gameObject.GetComponent<ObjectRef>().objectData;
    }
}

