using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DetactCollision: MonoBehaviour
{
    [SerializeField] private float sphereRadius = 0.1f; // �y���b�|
    [SerializeField] private Color pointColor = Color.red; // �I���C��
    private List<Vector3> pointToMark = new();

    [SerializeField]
    private LayerMask layerMask = 1 << 7; //���i���|���ϼh

    private static readonly float boxSize = GridMovement.unit * 0.5f;
    private Vector3 size = new Vector3(boxSize, boxSize, boxSize);


    private ObjectData data;



    /// <summary>
    /// �ˬd���w�y�ЬO�_�� Collider
    /// </summary>
    /// <param name="pos">�����I�y��</param>
    public bool ObjectTransformer_CheckCollision(Vector3 pos)
    {
        if (gameObject.CompareTag("Focus"))
        {
            List<Vector3> points = CalObjectPoint(pos);
            foreach (var point in points)
            {
                Collider[] hitColliders = Physics.OverlapBox(point, size, Quaternion.Euler(0, 0, 0), layerMask);
                if(hitColliders.Length > 0) //�S������point�I��I���c�~�i�^��False
                {
                    return true;
                }
            }
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
        List<Vector3> pointInObj = new(); //���󤺨C�ӳ�쪺�����I
        for (int i = 0; i < data.Size.x; i++)
        {
            for (int j = 0; j < data.Size.y; j++)
            {
                for(int k = 0; k < data.Size.z; k++)
                {
                    //TODO: �d��������j�������X���I�ƶq���ŭn�D
                    //�o�쪫�󤺦U���Z�������I������
                    Vector3 childpoint = new(i * GridMovement.unit, 
                                             j * GridMovement.unit, 
                                             k * GridMovement.unit);

                    //�N��줺�����I�[�J���󪺱���
                    Vector3 pointWithRotate = gameObject.transform.rotation * childpoint;

                    //�N�����I�[�W�����A�o��@�ɮy��
                    Vector3 pointToWorld = gridpos + pointWithRotate;
                    pointInObj.Add(pointToWorld);

                }
            }
        }
        pointToMark = pointInObj;
        return pointInObj;
    }
    private void FixedUpdate()
    {
        foreach (Vector3 point in pointToMark)
        {
            Debug.DrawRay(point, Vector3.up * sphereRadius, pointColor, 1/60f);
            Debug.DrawRay(point, Vector3.forward * sphereRadius, pointColor, 1 / 60f);
            Debug.DrawRay(point, Vector3.right * sphereRadius, pointColor, 1 / 60f);
        }
        pointToMark.Clear();
    }
    private void Start()
    {
        //���o������󪺤j�p
        data = gameObject.GetComponent<ObjectRef>().objectData;
    }
}
