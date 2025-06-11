using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [Header("輸入組合")]
    [SerializeField]
    List<MenuGroup> menuGroups = new List<MenuGroup>();

    private void selectObject(GameObject selectFirst)
    {
        EventSystem.current.SetSelectedGameObject(selectFirst);
        Debug.Log($"assign select to{selectFirst}.");
    }
    private void OpenSettingMenu(InputAction.CallbackContext context)
    {
        selectObject(menuGroups[0].SelectFirst);
    }
    private void OpenItemMenu(InputAction.CallbackContext context)
    {
        selectObject(menuGroups[1].SelectFirst);
    }
    private void OpenToolMenu(InputAction.CallbackContext context)
    {
        selectObject(menuGroups[2].SelectFirst);
    }
    private void OnEnable()
    {
        foreach (var group in menuGroups)
        {
            group.trigger?.Enable();
        }
        menuGroups[0].trigger.performed += OpenSettingMenu;
        menuGroups[1].trigger.performed += OpenItemMenu;
    }
    private void OnDisable()
    {
        foreach (var group in menuGroups)
        {
            group.trigger?.Disable();
        }
        menuGroups[0].trigger.performed -= OpenSettingMenu;
        menuGroups[1].trigger.performed -= OpenItemMenu;
    }
}

[Serializable]
public class MenuGroup
{
    [field:SerializeField] public int index { get; private set; }

    [Header("輸入按鍵")]
    [field: SerializeField]
    public InputAction trigger {  get; set; }

    [Header("預設選取物件")]
    [field: SerializeField]
    public GameObject SelectFirst{  get; private set; }
}
