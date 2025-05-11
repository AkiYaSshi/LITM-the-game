using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// 偵測是否選取到物件，若無則取消選取所有物件
/// </summary>
public class SelecctionManager : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] LayerMask layerMask;
    [SerializeField] Color RayColor = Color.red;

    [SerializeField] string focusTag = "Focus";
    [SerializeField] string noLapTag = "No Lapping";

    const float distance = Mathf.Infinity;

    private ObjectInteract objectInteract;

/// <summary>
/// 點擊任何一處時，偵測是否點擊到道具物件
/// </summary>
/// <param name="context"></param>
    private void FindClickable(InputAction.CallbackContext context)
    {
        UnselectAll();

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            hit.transform.tag = focusTag;
        }
        else
        {
            UnselectAll();
        }
        Debug.DrawRay(cam.transform.position, ray.direction * 100, RayColor, 0.5f);
    }

    /// <summary>
    /// 取消選取：移除任何物件的Focus Tag
    /// </summary>
    private void UnselectAll()
    {
        GameObject[] focusObjects = GameObject.FindGameObjectsWithTag(focusTag);
        foreach (GameObject obj in focusObjects)
        {
            obj.tag = noLapTag;
            foreach (Transform child in obj.transform)
            {
                child.tag = noLapTag;
            }
        }
    }
    private void Awake()
    {
        objectInteract = new ObjectInteract();
    }
    private void Start()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        objectInteract.ObjectTransform.select.performed += FindClickable;
        objectInteract.ObjectTransform.select.Enable();
    }


    private void OnDisable()
    {
        objectInteract.ObjectTransform.select.performed -= FindClickable;
        objectInteract.ObjectTransform.select.Disable();
    }
}
