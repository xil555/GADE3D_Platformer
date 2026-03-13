using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public DialogueSetSO dialogueSet;
    public bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[DialogueTrigger] OnTriggerEnter with " + other.name);

        if (!other.CompareTag("Player"))
        {
            Debug.Log("[DialogueTrigger] Other is not Player (tag = " + other.tag + ")");
            return;
        }

        if (triggerOnce && hasTriggered)
        {
            Debug.Log("[DialogueTrigger] Already triggered once, ignoring");
            return;
        }

        if (dialogueManager == null)
        {
            Debug.LogWarning("[DialogueTrigger] dialogueManager NOT ASSIGNED");
            return;
        }

        Debug.Log("[DialogueTrigger] Starting dialogue");
        dialogueManager.StartDialogue(dialogueSet);
        hasTriggered = true;
    }
}