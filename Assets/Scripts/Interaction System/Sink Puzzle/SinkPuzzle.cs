using UnityEngine;

public class SinkPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject[] toiletDoors;
    [SerializeField] private bool[] correctStates;

    public void CheckPuzzle()
    {
        for(int i = 0; i < toiletDoors.Length; i++)
        {
            DoorState doorState = toiletDoors[i].GetComponent<DoorState>();
            if (doorState == null || doorState.isOpen != correctStates[i])
            {
                Debug.Log("Puzzle not solved yet.");
                return;
            }
        }

        Debug.Log("Puzzle Solved!");
        OnPuzzleSolved();
    }

    private void OnPuzzleSolved()
    {
        // Logic to execute when the puzzle is solved
        // For example, you can unlock a door or trigger an event
        Debug.Log("Puzzle Solved! Triggering event...");
        // Add your event triggering logic here
    }
    
}


