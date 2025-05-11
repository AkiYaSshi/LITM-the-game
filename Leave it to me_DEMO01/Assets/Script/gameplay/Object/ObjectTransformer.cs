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

    /// <summary>
    /// �o��ƹ���m��v��a�O���ƥ�
    /// </summary>
    public static event Func<Vector3> mousePosition;

    /// <summary>
    /// ����Vector3�y�ФW���L�I����<br />Vector3: �����y��<br />bool: �I���������G
    /// </summary>
    public static event Func<Vector3, bool> CheckCollision;

    /// <summary>
    /// Ĳ�o����l�����檺�ƥ� <br />
    /// Vector3: ��l��m, ����ƫ��m
    /// </summary>
    public static event Func<Vector3, Vector3> DraggingObject;

    [SerializeField]
    private Camera mainCam;

    private float screenPosZ;
    private Vector3 PositionMouseToFloor, screenOffset;

    [SerializeField] float animTime = 0.4f;
    Vector3 oneSpin = new Vector3(0, 90, 0);
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
        PositionMouseToFloor = mousePosition.Invoke();
        PositionMouseToFloor += screenOffset;

        PositionSnapToGrid = DraggingObject.Invoke(PositionMouseToFloor);
        CheckCollision?.Invoke(PositionSnapToGrid);

        //���ʪ����m
        iTween.MoveUpdate(gameObject, PositionSnapToGrid, 0.1f);
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="context"></param>
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
    /// �T�O�I�����������S���ݰʲ{�H�A���o�ƹ��I���y�лP���󥻨��y�Ъ����t��
    /// </summary>
    /// <returns>���t��</returns>
    public Vector3 GetOffset()
    {
        PositionMouseToFloor = mousePosition.Invoke();
        return transform.position - PositionMouseToFloor;
    }


    /// <summary>
    /// ���ܤ�����P��L�����|�AY�b��Offset���W�@���
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
    }


    private void OnDisable()
    {
        rotation.performed -= Rotate;
        rotation.Disable();
    }

}
public class DetactCollision: MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    private void OnEnable()
    {
        ObjectTransformer.CheckCollision += ObjectTransformer_CheckCollision;
    }

    private bool ObjectTransformer_CheckCollision(Vector3 pos)
    {

        throw new NotImplementedException();
    }
}
