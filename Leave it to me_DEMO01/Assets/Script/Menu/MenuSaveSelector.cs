using UnityEngine;
using UnityEngine.UI;

public class MenuSaveSelector : MonoBehaviour
{
    /// <summary>
    /// �z�L�I�ssave file manager �]�w�s�ɽs��
    /// </summary>
    public void OnSaveSlotSelected(int slot)
    {
        SaveFileManager.SetSaveSlot(slot);
    }   
}
