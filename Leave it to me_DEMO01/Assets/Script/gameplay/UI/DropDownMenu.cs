using System.Collections;
using UnityEngine;
using static iTween;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField]
    [Header("�@�����B�n������")]
    GameObject mask;
    [SerializeField]
    [Header("����l����")]
    float oriSize;
    [SerializeField]
    [Header("�ʵe��������")]
    EaseType easeType;

    [SerializeField]
    [Header("���w�]���")]
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
        Debug.Log($"���s���U�A���{menuIsShow}");
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
    /// ��parent��height�P�B�n��bottom�ۥ[
    /// </summary>
    /// <returns></returns>
    private float GetSizeMin()
    {
        RectTransform parentRect = mask.transform.parent.GetComponent<RectTransform>();
        float targetMin = -parentRect.sizeDelta.y;

        return targetMin;
    }
}
