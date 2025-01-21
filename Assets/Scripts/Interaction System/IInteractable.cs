using UnityEngine;

// Interface for all interactable objects
public interface IInteractable
{
    void Interact();              // Defines the interaction behavior
    string GetDescription();      // Returns the interaction description
    bool isCollectable();         // Returns whether the object can be collected
}
