using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HideListAfterButtonClick : MonoBehaviour
{
    public static event Action<string> ShowHidden;
    [SerializeField]
    private List<OnClickAnimation> onClickAnimation;
    [SerializeField]
    private bool alwaysShowTarget = false;

    [SerializeField]
    [Header("代號")]
    [Tooltip("隱藏清單內物件時，該目標的代號")]
    private string CallingText;


    [SerializeField]
    private GameObject target;

    public void OnButtonClick()
    {
        if (onClickAnimation != null)
        {
            foreach (var onClickAnimation in onClickAnimation)
            {
                onClickAnimation.AnimationStart();
            }
        }
        ShowHidden?.Invoke(CallingText);
        if (!alwaysShowTarget)
        {
            target.GetComponent<Canvas>().enabled = false;
        }
    }
}
