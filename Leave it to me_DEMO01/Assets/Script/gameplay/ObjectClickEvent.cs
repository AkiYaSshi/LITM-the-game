using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 判斷可以放置物品
/// </summary>
public class ObjectClickEvent : MonoBehaviour
{
    public static event Action OnClicked, OnExit;
    [SerializeField]
    private GameObject standard, cell;

    /// <summary>
    /// 當按下滑鼠左鍵時廣播OnClicked
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
