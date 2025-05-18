using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 將物件放在滑鼠所在位置，並吸附在網格上
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
    /// 按下按鈕時，生成對應物件在房間中央
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
    /// 將S需要的component儲存至生成的物件
    /// </summary>
    /// <param name="newObject">生成物件</param>
    /// <param name="origin">原始物件類別</param>
    private void SetObjectData(GameObject newObject, ObjectData origin)
    {
        // 將對應的 ObjectData 附加到生成的物件
        ObjectRef objectRef = newObject.AddComponent<ObjectRef>();
        objectRef.objectData = origin;

        //將所需component附加到生成的物件
        newObject.AddComponent<SnapToGrid>();
        newObject.AddComponent<ObjectTransformer>();
        newObject.AddComponent<DetactCollision>();

        Rigidbody body = newObject.AddComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeAll;

        body.useGravity = false;
        body.isKinematic = true;
    }

    /// <summary>
    /// 生成物件中，子物件的設定
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
        
        //重複為子物件加上附件
        void setChildComponent(Transform childTransform)
        {
            GameObject child2 = childTransform.gameObject;
            child2.layer = objectLayerMask;
        }
    }

}
/// <summary>
/// 儲存原始物件資料在生成物件中，掛在component內的類別
/// </summary>
public class ObjectRef : MonoBehaviour
{
    public ObjectData objectData;
}
