using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using static iTween;
using System;
using UnityEngine.UI;

/// <summary>
/// 使用左右兩按鈕控制房間旋轉
/// </summary>
public class gameplay_RoomShift : MonoBehaviour
{
    public static event Action LeftbtnClick;
    public static event Action RightbtnClick;

    //方向列舉變數
    [Serializable]
    enum DIRECTION
    {
        NORTH = 0,
        EAST = 1,
        SOUT = 2,
        WEST = 3
    }

    DIRECTION m_DIRECTION; //目前房間方向
    
    public void onClickLeftButton()
         => LeftbtnClick?.Invoke();
    
    public void onClickRightButton()
        => RightbtnClick?.Invoke();
}
