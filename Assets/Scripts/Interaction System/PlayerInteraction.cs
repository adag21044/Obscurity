using TMPro;
using UnityEngine;

/// <summary>
/// Handles player interactions with interactable objects.
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("Maximum distance at which the player can interact with objects.")]
    [SerializeField] private float interactionDistance = 3f;

    [Tooltip("UI Text element to display interaction prompts.")]
    [SerializeField] private TextMeshProUGUI interactionText;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("PlayerInteraction: Main camera not found.");
        }

        if (interactionText == null)
        {
            Debug.LogError("PlayerInteraction: Interaction Text is not assigned.");
        }
    }

    private void Update()
    {
        HandleInteractionDetection();
    }

    /// <summary>
    /// Detects interactable objects and updates the UI accordingly.
    /// </summary>
    private void HandleInteractionDetection()
    {
        if (mainCamera == null || interactionText == null)
            return;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;

        bool successfulHit = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                // Display interaction prompt
                interactionText.text = interactable.GetDescription();
                successfulHit = true;

                // Handle interaction input
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }

        if (!successfulHit)
        {
            interactionText.text = string.Empty;
        }
    }
}
