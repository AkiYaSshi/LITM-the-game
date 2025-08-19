using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/// <summary>
/// ���󪺦첾�B����欰
/// </summary>
public class ObjectTransformer : MonoBehaviour
{
    #region �ܼƫŧi
    private ObjectInteract objectInteract;
    private InputAction rotation;
    private DetactCollision DetactCollision; //���󥻨���DetactCollision
    private ObjectRef objectRef;

    /// <summary>
    /// �o��ƹ���m��v��a�O���ƥ�
    /// </summary>
    public static event Func<bool, Vector3> mousePosition;

    /// <summary>
    /// Ĳ�o����l�����檺�ƥ� <br />
    /// Vector3: ��l��m, ����ƫ��m
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
    /// ���ʿ������
    /// </summary>
    private void OnMouseDrag()
    {
        PositionMouseToFloor = mousePosition.Invoke(objectRef.objectData.noCollision);
        PositionMouseToFloor += screenOffset;

        PositionSnapToGrid = DraggingObject.Invoke(PositionMouseToFloor);
        bool isOverLap = DetactCollision.ObjectTransformer_CheckCollision(PositionSnapToGrid);
        if (!isOverLap)
        {
            //���ʪ����m
            iTween.MoveUpdate(gameObject, PositionSnapToGrid, 0.2f);
        }
        
    }
    private void OnMouseUp()
    {
        DownObject.Invoke();
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="context"></param>
    private void Rotate(InputAction.CallbackContext context)
    {
        if (!gameObject.CompareTag("Focus"))
        {
            return;
        }

        float direction = context.ReadValue<float>();

        // �p����ਤ��
        Vector3 rotationAngle = oneSpin * direction; // direction �i�� -1 �� 1
        Quaternion targetRotation = Quaternion.Euler(rotationAngle);

        // �ˬd�I���ñ���
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
    /// �T�O�I�����������S���ݰʲ{�H�A���o�ƹ��I���y�лP���󥻨��y�Ъ����t��
    /// </summary>
    /// <returns>���t��</returns>
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
/// ���檫�󪺰ʵe
/// </summary>
public class TransformerAnimation : MonoBehaviour
{
    float animTime = ObjectTransformer.animTime;
    Vector3 originalRotation = new();


    private void PlayCollisionAnimation(GameObject target, Vector3 rotationAngle)
    {
        originalRotation = target.transform.rotation.eulerAngles;
        // �p�� 0.2 �������ਤ��
        Vector3 partialRotationAngle = rotationAngle * 0.2f;
        Quaternion partialRotation = Quaternion.Euler(partialRotationAngle);

        // �Ĥ@�B�G����� 0.2 ������
        iTween.RotateAdd(target, iTween.Hash(
            "amount", partialRotationAngle,
            "time", animTime * 0.5f, // �Y�u�ʵe�ɶ�
            "easetype", iTween.EaseType.easeInOutQuad,
            "oncomplete", "ReturnToOriginalRotation", // ������եΪ�^�ʵe
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
        // �ĤG�B�G����^��l����
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

