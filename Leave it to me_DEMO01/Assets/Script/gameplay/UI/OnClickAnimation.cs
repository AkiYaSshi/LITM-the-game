using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

/// <summary>
/// 處理按鈕點擊後的動畫效果
/// </summary>
public class OnClickAnimation : MonoBehaviour
{
    #region 變數宣告
    [Header("動畫目標")]
    [Tooltip("選擇要應用動畫的目標物件陣列")]
    [SerializeField]
    private GameObject[] Targets;

    [Header("動畫類型與設置")]
    [Tooltip("選擇動畫的類型：滑動或縮放")]
    [SerializeField]
    private AnimationType animationType;

    public enum AnimationType
    {
        /// <summary>
        /// 滑動動畫
        /// </summary>
        SLIDE,
        /// <summary>
        /// 縮放動畫
        /// </summary>
        SCALE
    }

    [Tooltip("選擇動畫的漸變類型（例如線性、緩入緩出等）")]
    [SerializeField]
    private iTween.EaseType BetweenType;

    [Header("動畫參數")]
    [Tooltip("動畫的方向和改變量（例如滑動距離或縮放比例）")]
    [SerializeField]
    private Vector3 Direction;

    [Tooltip("動畫的執行時間（單位：秒）")]
    [SerializeField]
    private float AnimationTime;

    [Tooltip("動畫開始前的延遲時間（單位：秒）")]
    [SerializeField]
    private float Delay = 0;

    [Header("座標與空間")]
    [Tooltip("是否基於螢幕座標計算移動（而不是世界座標）")]
    [SerializeField]
    private bool moveInScreenSpace = true;

    [Tooltip("是否使用全局位置（影響座標計算方式）")]
    [SerializeField]
    private bool UseGlobalPosition = true;

    [Header("攝影機與參考物件")]
    [Tooltip("用於計算螢幕座標的攝影機（若為空則使用主攝影機）")]
    [SerializeField]
    private Camera cam;

    [Tooltip("用於基準顯示的參考物件（例如顯示的標準位置）")]
    [SerializeField]
    private GameObject ShowObjStandard;
    #endregion

    // 其他私有變數（不會出現在 Inspector 中）
    private Space space;
    private Vector3 LastTransform;
    private Vector3 BackDirection;
    private bool reverse = false;

    /// <summary>
    /// 啟動動畫執行
    /// </summary>
    public void AnimationStart()
    {
        if (!reverse)
        {
            foreach (GameObject item in Targets)
            {
                LastTransform = ToScreenMovement(item);
                Hashtable hashtable = SetHash();
                switch (animationType)
                {
                    case AnimationType.SLIDE:
                        iTween.MoveBy(item, hashtable);
                        break;
                    case AnimationType.SCALE:
                        iTween.ScaleBy(item, hashtable);
                        break;
                }
            }
            reverse = true;
            return;
        }
        else //設定相反方向的改變
        {

            foreach (GameObject item in Targets)
            {
                LastTransform = -ToScreenMovement(item);
                Hashtable hashtable = SetHash();
                switch (animationType)
                {
                    case AnimationType.SLIDE:
                        iTween.MoveBy(item, hashtable);
                        break;
                    case AnimationType.SCALE:
                        iTween.ScaleBy(item, hashtable);
                        break;
                }
            }
            reverse = false;
        }
    }

    /// <summary>
    /// 將移動方向轉換為螢幕座標
    /// </summary>
    private Vector3 ToScreenMovement(GameObject _obj)
    {
        if (moveInScreenSpace && _obj.layer != 5)
        {
            Vector3 screenPosition = cam.WorldToScreenPoint(_obj.transform.position); //物件世界座標轉到螢幕座標

            Vector3 targetScreenPosition = screenPosition + Direction; //在螢幕視角位移

            return cam.ScreenToWorldPoint(targetScreenPosition) - _obj.transform.position; //將移動後位置減去當前座標，轉為世界向量
        }
        return Direction;
    }

    /// <summary>
    /// 設置 iTween 的 Hash 表參數
    /// </summary>
    /// <returns>返回配置好的 Hash 表</returns>
    private Hashtable SetHash()
    {
        return new()
        {
            { "amount", LastTransform },
            { "time", AnimationTime },
            { "delay", Delay },
            { "easetype", BetweenType },
            {"space", space}
        };
    }
    /// <summary>
    /// 初始化時獲取主攝影機
    /// </summary>
    private void Start()
    {
        cam = Camera.main;
        space = UseGlobalPosition ? Space.World : Space.Self;
    }

}