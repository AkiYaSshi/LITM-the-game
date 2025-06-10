using UnityEngine;
using UnityEngine.UI;

public class AllTaskComp : MonoBehaviour
{
    private Button Button;
    private void Awake()
    {
        Button = gameObject.GetComponent<Button>();
        Button.interactable = false;
    }
    void ToInteractable()
    {
        Button.interactable = true;
    }
    private void OnEnable()
    {
        TaskManager.AllComplete += ToInteractable;
    }
    private void OnDisable()
    {
        TaskManager.AllComplete -= ToInteractable;
    }
}
