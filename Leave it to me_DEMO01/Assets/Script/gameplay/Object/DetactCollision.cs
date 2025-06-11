using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UnityEngine;

public class DetactCollision: MonoBehaviour
{
    [SerializeField] private Color pointColor = Color.red; // �I���C��
    private List<Vector3> pointToMark = new();

    [SerializeField]
    private string LapTag = "Lapping";

    [SerializeField]
    private LayerMask layerMask = 1 << 8; //���i���|���ϼh: As Grid&Insantiate

    private static readonly float boxSize = GridMovement.unit * 0.5f;
    private Vector3 size = new Vector3(boxSize, boxSize, boxSize);


    private ObjectData data;



    /// <summary>
    /// �ˬd���w�y�ЬO�_�� Collider
    /// </summary>
    /// <param name="pos">�����I�y��</param>
    public bool ObjectTransformer_CheckCollision(Vector3 pos, Quaternion? quaternion = null)
    {
        if (gameObject.CompareTag("Focus"))
        {
            //�p�G����]�w���i���|�A�����^��false
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
                if(hasCollider.Count > 0) //�S������point�I��I���c�~�i�^��False
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
    /// <param name="quaternion">�������q(�Ω�ObjectTransFormer.Rotate)</param>
    private List<Vector3> CalObjectPoint(Vector3 gridpos, Quaternion? quaternion = null)
    {
        List<Vector3> pointInObj = new(); //���󤺨C�ӳ�쪺�����I
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

                    if(quaternion.HasValue)pointWithRotate = quaternion.Value * pointWithRotate;


                    //�N�����I�[�W�����A�o��@�ɮy��
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
        //���o������󪺤j�p
        data = gameObject.GetComponent<ObjectRef>().objectData;
    }
}

