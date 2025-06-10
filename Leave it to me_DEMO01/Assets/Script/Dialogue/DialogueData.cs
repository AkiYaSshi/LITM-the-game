using UnityEngine;

[System.Serializable]
public class DialogueData
{
    [Tooltip("NPC 頭像")]
    public Sprite npcIcon;
    [Tooltip("NPC 名稱")]
    public string npcName;
    [Tooltip("對話內容")]
    [TextArea] public string dialogueText; // 對話內容
}