using UnityEngine;

/// <summary>
/// �Ϫ��󪺨C���I�q����V�o�g�g�u
/// </summary>
public class SelectionRaycast : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private string raycastObjTag = "Focus";
    [SerializeField]
    private Color[] RayColor = new Color[3];


    ObjectData inheritData;
    private void FixedUpdate()
    {
        StartRayCast();
    }
    private void StartRayCast()
    {
        GameObject obj = GameObject.FindGameObjectWithTag(raycastObjTag);
        if (obj != null)
        {
            Vector3 position = obj.transform.position;
            inheritData = obj.GetComponent<ObjectRef>().objectData;
            RaycastHit hit;

            //x�b
            for (int i = 0; i < inheritData.Size.z; i++)
            {
                for (int j = 0; j < inheritData.Size.y; j++)
                {
                    // �p��۹�� obj ���ߪ���������
                    Vector3 localOffset = new Vector3(0, j * GridMovement.unit, i * GridMovement.unit);

                    Vector3 pivot = PivotWithRotateOffset(obj, position, localOffset);

                    Physics.Raycast(pivot,
                                    obj.transform.right,
                                    out hit,
                                    inheritData.Size.x * GridMovement.unit,
                                    targetLayer); //positive
                    Physics.Raycast(pivot,
                                    -obj.transform.right,
                                    GridMovement.unit,
                                    targetLayer);//negative
                    DebugDrawRays(pivot, obj.transform.right, inheritData.Size.x, 0);
                }
            }
            //y�b
            for (int i = 0; i < inheritData.Size.z; i++)
            {
                for (int j = 0; j < inheritData.Size.x; j++)
                {
                    // �p��۹�� obj ���ߪ���������
                    Vector3 localOffset = new Vector3(j * GridMovement.unit, 0, i * GridMovement.unit);

                    Vector3 pivot = PivotWithRotateOffset(obj, position, localOffset);

                    Physics.Raycast(pivot,
                                    obj.transform.up,
                                    out hit,
                                    inheritData.Size.y * GridMovement.unit,
                                    targetLayer); //positive
                    Physics.Raycast(pivot,
                                    -obj.transform.up,
                                    GridMovement.unit,
                                    targetLayer);//negative
                    DebugDrawRays(pivot, obj.transform.up, inheritData.Size.y, 1);
                }
            }
            //z�b
            for (int i = 0; i < inheritData.Size.y; i++)
            {
                for (int j = 0; j < inheritData.Size.x; j++)
                {
                    // �p��۹�� obj ���ߪ���������
                    Vector3 localOffset = new Vector3(j * GridMovement.unit, i * GridMovement.unit, 0);

                    Vector3 pivot = PivotWithRotateOffset(obj, position, localOffset);

                    Physics.Raycast(pivot,
                                    obj.transform.forward,
                                    out hit,
                                    inheritData.Size.z * GridMovement.unit,
                                    targetLayer); //positive
                    Physics.Raycast(pivot,
                                    -obj.transform.forward,
                                    GridMovement.unit,
                                    targetLayer);//negative
                    DebugDrawRays(pivot, obj.transform.forward, inheritData.Size.z, 2);
                }
            }
        }
    }

    /// <summary>
    /// �Npivot�۹��obj���ߪ������������������y��
    /// </summary>
    /// <param name="obj">����</param>
    /// <param name="position">�����m</param>
    /// <param name="localOffset"></param>
    /// <returns></returns>
    private static Vector3 PivotWithRotateOffset(GameObject obj, Vector3 position, Vector3 localOffset)
    {
        // �N�����������������y�С]�Ҽ{ obj ������^
        Vector3 rotatedOffset = obj.transform.rotation * localOffset;

        // �p��̲ת� pivot ��m�]���� + ����᪺�����^
        Vector3 pivot = position + rotatedOffset;
        return pivot;
    }

    private void DebugDrawRays(Vector3 pivot, Vector3 _Dir, int _Dist, int _colorIndex)
    {
        Debug.DrawRay(pivot, _Dir * _Dist * GridMovement.unit, RayColor[_colorIndex], 1 / 30f);
        Debug.DrawRay(pivot, -_Dir * GridMovement.unit, RayColor[_colorIndex], 1 / 30f);
    }


    private void Start()
    {
        targetLayer = LayerMask.GetMask("Insantiated");
    }
}
