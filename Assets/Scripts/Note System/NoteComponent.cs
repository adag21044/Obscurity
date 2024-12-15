using TMPro;
using UnityEngine;

public class NoteComponent : MonoBehaviour
{
    [SerializeField] private TextMeshPro noteText;  
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

    public void UpdateTextFromData()
    {
        if (noteText != null)
        {
            noteText.text = GetNoteContent();
            Debug.Log("Updated Note Text: " + noteText.text);
        }
        else
        {
            Debug.LogError("TextMeshPro reference is null in NoteComponent!");
        }
    }



    public void UpdateText(string value){
        noteText.text = value;
    }
}
