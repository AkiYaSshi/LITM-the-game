using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// 偵測是否選取到物件，若無則取消選取所有物件
/// </summary>
public class SelecctionManager : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] LayerMask unselectLayer, selectLayer;
    [SerializeField] Color RayColor = Color.red;

    [SerializeField] string focusTag = "Focus";
    [SerializeField] string noLapTag = "No Lapping";

    int  unselectIndex, selectIndex;

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

        if (Physics.Raycast(ray, out hit, distance, unselectLayer))
        {
            SwitchSelection(hit.transform.gameObject, focusTag, selectIndex);
        }
        else
        {
            UnselectAll();
        }
        Debug.DrawRay(cam.transform.position, ray.direction * 100, RayColor, 0.5f);
    }

    /// <summary>
    /// 當選擇狀態改變，改變物件的標籤及圖層
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="Tag"></param>
    /// <param name="LayerIndex"></param>
    private void SwitchSelection(GameObject obj, string Tag, int LayerIndex)
    {
        obj.tag = Tag;
        obj.layer = LayerIndex;
        //將物件內所有子物件圖層都設為selectLayer
        foreach (Transform transform in obj.transform)
        {
            GameObject child = transform.gameObject;
            child.layer = LayerIndex;
        }
    }

    /// <summary>
    /// 取消選取：移除任何物件的Focus Tag
    /// </summary>
    private void UnselectAll()
    {
        GameObject[] focusObjects = GameObject.FindGameObjectsWithTag(focusTag);
        foreach (GameObject obj in focusObjects)
        {
            SwitchSelection(obj, noLapTag, unselectIndex);
        }
    }

    /// <summary>
    /// Find Gameobject by layer
    /// </summary>
    /// <returns>game object</returns>
    private GameObject[] FindObjectByLayer(int targetLayer)
    {
        GameObject[] AllObject = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        List<GameObject> ObjectInLayer = new List<GameObject>();
        foreach (GameObject obj in AllObject)
        {
            if ((1 << obj.layer & targetLayer) != 0)
            {
                ObjectInLayer.Add(obj);
            }
        }
        if (ObjectInLayer.Count == 0) { return null; }
        return ObjectInLayer.ToArray();
    }

    private void Awake()
    {
        objectInteract = new ObjectInteract();
    }
    private void Start()
    {
        cam = Camera.main;
        unselectIndex = Mathf.RoundToInt(Mathf.Log(unselectLayer.value, 2));
        selectIndex = Mathf.RoundToInt(Mathf.Log(selectLayer.value, 2));
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
