using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    [SerializeField] private NoteSO noteData; // Reference to the ScriptableObject containing note data
    [SerializeField] private bool hasSpecialEffect = false; // Flag for special effects (e.g., scream)

    public string GetNoteContent()
    {
        return noteData != null ? noteData.noteContent : "No content available.";
    }

    public bool HasSpecialEffect()
    {
        return hasSpecialEffect;
    }
}
