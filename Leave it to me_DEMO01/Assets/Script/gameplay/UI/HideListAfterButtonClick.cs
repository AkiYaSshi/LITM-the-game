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
        target.GetComponent<Canvas>().enabled = false;
    }
}
