using UnityEngine;

// Coordinates PlayerInput, PlayerMovement, and CameraController
public class FPSCharacter : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private CameraController cameraController;

    [SerializeField] private Transform cameraTransform;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        cameraController = cameraTransform.GetComponent<CameraController>();
    }

    void Update()
    {
        // Get inputs
        Vector2 movementInput = playerInput.GetMovementInput();
        Vector2 mouseInput = playerInput.GetMouseInput();

        // Handle camera and movement
        cameraController.HandleMouseLook(mouseInput);
        playerMovement.Move(movementInput);
    }
}
