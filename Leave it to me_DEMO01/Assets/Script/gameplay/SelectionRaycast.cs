using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �Ϫ��󪺨C���I�q����V�o�g�g�u
/// </summary>
public class SelectionRaycast : MonoBehaviour
{
    public static LayerMask targetLayer;

    /// <summary>
    /// �Ҧ��b�V��x���g�u
    /// </summary>
    public static List<Transform> hitx = new List<Transform>();
    /// <summary>
    /// �Ҧ��b�V��y���g�u
    /// </summary>
    public static List<Transform> hity = new List<Transform>();
    /// <summary>
    /// �Ҧ��b�V��z���g�u
    /// </summary>
    public static List<Transform> hitz = new List<Transform>();

    public static Color[] rayColors;

    private void Start()
    {
        targetLayer = LayerMask.GetMask("As Grid");
    }

    /// <summary>
    /// �q���Ӥ�V�o�g�g�u�i�汴��
    /// </summary>
    /// <param name="inheritData">���󪺼ƾ�</param>
    /// <param name="layerMask">�ؼмh</param>
    /// <param name="rayColors">�g�u�C��}�C</param>
    public static GameObject StartRayCast(GameObject obj, ObjectData inheritData)
    {
        if (obj != null)
        {
            Vector3 position = obj.transform.position;

            // x�b
            AxisRayCast(obj, position, inheritData.Size.z, inheritData.Size.y, inheritData.Size.x, obj.transform.right, hitx);

            // y�b
            AxisRayCast(obj, position, inheritData.Size.z, inheritData.Size.x, inheritData.Size.y, obj.transform.up, hity);

            // z�b
            AxisRayCast(obj, position, inheritData.Size.y, inheritData.Size.x, inheritData.Size.z, obj.transform.forward, hitz);

            return obj;
        }
        Debug.LogError("There are no object RC able");
        return null;
    }

    /// <summary>
    /// �q�Y�b�V�g�X���Ϥ�V���g�u
    /// </summary>
    /// <param name="obj">�g�X�g�u������</param>
    /// <param name="position">�����m</param>
    /// <param name="Size1"></param>
    /// <param name="Size2"></param>
    /// <param name="CurAxis">�g�u�i���V</param>
    /// <param name="RayDirection"></param>
    private static void AxisRayCast(GameObject obj,
                                    Vector3 position,
                                    int Size1,
                                    int Size2,
                                    int CurAxis,
                                    Vector3 RayDirection,
                                    List<Transform> hitList)
    {
        hitList.Clear(); //�M�ũ�mhit��List
        for (int i = 0; i < Size1; i++)
        {
            for (int j = 0; j < Size2; j++)
            {
                Vector3 localOffset = new(); //�N�g�u�}�l�I���ʨ�C��unit����
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
    /// �N pivot �۹�� obj ���ߪ������������������y��
    /// </summary>
    /// <param name="obj">����</param>
    /// <param name="position">�����m</param>
    /// <param name="localOffset">���������q</param>
    /// <returns>����᪺ pivot ��m</returns>
    private static Vector3 PivotWithRotateOffset(GameObject obj, Vector3 position, Vector3 localOffset)
    {
        Vector3 rotatedOffset = obj.transform.rotation * localOffset;
        Vector3 pivot = position + rotatedOffset;
        return pivot;
    }

    /// <summary>
    /// ø�s�g�u�Ω󰣿�
    /// </summary>
    /// <param name="pivot">�g�u�_�I</param>
    /// <param name="_Dir">�g�u��V</param>
    /// <param name="_Dist">�g�u�Z��</param>
    /// <param name="_colorIndex">�C�����</param>
    /// <param name="rayColors">�g�u�C��}�C</param>
    private static void DebugDrawRays(Vector3 pivot, Vector3 _Dir, int _Dist, int _colorIndex)
    {
        Debug.DrawRay(pivot, _Dir * _Dist * GridMovement.unit, Color.black, 1 / 30f);
        Debug.DrawRay(pivot, -_Dir * GridMovement.unit, Color.black, 1 / 30f);
    }
    
}