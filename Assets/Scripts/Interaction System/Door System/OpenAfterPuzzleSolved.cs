using System.Collections;
using UnityEngine;

/// <summary>
/// Opens (or closes) the door with a smooth rotation animation.
/// Requires an attached <see cref="DoorState"/> component.
/// </summary>
[RequireComponent(typeof(DoorState))]
public class OpenAfterPuzzleSolved : MonoBehaviour
{
    /* ──────── AUDIO ──────── */
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip   doorOpenSound;

    /* ──────── ANIMATION ──────── */
    [Header("Animation")]
    [Tooltip("Degrees to rotate around global Y axis when opening.")]
    [SerializeField] private float openAngle        = 90f;
    [SerializeField] private float rotationDuration = 1f;

    /* ──────── RUNTIME STATE ──────── */
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen;
    private bool isAnimating;

    private DoorState doorState;

    private void Awake()
    {
        doorState      = GetComponent<DoorState>();
        closedRotation = transform.rotation;
        openRotation   = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        // Sync with initial DoorState (designer can set it in Inspector)
        isOpen = doorState.isOpen;
        if (isOpen) transform.rotation = openRotation;
    }

    /// <summary>Called by external scripts when the puzzle is solved.</summary>
    public void TriggerOpen()
    {
        if (isAnimating) return;
        StartCoroutine(RotateDoor());
    }

    private IEnumerator RotateDoor()
    {
        if (audioSource && doorOpenSound)
            audioSource.PlayOneShot(doorOpenSound);

        isAnimating = true;

        Quaternion startRot  = transform.rotation;
        Quaternion targetRot = isOpen ? closedRotation : openRotation;
        float      elapsed   = 0f;

        while (elapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRot;
        isOpen             = !isOpen;
        doorState.isOpen   =  isOpen;
        isAnimating        = false;
    }
}
