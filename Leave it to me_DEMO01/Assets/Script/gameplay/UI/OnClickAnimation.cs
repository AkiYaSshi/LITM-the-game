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
    /// <summary>
    /// 動畫化的目標物件陣列
    /// </summary>
    [SerializeField]
    private GameObject[] Targets;

    /// <summary>
    /// 動畫類型列舉
    /// </summary>
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

    /// <summary>
    /// 當前選擇的動畫類型
    /// </summary>
    [SerializeField]
    private AnimationType animationType;

    /// <summary>
    /// 動畫的漸變類型
    /// </summary>
    [SerializeField]
    private iTween.EaseType BetweenType;
    private Space space;

    /// <summary>
    /// 動畫的方向和改變量
    /// </summary>
    [SerializeField]
    private Vector3 Direction;
    private Vector3 LastTransform;
    private Vector3 BackDirection;

    /// <summary>
    /// 動畫的執行時間
    /// </summary>
    [SerializeField]
    private float AnimationTime;

    /// <summary>
    /// 動畫延遲開始時間
    /// </summary>
    [SerializeField]
    private float Delay = 0;

    /// <summary>
    /// 是否基於螢幕座標計算移動
    /// </summary>
    [SerializeField]
    private bool moveInScreenSpace = true;
    [SerializeField]
    private bool UseGlobalPosition = true;
    bool reverse = false;

    /// <summary>
    /// 攝影機參考
    /// </summary>
    private Camera cam;
    [SerializeField]
    private GameObject ShowObjStandard;
    #endregion
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