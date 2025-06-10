using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchIcon : MonoBehaviour
{
    [Tooltip("�i�������ϥܦC��")]
    [SerializeField]
    private List<Sprite> title;

    [Tooltip("�󴫹ϥܪ�����")]
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
