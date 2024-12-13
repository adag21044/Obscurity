using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    [SerializeField] private NoteSO noteData; // Reference to the ScriptableObject containing note data

    // Returns the note content
    public string GetNoteContent()
    {
        return noteData != null ? noteData.noteContent : "No content available.";
    }
}
