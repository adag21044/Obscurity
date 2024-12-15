using UnityEngine;

public class OpenDoorState : IDoorState
{
    public void Interact(Door door)
    {
        Debug.Log("The door is already open.");
    }
}
