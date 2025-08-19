using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/// <summary>
/// 物件的位移、旋轉行為
/// </summary>
public class ObjectTransformer : MonoBehaviour
{
    #region 變數宣告
    private ObjectInteract objectInteract;
    private InputAction rotation;
    private DetactCollision DetactCollision; //物件本身的DetactCollision
    private ObjectRef objectRef;

    /// <summary>
    /// 得到滑鼠位置投影到地板的事件
    /// </summary>
    public static event Func<bool, Vector3> mousePosition;

    /// <summary>
    /// 觸發物件吸附網格的事件 <br />
    /// Vector3: 原始位置, 網格化後位置
    /// </summary>
    public static event Func<Vector3, Vector3> DraggingObject;

    public static event Action<GameObject, Vector3> RotateAnimation;
    public static event Action DownObject;
    [SerializeField]
    private Camera mainCam;

    private float screenPosZ;
    private Vector3 PositionMouseToFloor, screenOffset;

    public const float animTime = 0.4f;
    readonly Vector3 oneSpin = new Vector3(0, 90, 0);
    Vector3 PositionSnapToGrid;
    #endregion

    private void OnMouseDown()
    {
        screenOffset = GetOffset();
    }
    /// <summary>
    /// 移動選取物件
    /// </summary>
    private void OnMouseDrag()
    {
        PositionMouseToFloor = mousePosition.Invoke(objectRef.objectData.noCollision);
        PositionMouseToFloor += screenOffset;

        PositionSnapToGrid = DraggingObject.Invoke(PositionMouseToFloor);
        bool isOverLap = DetactCollision.ObjectTransformer_CheckCollision(PositionSnapToGrid);
        if (!isOverLap)
        {
            //移動物件位置
            iTween.MoveUpdate(gameObject, PositionSnapToGrid, 0.2f);
        }
        
    }
    private void OnMouseUp()
    {
        DownObject.Invoke();
    }
    /// <summary>
    /// 物件旋轉
    /// </summary>
    /// <param name="context"></param>
    private void Rotate(InputAction.CallbackContext context)
    {
        if (!gameObject.CompareTag("Focus"))
        {
            return;
        }

        float direction = context.ReadValue<float>();

        // 計算旋轉角度
        Vector3 rotationAngle = oneSpin * direction; // direction 可為 -1 或 1
        Quaternion targetRotation = Quaternion.Euler(rotationAngle);

        // 檢查碰撞並旋轉
        if (!DetactCollision.ObjectTransformer_CheckCollision(gameObject.transform.position, targetRotation))
        {
            iTween.RotateAdd(gameObject, rotationAngle, animTime);
        }
        else
        {
            TransformerAnimation animation = gameObject.AddComponent<TransformerAnimation>();
            RotateAnimation?.Invoke(gameObject, rotationAngle);
        }
        InputSleep(animTime);
    }

    /// <summary>
    /// 確保點擊物件瞬間沒有抖動現象，取得滑鼠點擊座標與物件本身座標的偏差值
    /// </summary>
    /// <returns>偏差值</returns>
    public Vector3 GetOffset()
    {
        PositionMouseToFloor = mousePosition.Invoke(objectRef.objectData.noCollision);
        return transform.position - PositionMouseToFloor;
    }

    #region Input Sleep Functions
    private void InputSleep(float time)
    {
        rotation.Disable();
        Invoke("InputAwake", time + 0.01f);
    }
    void InputAwake()
    {
        rotation.Enable();
    }
    #endregion

    #region StartUp
    private void Awake()
    {
        objectInteract = new ObjectInteract();
    }
    private void Start()
    {
        mainCam = Camera.main;
        DetactCollision = gameObject.GetComponent<DetactCollision>();
        objectRef = gameObject.GetComponent<ObjectRef>();
    }

    private void OnEnable()
    {
        rotation = objectInteract.ObjectTransform.rotation;
        rotation.performed += Rotate;
        rotation.Enable();
    }
    private void OnDisable()
    {
        rotation.performed -= Rotate;
        rotation.Disable();
    }
    #endregion
}


/// <summary>
/// 執行物件的動畫
/// </summary>
public class TransformerAnimation : MonoBehaviour
{
    float animTime = ObjectTransformer.animTime;
    Vector3 originalRotation = new();


    private void PlayCollisionAnimation(GameObject target, Vector3 rotationAngle)
    {
        originalRotation = target.transform.rotation.eulerAngles;
        // 計算 0.2 倍的旋轉角度
        Vector3 partialRotationAngle = rotationAngle * 0.2f;
        Quaternion partialRotation = Quaternion.Euler(partialRotationAngle);

        // 第一步：旋轉到 0.2 倍角度
        iTween.RotateAdd(target, iTween.Hash(
            "amount", partialRotationAngle,
            "time", animTime * 0.5f, // 縮短動畫時間
            "easetype", iTween.EaseType.easeInOutQuad,
            "oncomplete", "ReturnToOriginalRotation", // 完成後調用返回動畫
            "oncompletetarget", target
        ));
        Invoke("DeleteComponent", animTime);
    }

    private void DeleteComponent()
    {
        TransformerAnimation.Destroy(this);
    }

    private void ReturnToOriginalRotation()
    {
        // 第二步：旋轉回原始角度
        iTween.RotateTo(gameObject, iTween.Hash(
            "rotation", originalRotation,
            "time", animTime * 0.5f,
            "easetype", iTween.EaseType.easeInOutQuad
        ));
    }
    private void OnEnable()
    {
        ObjectTransformer.RotateAnimation += PlayCollisionAnimation;
    }
    private void OnDisable()
    {
        ObjectTransformer.RotateAnimation -= PlayCollisionAnimation;
    }
}

