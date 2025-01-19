using UnityEngine;

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
        // Eğer input kilitliyse hareketi durdur
        if (InputManager.IsMovementLocked()) return;

        Vector2 movementInput = playerInput.GetMovementInput();
        Vector2 mouseInput = playerInput.GetMouseInput();

        cameraController.HandleMouseLook(mouseInput); // Fare hareketi
        playerMovement.Move(movementInput); // WASD hareketi
    }
}
