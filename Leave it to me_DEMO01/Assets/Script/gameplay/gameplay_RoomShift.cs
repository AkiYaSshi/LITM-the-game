using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using static iTween;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

/// <summary>
/// �ϥΥ��k����s����ж�����
/// </summary>
public class gameplay_RoomShift : MonoBehaviour
{
    ObjectInteract inputActions;
    InputAction Turn;

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
    private void CompareAxis(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() > 0.8f)
        {
            RightbtnClick?.Invoke();
            Turn.Disable();
            Invoke("EnableButton", 0.5f);
        }
        if(context.ReadValue<float>() < -0.8f)
        {
            LeftbtnClick?.Invoke();
            Turn.Disable();
            Invoke("EnableButton", 0.5f);
        }
    }
    void EnableButton()
    {
        Turn.Enable();
    }

    private void Awake()
    {
        inputActions = new ObjectInteract();
    }
    private void OnEnable()
    {
        Turn = inputActions.ObjectTransform.roomspin;
        Turn.performed += CompareAxis;
        Turn.Enable();
    }
    private void OnDisable()
    {
        Turn.performed -= CompareAxis;
        Turn.Disable();
    }

}
