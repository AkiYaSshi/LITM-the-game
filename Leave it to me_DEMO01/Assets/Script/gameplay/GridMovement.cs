using UnityEngine;

/// <summary>
/// 使物件網任何方向移動一個Grid單位
/// </summary>
public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    /// <summary>
    /// 一單位的網格大小
    /// </summary>
    public static float unit;
    public static void MoveUP(GameObject _moveObj)
    {
        _moveObj.transform.position += new Vector3(0, unit, 0);
        Debug.Log("Move UP");
    }
    private void Start()
    {
        unit = grid.cellSize.x;
    }
}
