using UnityEngine;

[CreateAssetMenu(menuName = "Interactable")]
public class InteractableSO : ScriptableObject
{
    public string description; // The description displayed to the player

    public string GetDescription => description;
}