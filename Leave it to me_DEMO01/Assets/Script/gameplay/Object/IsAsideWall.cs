using UnityEngine;

/// <summary>
/// 依據射線判斷物件有沒有靠牆，若有則加上Is Display判定
/// </summary>
public class IsAsideWall : MonoBehaviour
{
    ObjectRef objectReference;
    private void AddComponent()
    {
        IsDisplay isDisplay = gameObject.AddComponent<IsDisplay>();
        isDisplay.fowardAxis = objectReference.objectData.FowardAxis;
    }
    private void Start()
    {
        objectReference = GetComponent<ObjectRef>();
    }
}
