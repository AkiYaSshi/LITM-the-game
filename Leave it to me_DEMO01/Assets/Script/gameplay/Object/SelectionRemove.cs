using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
        objectReference = target.GetComponent<ObjectRef>();

        Destroy(target, timeDelay);
        Debug.Log($"destroy {objectReference.objectData.Name}.");
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
