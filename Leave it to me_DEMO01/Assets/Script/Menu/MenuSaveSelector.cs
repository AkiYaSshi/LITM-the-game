using UnityEngine;
using UnityEngine.UI;

public class MenuSaveSelector : MonoBehaviour
{
    [Tooltip("���ջݭn�����X�A��J�o�̤������gameplay�Y�i�s��")]
    [SerializeField]
    private int inputSlot;
    /// <summary>
    /// �z�L�I�ssave file manager �]�w�s�ɽs��
    /// </summary>
    public void OnSaveSlotSelected(int slot)
    {
        SaveFileManager.SetSaveSlot(slot);
    }   
    public void InpuSetSaveSlot()
    {
        SaveFileManager.SetSaveSlot(inputSlot);
    }
}
