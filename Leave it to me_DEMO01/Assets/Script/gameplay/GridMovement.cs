using UnityEngine;

/// <summary>
/// �Ϫ���������V���ʤ@��Grid���
/// </summary>
public class GridMovement : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    /// <summary>
    /// �@��쪺����j�p
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
