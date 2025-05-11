using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �P�_�i�H��m���~
/// </summary>
public class ObjectClickEvent : MonoBehaviour
{
    public static event Action OnClicked, OnExit;
    [SerializeField]
    private GameObject standard, cell;

    /// <summary>
    /// ����U�ƹ�����ɼs��OnClicked
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape)) 
            OnExit?.Invoke();
    }

    public bool IsClickable()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            return false;
        }
        
        return true;
    }

}
