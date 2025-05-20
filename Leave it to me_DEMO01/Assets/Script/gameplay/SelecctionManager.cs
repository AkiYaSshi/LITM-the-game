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
    #region 變數宣告
    public static event Action<GameObject> FocusChange;

    [Header("相機設置")]
    [Tooltip("用於射線檢測的主相機，若未設置則使用主相機")]
    [SerializeField] private Camera cam;

    [Header("圖層與標籤設置")]
    [Tooltip("未選取時的圖層，用於取消選取物件")]
    [SerializeField] private LayerMask unselectLayer;

    [Tooltip("選取時的圖層，用於標記選取物件")]
    [SerializeField] private LayerMask selectLayer;

    [Tooltip("未選取時的標籤名稱(不可重疊)")]
    [SerializeField] private string noLapTag = "No Lapping";

    [Tooltip("未選取時的標籤名稱(可重疊)")]
    [SerializeField] private string LapTag = "Lapping";

    [Tooltip("選取時的標籤名稱")]
    [SerializeField] private string focusTag = "Focus";

    [Header("除錯與視覺化")]
    [Tooltip("射線的顏色，用於除錯顯示")]
    [SerializeField] private Color RayColor = Color.red;

    // 以下為私有變數，不顯示於 Inspector
    private int unselectIndex, selectIndex;
    private const float distance = Mathf.Infinity;
    private ObjectInteract objectInteract;
    #endregion

    /// <summary>
    /// 點擊任何一處時，偵測是否點擊到道具物件
    /// </summary>
    /// <param name="context">輸入動作的上下文</param>
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
    /// <param name="obj">目標物件</param>
    /// <param name="Tag">要設置的標籤</param>
    /// <param name="LayerIndex">要設置的圖層索引</param>
    private void SwitchSelection(GameObject obj, string Tag, int LayerIndex)
    {
        obj.tag = Tag;
        obj.layer = LayerIndex;

        // 改變物件內所有子物件的圖層
        foreach (Transform transform in obj.transform)
        {
            GameObject child = transform.gameObject;
            child.layer = LayerIndex;

            // 若子物件沒有 MeshRenderer，判斷旗下還有子物件
            MeshRenderer meshRenderer;
            if (!child.TryGetComponent(out meshRenderer))
            {
                foreach (Transform childTransform2 in child.transform)
                {
                    GameObject child2 = childTransform2.gameObject;
                    child2.layer = LayerIndex;
                }
            }
        }
        FocusChange?.Invoke(obj);
    }

    /// <summary>
    /// 取消選取：移除任何物件的 Focus Tag
    /// </summary>
    private void UnselectAll()
    {
        GameObject[] focusObjects = GameObject.FindGameObjectsWithTag(focusTag);
        foreach (GameObject obj in focusObjects)
        {
            if(obj.GetComponent<ObjectRef>().objectData.noCollision)
                SwitchSelection(obj, LapTag, unselectIndex);
            else
                SwitchSelection(obj, noLapTag, unselectIndex);
        }
        FocusChange?.Invoke(null);
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