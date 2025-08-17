using System.Collections;
using UnityEngine;
using static iTween;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField]
    [Header("作為選單遮罩的物件")]
    GameObject mask;
    [SerializeField]
    [Header("選單初始長度")]
    float oriSize;
    [SerializeField]
    [Header("動畫漸變類型")]
    EaseType easeType;

    [SerializeField]
    [Header("選單預設顯示")]
    bool menuIsShow;

    Hashtable show = new();
    Hashtable hide = new();

    private RectTransform maskSize;

    private float min;

    private void Start()
    {
        maskSize = mask.GetComponent<RectTransform>();

        min = GetSizeMin();

        SetHash();
    }

    public void OnClick()
    {
        min = GetSizeMin();
        Debug.Log($"按鈕按下，選單{menuIsShow}");
        if (menuIsShow)
        {
            iTween.ValueTo(gameObject, hide);
            menuIsShow = false;
        }
        else
        {
            iTween.ValueTo(gameObject, show);
            menuIsShow = true;
        }
    }
    public void ChangeMaskLength(float maskButton)
    {
        maskSize.offsetMax = new Vector2(maskSize.offsetMax.x, maskButton);
    }

    private void SetHash()
    {
        show.Add("from", min);
        show.Add("to", -oriSize);
        show.Add("time", 0.3f);
        show.Add("easetype", easeType);
        show.Add("onupdate", "ChangeMaskLength");

        hide.Add("from", -oriSize);
        hide.Add("to", min);
        hide.Add("time", 0.3f);
        hide.Add("easetype", easeType);
        hide.Add("onupdate", "ChangeMaskLength");
    }

    /// <summary>
    /// 由parent的height與遮罩的bottom相加
    /// </summary>
    /// <returns></returns>
    private float GetSizeMin()
    {
        RectTransform parentRect = mask.transform.parent.GetComponent<RectTransform>();
        float targetMin = -parentRect.sizeDelta.y;

        return targetMin;
    }
}
