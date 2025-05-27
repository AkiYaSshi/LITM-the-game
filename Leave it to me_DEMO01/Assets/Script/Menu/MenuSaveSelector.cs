using UnityEngine;
using UnityEngine.UI;

public class MenuSaveSelector : MonoBehaviour
{
    /// <summary>
    /// 透過呼叫save file manager 設定存檔編號
    /// </summary>
    public void OnSaveSlotSelected(int slot)
    {
        SaveFileManager.SetSaveSlot(slot);
    }   
}
