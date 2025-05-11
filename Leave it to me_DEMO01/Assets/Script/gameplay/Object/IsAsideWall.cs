using UnityEngine;

/// <summary>
/// Check if object is next to wall and add an is display component
/// </summary>
public class IsAsideWall : MonoBehaviour
{
    [SerializeField] string focusTag = "Focus";
    [SerializeField] 
    private LayerMask WallLayer;

    ObjectData objectData;
    GameObject target;

    private void FixedUpdate()
    {
        GetFocus();
        if ((target != null))
        {
            SelectionRaycast.StartRayCast(target, objectData);
            DetactHit(SelectionRaycast.hitx);
            DetactHit(SelectionRaycast.hitz);

        }

        //Detact if focus hit walls
        void DetactHit(System.Collections.Generic.List<Transform> _hitArray)
        {
            for (int i = 0; i < _hitArray?.Count; i++)
            {
                //Debug.Log("Aside a wall: " + _hitArray[i].name);
                //target.GetComponent<IsDisplay>().enabled = (((1 << _hitArray[i].gameObject.layer) & WallLayer) != 0);
            }
        }
    }

    /// <summary>
    /// get object data from focus
    /// </summary>
    private void GetFocus()
    {
        target = GameObject.FindGameObjectWithTag(focusTag);
        objectData = target?.GetComponent<ObjectRef>().objectData;
    }
}
