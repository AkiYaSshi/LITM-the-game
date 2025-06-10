using UnityEngine;

[System.Serializable]
public class DialogueData
{
    [Tooltip("NPC �Y��")]
    public Sprite npcIcon;
    [Tooltip("NPC �W��")]
    public string npcName;
    [Tooltip("��ܤ��e")]
    [TextArea] public string dialogueText; // ��ܤ��e
}