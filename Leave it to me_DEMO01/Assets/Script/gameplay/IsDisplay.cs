using UnityEngine;
using static iTween;

/// <summary>
/// 依據物件射出的法線，判斷物件是否朝著螢幕<br />
/// 沒有則隱藏
/// </summary>
public class IsDisplay : MonoBehaviour
{
    /// <summary>
    /// 物件正面面朝方向
    /// </summary>
    public enum FowardAxis
    {
        X_POSITIVE, X_NEGATIVE, Y_POSITIVE, Y_NEGATIVE, Z_POSITIVE, Z_NEGATIVE
    }

    public FowardAxis fowardAxis;

    [SerializeField]
    float maxDistance = 30f;

    [SerializeField]
    private LayerMask targetLayer = 1 << 6;

    Vector3 direction;
    Vector3 delta;

    private OriginalSize originalSize;

    [SerializeField]
    Color rayColor = new Color();

    float animTime = 0.4f;

    void Start()
    {
        originalSize = gameObject.AddComponent<OriginalSize>();
        originalSize.GetLocalScale();
        assignDirection();
        raycastDetact();
    }

    /// <summary>
    /// 從物件中心點沿direction方向打出雷射<br />
    /// 若有打中對應圖層內的collider則隱藏物件
    /// </summary>
    void raycastDetact()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, targetLayer))
        {
            iTween.ScaleTo(gameObject, new Vector3(0, 0, 0), animTime);
        }
        else
        {
            iTween.ScaleTo(gameObject, originalSize.Size, animTime);
            Debug.DrawRay(transform.position, direction * maxDistance, rayColor, 2f);
        }
    }

    /// <summary>
    /// 向右旋轉時，法線也預先向右旋轉
    /// </summary>
    void RayDireAdjustRight()
    {
        direction = Quaternion.AngleAxis(90, Vector3.up) * direction;
    }

    /// <summary>
    /// 向左旋轉時，法線也預先向左旋轉
    /// </summary>
    void RayDireAdjustLeft() 
    {
        direction = Quaternion.AngleAxis(-90, Vector3.up) * direction;
    }

    /// <summary>
    /// 依據從unity傳入的Foward Axis類型決定射線向前方位
    /// </summary>
    void assignDirection()
    {
        switch (fowardAxis)
        {
            case FowardAxis.X_POSITIVE:
                direction = transform.right;
                break;
            case FowardAxis.X_NEGATIVE:
                direction = -transform.right;
                break;
            case FowardAxis.Y_POSITIVE:
                direction = transform.up;
                break;
            case FowardAxis.Y_NEGATIVE:
                direction = -transform.up;
                break;
            case FowardAxis.Z_POSITIVE:
                direction = transform.forward;
                break;
            case FowardAxis.Z_NEGATIVE:
                direction = -transform.forward;
                break;
        }

    }
    private void OnEnable()
    {
        gameplay_RoomShift.LeftbtnClick += assignDirection;
        gameplay_RoomShift.RightbtnClick += assignDirection;
        gameplay_RoomShift.LeftbtnClick += RayDireAdjustLeft;
        gameplay_RoomShift.RightbtnClick += RayDireAdjustRight;
        RoomData.LoadRoom += assignDirection;
        RoomData.LoadRoom += raycastDetact;
        gameplay_RoomShift.LeftbtnClick += raycastDetact;
        gameplay_RoomShift.RightbtnClick += raycastDetact;
    }
    private void OnDisable()
    {
        gameplay_RoomShift.LeftbtnClick -= assignDirection;
        gameplay_RoomShift.RightbtnClick -= assignDirection;
        gameplay_RoomShift.LeftbtnClick -= RayDireAdjustLeft;
        gameplay_RoomShift.RightbtnClick += RayDireAdjustRight;
        RoomData.LoadRoom -= assignDirection;
        RoomData.LoadRoom -= raycastDetact;
        gameplay_RoomShift.LeftbtnClick -= raycastDetact;
        gameplay_RoomShift.RightbtnClick -= raycastDetact;

    }
}

/// <summary>
/// 在遊戲開始儲存物件的原始大小
/// </summary>
public class OriginalSize: MonoBehaviour
{
    public Vector3 Size;

    /// <summary>
    /// 儲存物件原始大小
    /// </summary>
    public void GetLocalScale()
    {
        Size = transform.localScale;
    }
}
