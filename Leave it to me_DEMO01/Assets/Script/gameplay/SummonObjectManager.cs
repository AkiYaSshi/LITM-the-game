using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// �N�����b�ƹ��Ҧb��m�A�çl���b����W
/// </summary>
public class SummonObjectManager : MonoBehaviour
{
    [SerializeField] 
    private Grid grid;
    [SerializeField] 
    private GameObject objectParentTo;
    [SerializeField] 
    private GameObject objectSpawnAt;
    [SerializeField] private LayerMask objectLayerMask;

    [SerializeField]
    private ObjectDataSO ObjectData;
    private int selectedObjectIndex = -1;

    private void Start()
    {
        objectLayerMask = 8;
    }

    /// <summary>
    /// ���U���s�ɡA�ͦ���������b�ж�����
    /// </summary>
    public void SummonObject(int ID)
    {
        selectedObjectIndex = ObjectData.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError("No ID found " + ID);
        }

        Vector3Int gridPos = grid.WorldToCell(objectSpawnAt.transform.position);
        GameObject newObject = Instantiate(ObjectData.objectsData[selectedObjectIndex].Prefab, objectParentTo.transform);

        SetObjectData(newObject, ObjectData.objectsData[selectedObjectIndex]);
        SetChildLayer(newObject);
        newObject.transform.position = grid.CellToWorld(gridPos);
    }

    /// <summary>
    /// �NS�ݭn��component�x�s�ܥͦ�������
    /// </summary>
    /// <param name="newObject">�ͦ�����</param>
    /// <param name="origin">��l�������O</param>
    private void SetObjectData(GameObject newObject, ObjectData origin)
    {
        // �N������ ObjectData ���[��ͦ�������
        ObjectRef objectRef = newObject.AddComponent<ObjectRef>();
        objectRef.objectData = origin;

        //�N�һ�component���[��ͦ�������
        newObject.AddComponent<SnapToGrid>();
        newObject.AddComponent<ObjectTransformer>();
        newObject.AddComponent<DetactCollision>();

        Rigidbody body = newObject.AddComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeAll;

        body.useGravity = false;
        body.isKinematic = true;
    }

    /// <summary>
    /// �ͦ����󤤡A�l���󪺳]�w
    /// </summary>
    /// <param name="newObject"></param>
    private void SetChildLayer(GameObject newObject)
    {
        newObject.layer = objectLayerMask;
        foreach (Transform childTransform in newObject.transform)
        {
            GameObject child = childTransform.gameObject;
            if(child.GetComponent<MeshRenderer>() == null)
            {
                foreach(Transform childTransform2 in child.transform)
                {
                    setChildComponent(childTransform2);
                }
            }
            else
            {
                setChildComponent(childTransform);
            }
        }
        
        //���Ƭ��l����[�W����
        void setChildComponent(Transform childTransform)
        {
            GameObject child2 = childTransform.gameObject;
            child2.layer = objectLayerMask;
        }
    }

}
/// <summary>
/// �x�s��l�����Ʀb�ͦ����󤤡A���bcomponent�������O
/// </summary>
public class ObjectRef : MonoBehaviour
{
    public ObjectData objectData;
}
