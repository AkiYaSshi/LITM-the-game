using UnityEngine;

public class SaveExistingCheck : MonoBehaviour
{
    [Tooltip("�n���X���������O")]
    [SerializeField]
    private GameObject warning;

    public void CheckExisting(int number)
    {
        SaveInfoLoader.IsSaveExisting(number);
    }
}
