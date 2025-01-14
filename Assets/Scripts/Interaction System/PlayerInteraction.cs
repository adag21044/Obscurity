using TMPro;
using UnityEngine;

// Handles player interactions with objects
public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCam;                          // Reference to the player's camera
    public float interactionDistance = 2f;          // Maximum distance to interact with objects
    public GameObject interactionUI;                // UI element for interaction prompt
    public TextMeshProUGUI interactionText;         // Text field for interaction description

    private IInteractable currentInteractable;      // Currently detected interactable object

    private void Update()
    {
        DetectInteraction();                        // Check for interactable objects
        HandleInput();                              // Handle player input for interactions
    }

    // Casts a ray to detect interactable objects
    void DetectInteraction()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f); // Cast ray from screen center
        RaycastHit hit;

        currentInteractable = null;                // Reset current interactable
        interactionUI.SetActive(false);            // Hide interaction UI

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null && interactable.IsAvailable())
            {
                // Set the current interactable and update the UI
                currentInteractable = interactable;
                interactionText.text = interactable.GetDescription();
                interactionUI.SetActive(true);
            }
        }
    }

    // Handles player input for interacting with objects
    void HandleInput()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable is IPuzzle puzzle)
            {
                puzzle.Interact(); // Puzzle etkileşimi
            }
            else
            {
                currentInteractable.Interact(); // Diğer etkileşim
            }
        }
    }

}
