using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueSet", menuName = "Dialogue/Dialogue Set")]
public class DialogueSetSO : ScriptableObject
{
    public DialogueSO[] dialogueItems;
}
