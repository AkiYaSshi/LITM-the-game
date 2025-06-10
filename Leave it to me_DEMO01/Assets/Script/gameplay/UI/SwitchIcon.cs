using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIcon : MonoBehaviour
{
    [Tooltip("可切換的圖示列表")]
    [SerializeField]
    private List<Sprite> title;

    [Tooltip("更換圖示的物件")]
    [SerializeField]
    private GameObject target;

    public void SetIcon(int index)
    {
        target.GetComponent<Image>().sprite = title[index];
    }

    private void OnEnable()
    {
        TaskFocusManager.setFocus += SetIcon;
    }
    private void OnDisable()
    {
        TaskFocusManager.setFocus -= SetIcon;
    }
}
