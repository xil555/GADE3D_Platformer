using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterMessage;
    public Image characterIcon;

    public float characterDelay = 0.05f;
    public float punctuationDelay = 0.3f;

    public int maxWords = 50;

    public bool moveNext;
    public GameObject moveNextButt;

    public GameObject dialoguePanel;

    private CustomQueue dialogueQueue = new CustomQueue();

    private void Awake()
    {
        instance = this;
    }

    public void MoveNext()
    {
        moveNext = true;
    }

    // START dialogue
    public void StartDialogue(NPCManager npcManager)
    {
        dialogueQueue.Clear();

        var list = npcManager.GetCurrentDialogueList();

        foreach (var dialogue in list)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        StartCoroutine(ProcessQueue());
    }

    IEnumerator ProcessQueue()
    {
        dialoguePanel.SetActive(true);
   

        while (!dialogueQueue.IsEmpty())
        {
            NPCDialogueSO currentDialogue = dialogueQueue.Dequeue();

            yield return StartCoroutine(ShowDialogueCoroutine(currentDialogue));
        }

        dialoguePanel.SetActive(false);
    }

    IEnumerator ShowDialogueCoroutine(NPCDialogueSO dialogue)
    {
        characterName.text = dialogue.characterName;
        characterIcon.sprite = dialogue.characterIcon;

        characterMessage.text = "";

        foreach (char character in dialogue.messageLines)
        {
            characterMessage.text += character;

            yield return new WaitForSeconds(
                IsPunctuation(character) ? punctuationDelay : characterDelay
            );

            if (characterMessage.textInfo.wordCount >= maxWords &&
                (character == ' ' || IsPunctuation(character) || character == ','))
            {
                moveNextButt.SetActive(true);
                yield return new WaitUntil(() => moveNext);

                characterMessage.text = "";
                moveNextButt.SetActive(false);
                moveNext = false;
            }
        }

        moveNextButt.SetActive(true);
        yield return new WaitUntil(() => moveNext);

        characterMessage.text = "";
        moveNextButt.SetActive(false);
        moveNext = false;
    }

    bool IsPunctuation(char character)
    {
        return character == '.' || character == '?' || character == '!';
    }
}