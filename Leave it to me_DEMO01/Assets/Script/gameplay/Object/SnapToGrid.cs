using System;
using UnityEngine;

/// <summary>
/// 使物件合理對齊網格
/// </summary>
public class SnapToGrid : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    private void Awake()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
    private void Start()
    {
        gameObject.transform.position = ObjectToGrid(gameObject.transform.position);
    }
    private Vector3 ObjectToGrid(Vector3 _objPos)
    {
        _objPos = ConstrainPosition(_objPos);

        //輸出最終網格上的位置
        Vector3Int gridPos = grid.WorldToCell(_objPos);
        return grid.GetCellCenterWorld(gridPos);

    }

    /// <summary>
    /// 使物件位置不要穿透地板或牆壁
    /// </summary>
    private Vector3 ConstrainPosition(Vector3 _objPos)
    {
        //高於地板
        if (_objPos.y < 0.2f)
        {
            _objPos = new Vector3(
                _objPos.x,
                0.2f,
                _objPos.z
            );
        }

        return _objPos;
    }

    private void OnEnable()
    {
        ObjectTransformer.DraggingObject += ObjectToGrid;
    }
    private void OnDisable()
    {
        ObjectTransformer.DraggingObject -= ObjectToGrid;
    }
}
