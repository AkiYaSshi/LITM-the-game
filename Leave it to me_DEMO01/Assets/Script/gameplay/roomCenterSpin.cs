using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
using static iTween;
using System;

public class roomCenterSpin : MonoBehaviour
{
    [SerializeField] float animTime = 0.4f;
    Vector3 oneSpin;
    private void Start()
    {
        oneSpin = new Vector3(0, 90, 0);
    }
    void button_arrowRight_click()
    {
        iTween.RotateAdd(gameObject, -oneSpin, animTime);
    }
    void button_arrowLeft_click()
    {
        iTween.RotateAdd(gameObject, oneSpin, animTime);
    }

    void OnEnable()
    {
        gameplay_RoomShift.LeftbtnClick += button_arrowRight_click;
        gameplay_RoomShift.RightbtnClick += button_arrowLeft_click;
    }
    private void OnDisable()
    {
        gameplay_RoomShift.LeftbtnClick -= button_arrowRight_click;
        gameplay_RoomShift.RightbtnClick -= button_arrowLeft_click;
    }
}
