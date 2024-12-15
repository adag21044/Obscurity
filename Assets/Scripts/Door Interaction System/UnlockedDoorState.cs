using UnityEngine;

public class UnlockedDoorState : IDoorState
{
    public void Interact(Door door)
    {
        // Open the door and change state to opened
        Debug.Log("The door opens.");
        door.ChangeState(new OpenDoorState());
        door.OpenDoor();
    }
}
