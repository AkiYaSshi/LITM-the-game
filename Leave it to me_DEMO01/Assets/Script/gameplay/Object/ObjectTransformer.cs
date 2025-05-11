using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���󪺦첾�B����欰
/// </summary>
public class ObjectTransformer : MonoBehaviour
{
    private ObjectInteract objectInteract;
    private InputAction rotation;

    /// <summary>
    /// �o��ƹ���m��v��a�O���ƥ�
    /// </summary>
    public static event Func<Vector3> mousePosition;

    /// <summary>
    /// Ĳ�o����l�����檺�ƥ� <br />
    /// Vector3: ��l��m, ����ƫ��m
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
    /// ���ʿ������
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
    /// �T�O�I�����������S���ݰʲ{�H�A���o�ƹ��I���y�лP���󥻨��y�Ъ����t��
    /// </summary>
    /// <returns>���t��</returns>
    public Vector3 GetOffset()
    {
        lastPos = mousePosition.Invoke();
        return transform.position - lastPos;
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
        DecollideOthers.OverLap += AddOffset;
    }


    private void OnDisable()
    {
        rotation.performed -= Rotate;
        rotation.Disable();
        DecollideOthers.OverLap -= AddOffset;
    }

}
