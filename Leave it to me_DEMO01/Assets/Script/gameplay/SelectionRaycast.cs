using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 使物件的每個點從六方向發射射線
/// </summary>
public class SelectionRaycast : MonoBehaviour
{
    public static LayerMask targetLayer;

    /// <summary>
    /// 所有軸向為x的射線
    /// </summary>
    public static List<Transform> hitx = new List<Transform>();
    /// <summary>
    /// 所有軸向為y的射線
    /// </summary>
    public static List<Transform> hity = new List<Transform>();
    /// <summary>
    /// 所有軸向為z的射線
    /// </summary>
    public static List<Transform> hitz = new List<Transform>();

    public static Color[] rayColors;

    private void Start()
    {
        targetLayer = LayerMask.GetMask("As Grid");
    }

    /// <summary>
    /// 從六個方向發射射線進行探測
    /// </summary>
    /// <param name="inheritData">物件的數據</param>
    /// <param name="layerMask">目標層</param>
    /// <param name="rayColors">射線顏色陣列</param>
    public static GameObject StartRayCast(GameObject obj, ObjectData inheritData)
    {
        if (obj != null)
        {
            Vector3 position = obj.transform.position;

            // x軸
            AxisRayCast(obj, position, inheritData.Size.z, inheritData.Size.y, inheritData.Size.x, obj.transform.right, hitx);

            // y軸
            AxisRayCast(obj, position, inheritData.Size.z, inheritData.Size.x, inheritData.Size.y, obj.transform.up, hity);

            // z軸
            AxisRayCast(obj, position, inheritData.Size.y, inheritData.Size.x, inheritData.Size.z, obj.transform.forward, hitz);

            return obj;
        }
        Debug.LogError("There are no object RC able");
        return null;
    }

    /// <summary>
    /// 從某軸向射出正反方向的射線
    /// </summary>
    /// <param name="obj">射出射線的物件</param>
    /// <param name="position">物件位置</param>
    /// <param name="Size1"></param>
    /// <param name="Size2"></param>
    /// <param name="CurAxis">射線進行方向</param>
    /// <param name="RayDirection"></param>
    private static void AxisRayCast(GameObject obj,
                                    Vector3 position,
                                    int Size1,
                                    int Size2,
                                    int CurAxis,
                                    Vector3 RayDirection,
                                    List<Transform> hitList)
    {
        hitList.Clear(); //清空放置hit的List
        for (int i = 0; i < Size1; i++)
        {
            for (int j = 0; j < Size2; j++)
            {
                Vector3 localOffset = new(); //將射線開始點移動到每個unit中心
                if ((RayDirection == obj.transform.right))
                {
                    localOffset = new Vector3(0, j * GridMovement.unit, i * GridMovement.unit);
                }
                else if ((RayDirection == obj.transform.up))
                {
                    localOffset = new Vector3(j * GridMovement.unit, 0, i * GridMovement.unit);
                }
                else if (RayDirection == obj.transform.forward)
                {
                    localOffset = new Vector3(j * GridMovement.unit,i * GridMovement.unit, 0);
                }
                Vector3 pivot = PivotWithRotateOffset(obj, position, localOffset);

                Physics.Raycast(pivot, RayDirection, out RaycastHit hit, CurAxis * GridMovement.unit, targetLayer); // positive
                if (hit.transform != null)
                {
                    hitList.Add(hit.transform);
                }

                Physics.Raycast(pivot, -RayDirection, out hit, GridMovement.unit, targetLayer); // negative
                if (hit.transform != null)
                {
                    hitList.Add(hit.transform);
                }

                DebugDrawRays(pivot, RayDirection, CurAxis, 0);
            }
        }
    }

    /// <summary>
    /// 將 pivot 相對於 obj 中心的局部偏移旋轉到全局座標
    /// </summary>
    /// <param name="obj">物件</param>
    /// <param name="position">物件位置</param>
    /// <param name="localOffset">局部偏移量</param>
    /// <returns>旋轉後的 pivot 位置</returns>
    private static Vector3 PivotWithRotateOffset(GameObject obj, Vector3 position, Vector3 localOffset)
    {
        Vector3 rotatedOffset = obj.transform.rotation * localOffset;
        Vector3 pivot = position + rotatedOffset;
        return pivot;
    }

    /// <summary>
    /// 繪製射線用於除錯
    /// </summary>
    /// <param name="pivot">射線起點</param>
    /// <param name="_Dir">射線方向</param>
    /// <param name="_Dist">射線距離</param>
    /// <param name="_colorIndex">顏色索引</param>
    /// <param name="rayColors">射線顏色陣列</param>
    private static void DebugDrawRays(Vector3 pivot, Vector3 _Dir, int _Dist, int _colorIndex)
    {
        Debug.DrawRay(pivot, _Dir * _Dist * GridMovement.unit, Color.black, 1 / 30f);
        Debug.DrawRay(pivot, -_Dir * GridMovement.unit, Color.black, 1 / 30f);
    }
    
}