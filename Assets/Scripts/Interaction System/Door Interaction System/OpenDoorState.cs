using UnityEngine;

/// <summary>
/// Represents the state of a door when it is already open.
/// </summary>
public class OpenDoorState : IDoorState
{
    public void Interact(Door door)
    {
        Debug.Log("The door is already open.");
        UIManager.Instance.DisplayMessage("The door is already open.", 2f);
    }
}
