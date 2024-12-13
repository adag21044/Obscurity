using UnityEngine;
using TMPro;

public class NoteUIController : MonoBehaviour
{
    private bool isNoteOpen = false; // Tracks if the note is currently open
    [SerializeField] private GameObject noteCanvas; // Reference to the note UI canvas
    [SerializeField] private TMP_Text noteText; // Text component to display note content

    public static NoteUIController instance;

    private void Awake()
    {
        // Implement Singleton pattern for global access
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        // Close the note when 'Q' is pressed
        if (isNoteOpen && Input.GetKeyDown(KeyCode.Q))
        {
            CloseNote();
        }
    }

    // Displays the note content on the canvas
    public void DisplayNote(string noteContent)
    {
        noteText.text = noteContent;
        noteCanvas.SetActive(true);
        isNoteOpen = true;
    }

    // Closes the note UI
    public void CloseNote()
    {
        noteCanvas.SetActive(false);
        isNoteOpen = false;
    }
}
