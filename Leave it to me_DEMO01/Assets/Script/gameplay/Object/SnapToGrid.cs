using System;
using UnityEngine;

/// <summary>
/// �Ϫ���X�z�������
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

        //��X�̲׺���W����m
        Vector3Int gridPos = grid.WorldToCell(_objPos);
        return grid.GetCellCenterWorld(gridPos);

    }

    /// <summary>
    /// �Ϫ����m���n��z�a�O�����
    /// </summary>
    private Vector3 ConstrainPosition(Vector3 _objPos)
    {
        //����a�O
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
