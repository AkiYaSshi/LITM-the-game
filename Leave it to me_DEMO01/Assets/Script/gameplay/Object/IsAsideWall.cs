using UnityEngine;

/// <summary>
/// �̾ڮg�u�P�_���󦳨S���a��A�Y���h�[�WIs Display�P�w
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
