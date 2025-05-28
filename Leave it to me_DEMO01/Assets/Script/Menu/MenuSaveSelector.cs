using UnityEngine;
using UnityEngine.UI;

public class MenuSaveSelector : MonoBehaviour
{
    [Tooltip("測試需要的場合，輸入這裡之後跳到gameplay即可存檔")]
    [SerializeField]
    private int inputSlot;
    /// <summary>
    /// 透過呼叫save file manager 設定存檔編號
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
