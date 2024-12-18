using UnityEngine;

/// <summary>
/// Represents the state of a door when it is locked.
/// </summary>
public class LockedDoorState : IDoorState
{
    public void Interact(Door door)
    {
        Debug.Log("The door is locked. Find the correct key.");
        UIManager.Instance.DisplayMessage("The door is locked. Find the right key!", 2f);
    }
}
