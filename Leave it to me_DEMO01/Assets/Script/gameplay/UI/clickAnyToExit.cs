using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class clickAnyToExit : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private Button btn;
    [SerializeField]
    private List<OnClickAnimation> onClickAnimation;
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
        panel.GetComponent<Canvas>().enabled = false;
    }

}
