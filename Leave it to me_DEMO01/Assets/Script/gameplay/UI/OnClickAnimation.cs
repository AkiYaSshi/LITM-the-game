using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [Tooltip("用於計算位移量的圖層")]
    [SerializeField]
    private Canvas canvas;

    [Tooltip("用於基準顯示的參考物件（例如顯示的標準位置）")]
    [SerializeField]
    private GameObject ShowObjStandard;
    #endregion

    // 其他私有變數（不會出現在 Inspector 中）
    private Space space;
    private Vector3 LastTransform;
    private Vector3 BackDirection;
    private bool reverse = false;
    private bool isUI;
    private CanvasScaler scaler;

    /// <summary>
    /// 啟動動畫執行
    /// </summary>
    public void AnimationStart()
    {
        if (!reverse)
        {
            Animation();
            reverse = true;
            return;
        }
        else //設定相反方向的改變
        {
            Animation(-1);
            reverse = false;
        }
    }

    private void Animation(int dire = 1)
    {
        foreach (GameObject item in Targets)
        {
            if (!isUI) LastTransform = Object3DMovement(item) * dire; //非UI物件
            else if (isUI) LastTransform = UIMovement(item) * dire; //UI物件
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
    }

    #region 座標換算
    /// <summary>
    /// 將移動方向轉換為螢幕座標
    /// </summary>
    private Vector3 Object3DMovement(GameObject _obj)
    {
        if (moveInScreenSpace && _obj.layer != 5)
        {
            Vector3 screenPosition = cam.WorldToScreenPoint(_obj.transform.position); //物件世界座標轉到螢幕座標

            Vector3 targetScreenPosition = screenPosition + Direction; //在螢幕視角位移
            targetScreenPosition.x = screenPosition.x + (Direction.x / 1920) * Screen.width;
            targetScreenPosition.y = screenPosition.y + (Direction.y / 1080) * Screen.height;

            return cam.ScreenToWorldPoint(targetScreenPosition) - _obj.transform.position; //將移動後位置減去當前座標，轉為世界向量
        }
        return Direction;
    }
    /// <summary>
    /// UI的對螢幕位移計算
    /// </summary>
    /// <param name="_obj"></param>
    /// <returns></returns>
    private Vector3 UIMovement(GameObject _obj)
    {
        Vector2 refResolution = scaler.referenceResolution;
        float scaleFactor = scaler.scaleFactor;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // 將相對位移量（比例）轉換為 Canvas 單位
        float displacementX = Direction.x / refResolution.x * screenSize.x;
        float displacementY = Direction.y / refResolution.y * screenSize.y;
        float displacementZ = Direction.z;

        return new Vector3(displacementX, displacementY, displacementZ);
    }
    #endregion

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
        if(canvas != null)
        {
            isUI = true;
            scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler == null || scaler.referenceResolution == Vector2.zero)
            {
                Debug.LogError("CanvasScaler 或 Reference Resolution 未正確設置！");
                return;
            }
        }
        space = UseGlobalPosition ? Space.World : Space.Self;
    }

}