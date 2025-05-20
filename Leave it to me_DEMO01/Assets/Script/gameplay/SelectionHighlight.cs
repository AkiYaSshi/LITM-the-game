using Unity.VisualScripting;
using UnityEngine;

public class SelectionHighlight : MonoBehaviour
{
    [SerializeField]
    private const string focusTag = "Focus";
    MaterialPropertyBlock propertyBlock;

    private void Start(){
        if(propertyBlock == null){
            propertyBlock = new();
        }
    }
    private void Highlight(GameObject target){
        Renderer renderer = target.GetComponent<Renderer>();
    }
}
