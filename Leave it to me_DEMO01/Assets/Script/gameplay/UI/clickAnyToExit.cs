using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class clickAnyToExit : MonoBehaviour
{
    [SerializeField] GameObject target;
    private Button btn;
    [SerializeField]
    private List<OnClickAnimation> onClickAnimation;
    [SerializeField]
    private bool alwaysShowTarget = false;
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    /// <summary>
    /// �I���ù����N�@�B���}
    /// </summary>
    void OnButtonClick()
    {
        Debug.Log("clicked on the anywhere");
        if (onClickAnimation != null)
        {
            foreach (var onClickAnimation in onClickAnimation)
            {
                onClickAnimation.AnimationStart();
            }
        }
        if(!alwaysShowTarget) target.GetComponent<Canvas>().enabled = false;
    }

}
