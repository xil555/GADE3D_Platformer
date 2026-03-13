using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;
    public Image iconImage;
    public GameObject dialoguePanel;

    private DialogueSetSO currentSet;
    private Queue<DialogueSO> dialogueQueue = new Queue<DialogueSO>(50);

    private void Start()
    {
        Debug.Log("[DialogueManager] Start");

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
            Debug.Log("[DialogueManager] Hiding panel on start");
        }
        else
        {
            Debug.LogWarning("[DialogueManager] dialoguePanel NOT ASSIGNED");
        }
    }

    public void StartDialogue(DialogueSetSO set)
    {
        Debug.Log("[DialogueManager] StartDialogue called");

        if (set == null)
        {
            Debug.LogWarning("[DialogueManager] StartDialogue: set is NULL");
            return;
        }

        if (set.dialogueItems == null || set.dialogueItems.Length == 0)
        {
            Debug.LogWarning("[DialogueManager] StartDialogue: set has no items");
            return;
        }
        Debug.Log("[DialogueManager] StartDialogue actually running");
        currentSet = set;
        dialogueQueue.Clear();

        foreach (DialogueSO item in currentSet.dialogueItems)
        {
            dialogueQueue.Enqueue(item);
        }

        Debug.Log("[DialogueManager] Enqueued " + dialogueQueue.Count + " items");

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
            Debug.Log("[DialogueManager] Panel set active");
        }

        DisplayNextDialogue();
    }

    public void OnNextButtonPressed()
    {
        Debug.Log("[DialogueManager] Next button pressed");
        DisplayNextDialogue();
    }

    private void DisplayNextDialogue()
    {
        Debug.Log("[DialogueManager] DisplayNextDialogue. Count = " + dialogueQueue.Count);

        if (dialogueQueue.Count == 0)
        {
            Debug.Log("[DialogueManager] No more dialogue, hiding panel");
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
            return;
        }

        DialogueSO item = dialogueQueue.Dequeue();
        Debug.Log("[DialogueManager] Showing line: " + item.message);

        if (dialoguePanel != null && !dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(true);
            Debug.Log("[DialogueManager] Panel re-activated");
        }

        if (nameText != null) nameText.text = item.characterName;
        if (messageText != null) messageText.text = item.message;
        if (iconImage != null) iconImage.sprite = item.characterIcon;
    }
}