using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 刪除所選的物件
/// </summary>
public class SelectionRemove : MonoBehaviour
{
    #region Var
    private ObjectInteract objectInteract;
    private InputAction delete;

    private GameObject target;
    [SerializeField]
    private float timeDelay = 0.3f;

    private const string FOCUS = "Focus";

    ObjectRef objectReference;
    #endregion
    private void DeleteObject(InputAction.CallbackContext context)
    {
        GameObject target = GameObject.FindGameObjectWithTag(FOCUS);
        if (target != null)
        {
            objectReference = target?.GetComponent<ObjectRef>();

            Destroy(target, timeDelay);
        }
    }
    private void Awake()
    {
        objectInteract = new ObjectInteract();
    }

    private void OnEnable()
    {
        delete = objectInteract.ObjectTransform.delete;
        delete.performed += DeleteObject;
        delete.Enable();
    }


    private void OnDisable()
    {
        delete.performed -= DeleteObject;
        delete.Disable();
    }
}
