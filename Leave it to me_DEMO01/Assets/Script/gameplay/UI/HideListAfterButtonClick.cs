using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HideListAfterButtonClick : MonoBehaviour
{
    public static event Action ShowHidden;
    [SerializeField]
    private List<OnClickAnimation> onClickAnimation;
    [SerializeField]
    private bool alwaysShowTarget = false;

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
        ShowHidden?.Invoke();
        if (!alwaysShowTarget)
        {
            target.GetComponent<Canvas>().enabled = false;
        }
    }
}
