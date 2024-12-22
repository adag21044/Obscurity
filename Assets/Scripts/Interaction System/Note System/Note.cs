using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private NoteSO noteData;          // Reference to note data
    [SerializeField] private GameObject noteUIPanel;   // Reference to note UI panel
    [SerializeField] private TMPro.TextMeshProUGUI noteUIText; // Text element to display note content

    private bool isOpen = false; // Tracks if the note is currently open

    // Returns the description of the note
    public string GetDescription()
    {
        return isOpen ? "Close the note" : "Press E to read the note";
    }

    // Handles interaction with the note
    public void Interact()
    {
        if (!isOpen)
        {
            OpenNote();
        }
        else
        {
            CloseNote();
        }
    }

    // Opens the note UI and displays its content
    private void OpenNote()
    {
        if (noteData != null && noteUIPanel != null && noteUIText != null)
        {
            noteUIText.text = noteData.content; // Set the note content in the UI
            noteUIPanel.SetActive(true);       // Show the UI panel
            isOpen = true;
        }
    }

    // Closes the note UI
    private void CloseNote()
    {
        if (noteUIPanel != null)
        {
            noteUIPanel.SetActive(false);      // Hide the UI panel
            isOpen = false;
        }
    }

    // Determines if the note can be interacted with
    public bool IsAvailable()
    {
        return true; // Always available
    }
}
