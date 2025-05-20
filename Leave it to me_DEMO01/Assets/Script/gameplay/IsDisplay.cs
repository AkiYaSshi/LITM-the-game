using UnityEngine;
using static iTween;

/// <summary>
/// ??????g?X???k?u?A?P?_????O?_???ˆ{?<br />
/// ?S???h????
/// </summary>
public class IsDisplay : MonoBehaviour
{
    /// <summary>
    /// ??????????V
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
    /// ?q??????I?udirection??V???X?p?g<br />
    /// ?Y????????????h????collider?h???ˆ§???
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
    /// ?V?k????ˆ¨A?k?u?]?w???V?k????
    /// </summary>
    void RayDireAdjustRight()
    {
        direction = Quaternion.AngleAxis(90, Vector3.up) * direction;
    }

    /// <summary>
    /// ?V??????ˆ¨A?k?u?]?w???V??????
    /// </summary>
    void RayDireAdjustLeft() 
    {
        direction = Quaternion.AngleAxis(-90, Vector3.up) * direction;
    }

    /// <summary>
    /// ???qunity??J??Foward Axis?????M?w?g?u?V?e???
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
/// ?b?C???}?l?x?s?????l?j?p
/// </summary>
public class OriginalSize: MonoBehaviour
{
    public Vector3 Size;

    /// <summary>
    /// ?x?s?????l?j?p
    /// </summary>
    public void GetLocalScale()
    {
        Size = transform.localScale;
    }
}
