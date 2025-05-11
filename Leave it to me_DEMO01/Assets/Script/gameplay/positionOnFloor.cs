using UnityEngine;
/// <summary>
/// �q��v���V�ƹ��o�g�g�u�A�p���й����S�w���W����m
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
    /// �ƹ��Ҧb��m��v��a�O�W
    /// </summary>
    /// <returns>�̲צ�m</returns>
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
