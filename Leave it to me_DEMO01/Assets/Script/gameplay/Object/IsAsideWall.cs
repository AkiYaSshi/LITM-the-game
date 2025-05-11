using UnityEngine;

/// <summary>
/// Check if object is next to wall and add an is display component
/// </summary>
public class IsAsideWall : MonoBehaviour
{
    [SerializeField] string focusTag = "Focus";
    [SerializeField] LayerMask WallLayer;

    ObjectData objectData;
    GameObject target;

    /// <summary>
    /// Add is Display to object
    /// </summary>
    private void AddComponent()
    {
        IsDisplay isDisplay = gameObject.AddComponent<IsDisplay>();
    }

    private void FixedUpdate()
    {
        GetFocus();
        if ((target != null))
        {
            SelectionRaycast.StartRayCast(target, objectData);
            if (SelectionRaycast.hitx != null)
            {
                for (int i = 0; i < SelectionRaycast.hitx.Count; i++)
                {
                    if (SelectionRaycast.hitx[i].gameObject.layer == WallLayer)
                    {
                        Debug.Log("Aside a wall: " + SelectionRaycast.hitx[i].name);
                    }
                }
            }
            for(int i = 0; i < SelectionRaycast.hitz?.Count; i++)
            {
                if (SelectionRaycast.hitz[i].gameObject.layer == WallLayer)
                {
                    Debug.Log("Aside a wall: " + SelectionRaycast.hitx[i].name);
                }
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
