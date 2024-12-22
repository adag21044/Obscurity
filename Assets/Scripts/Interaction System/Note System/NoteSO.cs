using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Interaction/Note")]
public class NoteSO : ScriptableObject
{
    [TextArea] public string content; // The content of the note
}
