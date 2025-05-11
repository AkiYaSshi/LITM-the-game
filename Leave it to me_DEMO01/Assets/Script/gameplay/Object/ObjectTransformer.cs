using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 物件的位移、旋轉行為
/// </summary>
public class ObjectTransformer : MonoBehaviour
{
    private ObjectInteract objectInteract;
    private InputAction rotation;

    /// <summary>
    /// 得到滑鼠位置投影到地板的事件
    /// </summary>
    public static event Func<Vector3> mousePosition;

    /// <summary>
    /// 觸發物件吸附網格的事件 <br />
    /// Vector3: 原始位置, 網格化後位置
    /// </summary>
    public static event Func<Vector3, Vector3> DraggingObject;

    [SerializeField]
    private Camera mainCam;

    private float screenPosZ;
    private Vector3 lastPos, screenOffset;

    [SerializeField] float animTime = 0.4f;
    Vector3 oneSpin = new Vector3(0, 90, 0);
    Vector3 mouseToObject;

    private void OnMouseDown()
    {
        screenOffset = GetOffset();
    }
    /// <summary>
    /// 移動選取物件
    /// </summary>
    private void OnMouseDrag()
    {
        lastPos = mousePosition.Invoke();
        lastPos += screenOffset;

        mouseToObject = DraggingObject.Invoke(lastPos);
        iTween.MoveUpdate(gameObject, mouseToObject, 0.1f);
    }

    private void Rotate(InputAction.CallbackContext context)
    {
        if(gameObject.tag != "Focus")
        {
            return;
        }
        string _dir = rotation.ReadValueAsObject()?.ToString();

        if(_dir == "-1")
        {
        iTween.RotateAdd(gameObject, oneSpin, animTime);
        }
        if (_dir == "1")
        {
            iTween.RotateAdd(gameObject, -oneSpin, animTime);
        }
    }

    /// <summary>
    /// 確保點擊物件瞬間沒有抖動現象，取得滑鼠點擊座標與物件本身座標的偏差值
    /// </summary>
    /// <returns>偏差值</returns>
    public Vector3 GetOffset()
    {
        lastPos = mousePosition.Invoke();
        return transform.position - lastPos;
    }
    /// <summary>
    /// 當選擇中物件與其他物件重疊，Y軸的Offset往上一單位
    /// </summary>
    private void AddOffset()
    {
        screenOffset.y += GridMovement.unit;
    }
    private void Awake()
    {
        objectInteract = new ObjectInteract();
    }
    private void Start()
    {
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        rotation = objectInteract.ObjectTransform.rotation;
        rotation.performed += Rotate;
        rotation.Enable();
        DecollideOthers.OverLap += AddOffset;
    }


    private void OnDisable()
    {
        rotation.performed -= Rotate;
        rotation.Disable();
        DecollideOthers.OverLap -= AddOffset;
    }

}
