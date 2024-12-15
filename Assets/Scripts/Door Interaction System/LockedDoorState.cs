using UnityEngine;

public class LockedDoorState : IDoorState
{
    public void Interact(Door door)
    {
        Debug.Log("The door is locked. Find the correct key.");
    }
}
