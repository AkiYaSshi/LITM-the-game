using System;
using UnityEngine;

/// <summary>
/// ���ۦ�component������I���L����.tag == Focus�A�NFocus���W�H�קK�����|
/// </summary>
public class DecollideOthers : MonoBehaviour
{
    public static event Action OverLap;

    [SerializeField]
    private Grid grid;
    private void Awake()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.CompareTag("Focus"))
        {
            OverLap?.Invoke();
            GridMovement.MoveUP(other.transform.parent.gameObject);
        }
    }
}
