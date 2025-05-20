using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// �����O�_����쪫��A�Y�L�h��������Ҧ�����
/// </summary>
public class SelecctionManager : MonoBehaviour
{
    #region �ܼƫŧi
    public static event Action<GameObject> FocusChange;

    [Header("�۾��]�m")]
    [Tooltip("�Ω�g�u�˴����D�۾��A�Y���]�m�h�ϥΥD�۾�")]
    [SerializeField] private Camera cam;

    [Header("�ϼh�P���ҳ]�m")]
    [Tooltip("������ɪ��ϼh�A�Ω�����������")]
    [SerializeField] private LayerMask unselectLayer;

    [Tooltip("����ɪ��ϼh�A�Ω�аO�������")]
    [SerializeField] private LayerMask selectLayer;

    [Tooltip("������ɪ����ҦW��(���i���|)")]
    [SerializeField] private string noLapTag = "No Lapping";

    [Tooltip("������ɪ����ҦW��(�i���|)")]
    [SerializeField] private string LapTag = "Lapping";

    [Tooltip("����ɪ����ҦW��")]
    [SerializeField] private string focusTag = "Focus";

    [Header("�����P��ı��")]
    [Tooltip("�g�u���C��A�Ω󰣿����")]
    [SerializeField] private Color RayColor = Color.red;

    // �H�U���p���ܼơA����ܩ� Inspector
    private int unselectIndex, selectIndex;
    private const float distance = Mathf.Infinity;
    private ObjectInteract objectInteract;
    #endregion

    /// <summary>
    /// �I������@�B�ɡA�����O�_�I����D�㪫��
    /// </summary>
    /// <param name="context">��J�ʧ@���W�U��</param>
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
    /// ���ܪ��A���ܡA���ܪ��󪺼��Ҥιϼh
    /// </summary>
    /// <param name="obj">�ؼЪ���</param>
    /// <param name="Tag">�n�]�m������</param>
    /// <param name="LayerIndex">�n�]�m���ϼh����</param>
    private void SwitchSelection(GameObject obj, string Tag, int LayerIndex)
    {
        obj.tag = Tag;
        obj.layer = LayerIndex;

        // ���ܪ��󤺩Ҧ��l���󪺹ϼh
        foreach (Transform transform in obj.transform)
        {
            GameObject child = transform.gameObject;
            child.layer = LayerIndex;

            // �Y�l����S�� MeshRenderer�A�P�_�X�U�٦��l����
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
    /// ��������G�������󪫥� Focus Tag
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