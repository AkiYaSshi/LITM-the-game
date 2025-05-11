using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DetactCollision: MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask = 1 << 8; //���i���|���ϼh

    private static readonly float boxSize = GridMovement.unit * 0.5f;
    private Vector3 size = new Vector3(boxSize, boxSize, boxSize);

    private ObjectData data;



    /// <summary>
    /// �ˬd���w�y�ЬO�_�� Collider
    /// </summary>
    /// <param name="pos">�����I�y��</param>
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
    /// �p�⪫�󤺦b����W�I���@�ɮy��
    /// </summary>
    /// <returns>�y�Э�</returns>
    /// <param name="gridpos">�����I�y��</param>
    private List<Vector3> CalObjectPoint(Vector3 gridpos)
    {
        List<Vector3> point = new(); //���󤺨C�ӳ�쪺�����I
        for (int i = 0; i < data.Size.x; i++)
        {
            for (int j = 0; j < data.Size.y; j++)
            {
                for(int k = 0; k < data.Size.z; k++)
                {
                    //�o�쪫�󤺦U���Z�������I������
                    Vector3 childpoint = new(i * GridMovement.unit, 
                                             j * GridMovement.unit, 
                                             k * GridMovement.unit);

                    //�N��줺�����I�[�J���󪺱���
                    Vector3 pointWithRotate = gameObject.transform.rotation * childpoint;

                    //�N�����I�[�W�����A�o��@�ɮy��
                    Vector3 pointToWorld = gridpos + pointWithRotate;

                    point.Add(pointToWorld);
                }
            }
        }
        return point;
    }
    private void Start()
    {
        //���o������󪺤j�p
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
