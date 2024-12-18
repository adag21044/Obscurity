using UnityEngine;

/// <summary>
/// Base class for all interactable objects in the game.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    /// <summary>
    /// Enum representing different types of interactions.
    /// </summary>
    public enum InteractionType
    {
        None,
        Pickup,
        Use,
        Examine,
        Talk,
        Open,
        Close,
        Activate,
        Deactivate,
        Drop,
        Combine,
        Destroy,
        Read,
        Push,
        Pull,
        Rotate,
        Inspect,
        Enter,
        Exit,
        Trade,
        Unlock,
        Lock,
        Collect,
        Heal,
        Equip,
        Unequip,
        Save,
        Load,
        Interact,
        Toggle,
        Click
    }

    [Tooltip("Type of interaction this object supports.")]
    public InteractionType interactionType = InteractionType.Interact;

    /// <summary>
    /// Provides a description of the interaction to display to the player.
    /// </summary>
    /// <returns>A string describing the interaction.</returns>
    public abstract string GetDescription();

    /// <summary>
    /// Defines what happens when the player interacts with this object.
    /// </summary>
    public abstract void Interact();
}
