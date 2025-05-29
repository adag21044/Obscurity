using System.Collections;
using UnityEngine;

/// <summary>
/// Door interaction component. Rotates the door around its Y axis by <see cref="openAngle"/> degrees.
/// Implements IInteractable so it can be triggered by a generic interaction system.
/// </summary>
[RequireComponent(typeof(DoorState))]
public class SpDoorOpener : MonoBehaviour, IInteractable
{
    /* ──────── AUDIO ──────── */
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;     // Source that will play the SFX
    [SerializeField] private AudioClip   doorOpenSound;   // Clip to play while toggling the door

    /* ──────── ANIMATION ──────── */
    [Header("Animation")]
    [Tooltip("Degrees to rotate around the global Y axis when the door opens.")]
    [SerializeField] private float openAngle        = 90f;
    [Tooltip("Time (seconds) for the rotation animation.")]
    [SerializeField] private float rotationDuration = 1f;

    /* ──────── RUNTIME STATE ──────── */
    private Quaternion closedRotation;   // Calculated in Awake – NOT serialized
    private Quaternion openRotation;     // Calculated in Awake – NOT serialized
    private bool       isOpen;           // Mirrors DoorState.isOpen
    private bool       isAnimating;

    private DoorState doorState;

    /* ──────── LIFECYCLE ──────── */
    private void Awake()
    {
        doorState      = GetComponent<DoorState>();

        // Define reference rotations
        closedRotation = transform.rotation;
        openRotation   = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        // Sync with serialized DoorState so designers can choose initial state
        isOpen = doorState.isOpen;
        if (isOpen)
        {
            transform.rotation = openRotation;
        }
    }

    /* ──────── INTERACTION ──────── */
    public void Interact()
    {
        if (isAnimating) return;  // Prevent overlapping coroutines
        StartCoroutine(RotateDoor());
    }

    private IEnumerator RotateDoor()
    {
        // Play SFX (if assigned)
        if (audioSource && doorOpenSound)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }

        isAnimating = true;

        Quaternion startRot   = transform.rotation;
        Quaternion targetRot  = isOpen ? closedRotation : openRotation;
        float      elapsed    = 0f;

        while (elapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRot;

        // Toggle state flags
        isOpen            = !isOpen;
        doorState.isOpen  =  isOpen;
        isAnimating       = false;
    }

    /* ──────── UI HELPER ──────── */
    public string GetDescription() => "Press E to toggle the door.";
}
