using UnityEngine;

/// <summary>
/// Represents the state of a door when it is unlocked.
/// </summary>
public class UnlockedDoorState : IDoorState
{
    public void Interact(Door door)
    {
        Debug.Log("The door opens.");
        door.ChangeState(new OpenDoorState());
        door.StartCoroutine(door.OpenDoorGradually());
    }
}
