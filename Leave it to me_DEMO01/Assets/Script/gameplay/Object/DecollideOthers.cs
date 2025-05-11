using System;
using UnityEngine;

/// <summary>
/// 掛著此component的物件碰到其他物件.tag == Focus，將Focus往上以避免物件重疊
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
