using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Note System/Note")]
public class NoteSO : ScriptableObject
{
    [TextArea] public string noteContent; // The content of the note
}
