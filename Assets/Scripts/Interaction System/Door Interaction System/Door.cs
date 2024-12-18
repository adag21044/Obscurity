using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a door that can be locked and unlocked with a key.
/// </summary>
public class Door : Interactable
{
    [Header("Door Settings")]
    [Tooltip("Tag of the key required to unlock the door.")]
    [SerializeField] private string requiredKeyTag;

    [Tooltip("Pivot point around which the door rotates.")]
    [SerializeField] private Transform doorPivot;

    [Tooltip("Audio source to play when the door opens.")]
    [SerializeField] private AudioSource audioSource;

    private IDoorState currentState;
    private bool isOpened = false;

    private void Start()
    {
        currentState = new LockedDoorState();
    }

    /// <summary>
    /// Provides a description of the door's current state.
    /// </summary>
    /// <returns>A string describing the door's state.</returns>
    public override string GetDescription()
    {
        switch (currentState)
        {
            case LockedDoorState _:
                return "Press [E] - The door is locked";
            case UnlockedDoorState _ when !isOpened:
                return "Press [E] - Open the door";
            case OpenDoorState _:
                return "The door is open";
            default:
                return "Press [E] to interact with the door";
        }
    }

    /// <summary>
    /// Handles interaction with the door based on its current state.
    /// </summary>
    public override void Interact()
    {
        if (KeyManager.Instance.HasKey(requiredKeyTag))
        {
            UnlockDoor();
        }
        else
        {
            currentState.Interact(this);
            UIManager.Instance.DisplayMessage("The door is locked. Find the right key!", 2f);
        }
    }

    /// <summary>
    /// Unlocks the door and changes its state.
    /// </summary>
    public void UnlockDoor()
    {
        Debug.Log($"Door unlocked with key: {requiredKeyTag}");
        UIManager.Instance.DisplayMessage("You unlocked the door!", 2f);
        ChangeState(new UnlockedDoorState());
        StartCoroutine(OpenDoorGradually());
    }

    /// <summary>
    /// Coroutine to open the door gradually over time.
    /// </summary>
    /// <returns>IEnumerator for the coroutine.</returns>
    public IEnumerator OpenDoorGradually()
    {
        if (isOpened)
            yield break;

        isOpened = true;
        Quaternion initialRotation = doorPivot.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, -145, 0); // Adjust rotation as needed
        float duration = 2f; // Duration of the opening animation
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            doorPivot.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / duration);
            yield return null;
        }

        doorPivot.localRotation = targetRotation;

        Debug.Log("The door opens.");
        UIManager.Instance.DisplayMessage("The door opens.", 2f);

        audioSource?.Play();
    }

    /// <summary>
    /// Changes the door's state.
    /// </summary>
    /// <param name="newState">The new state to transition to.</param>
    public void ChangeState(IDoorState newState)
    {
        currentState = newState;
        Debug.Log($"Door state changed to: {newState.GetType().Name}");
    }

    /// <summary>
    /// Gets the tag of the key required to unlock the door.
    /// </summary>
    /// <returns>The required key's tag.</returns>
    public string GetRequiredKeyTag()
    {
        return requiredKeyTag;
    }
}
