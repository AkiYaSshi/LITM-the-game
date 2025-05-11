using UnityEngine;
/// <summary>
/// 從攝影機向滑鼠發射射線，計算游標對應特定面上的位置
/// </summary>
public class positionOnFloor : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCam;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask rayCastingLayer;

    [SerializeField]
    private const float rayCastingDistance = 20;
    [SerializeField]
    private const float objToCameraRange = 7;

    /// <summary>
    /// 滑鼠所在位置投影到地板上
    /// </summary>
    /// <returns>最終位置</returns>
    public Vector3 MouseToFloorPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCam.nearClipPlane;

        Ray ray = sceneCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayCastingDistance, rayCastingLayer))
        {
            lastPosition = hit.point;
            Debug.DrawRay(mousePos, hit.point*100, Color.cyan);
        }
        else 
        {
            Debug.DrawRay(mousePos, hit.point * 100, Color.magenta);
            lastPosition = sceneCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, sceneCam.nearClipPlane + objToCameraRange));
        }
        return lastPosition;
    }
     
    private void OnEnable()
    {
        ObjectTransformer.mousePosition += MouseToFloorPosition;
    }
    private void OnDisable()
    {
        ObjectTransformer.mousePosition -= MouseToFloorPosition;
    }
}
