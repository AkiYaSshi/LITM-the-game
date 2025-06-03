using UnityEngine;

public class SaveExistingCheck : MonoBehaviour
{
    [Tooltip("要跳出的提醒面板")]
    [SerializeField]
    private GameObject warning;

    public void CheckExisting(int number)
    {
        SaveInfoLoader.IsSaveExisting(number);
    }
}
