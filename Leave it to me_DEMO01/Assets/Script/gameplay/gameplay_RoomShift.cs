using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using static iTween;
using System;
using UnityEngine.UI;

/// <summary>
/// �ϥΥ��k����s����ж�����
/// </summary>
public class gameplay_RoomShift : MonoBehaviour
{
    public static event Action LeftbtnClick;
    public static event Action RightbtnClick;

    //��V�C�|�ܼ�
    [Serializable]
    enum DIRECTION
    {
        NORTH = 0,
        EAST = 1,
        SOUT = 2,
        WEST = 3
    }

    DIRECTION m_DIRECTION; //�ثe�ж���V
    
    public void onClickLeftButton()
         => LeftbtnClick?.Invoke();
    
    public void onClickRightButton()
        => RightbtnClick?.Invoke();
}
