using System.Collections;
using UnityEngine;

/// <summary>
/// Periodically checks three DoorState components; when the pattern
/// (open • closed • open) is matched, triggers a door to open.
/// </summary>
public class WatchAndOpen : MonoBehaviour
{
    [Header("Puzzle Data")]
    [SerializeField] private DoorState[] doorStates;   // Expecting size == 3
    [SerializeField] private OpenAfterPuzzleSolved doorToOpen;   // Door that should open

    [Header("Polling")]
    [SerializeField] private float checkInterval = 5f;

    private bool isPuzzleSolved;

    private void Start() => StartCoroutine(CheckPuzzleCoroutine());

    private IEnumerator CheckPuzzleCoroutine()
    {
        while (!isPuzzleSolved)
        {
            EvaluatePuzzle();
            yield return new WaitForSeconds(checkInterval);
        }
    }

    private void EvaluatePuzzle()
    {
        if (doorStates.Length < 3)
        {
            Debug.LogError("WatchAndOpen requires exactly 3 DoorState references.");
            return;
        }

        isPuzzleSolved = doorStates[0].isOpen && !doorStates[1].isOpen && doorStates[2].isOpen;

        if (isPuzzleSolved)
        {
            Debug.Log("Puzzle solved! Opening the door…");
            doorToOpen.TriggerOpen();          // ✔️ instance-based call
        }
    }
}
