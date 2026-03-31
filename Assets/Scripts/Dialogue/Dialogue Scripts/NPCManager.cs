using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public List<NPCDialogueSO> beginnerDialogues;
    public List<NPCDialogueSO> advancedDialogues;
    public List<NPCDialogueSO> expertDialogues;

    public enum DialogueState { Beginner, Advanced, Expert }
    public DialogueState currentState;

    public List<NPCDialogueSO> GetCurrentDialogueList()
    {
        switch (currentState)
        {
            case DialogueState.Advanced:
                return advancedDialogues;
            case DialogueState.Expert:
                return expertDialogues;
            default:
                return beginnerDialogues;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.instance.StartDialogue(this);
        }
    }
}