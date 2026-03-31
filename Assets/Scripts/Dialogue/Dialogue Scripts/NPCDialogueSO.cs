using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogueSO", menuName = "Scriptable Objects/NPCDialogueSO")]
public class NPCDialogueSO : ScriptableObject
{
    public enum SpeakerType { NPC, Player }

    public SpeakerType speaker;

    public string characterName;
    public Sprite characterIcon;

    [TextArea(3, 5)]
    public string messageLines;
}