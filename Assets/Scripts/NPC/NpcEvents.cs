using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NpcEvents : Interactable
{
    // IM2073 Project
    [SerializeField]
    private string[] dialogueLines; // An array to hold the dialogue lines
    private int currentLine = 0; // Track the current line of dialogue
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;

    public GameManager gameManager;

    public UnityEvent onDialogueComplete; // Optional: An event to trigger when dialogue is complete

    // Override the Interact method from Interactable
    protected override void Interact()
    {
        if (dialogueLines.Length == 0) return; // If no dialogue, exit the method

        if (!dialoguePanel.activeInHierarchy)
        {
            dialoguePanel.SetActive(true); // Activate the panel when interaction starts
        }

        // Show the current line of dialogue
        ShowDialogue(dialogueLines[currentLine]);

        // Move to the next line for the next interaction
        currentLine++;

        // Check if we've reached the end of the dialogue
        if (currentLine >= dialogueLines.Length)
        {
            // Trigger an event when dialogue is complete
            onDialogueComplete.Invoke();

            // Reset dialogue to allow it to be repeated or to disable further interaction
            currentLine = 0; // Loop dialogue
            dialoguePanel.SetActive(false);

            gameManager.StartTimer();
        }
    }

    private void ShowDialogue(string line)
    {
        dialogueText.text = line;
        Debug.Log(line); // For testing, simply log the dialogue line. Replace this with your UI update logic.
    }
}
// End Code