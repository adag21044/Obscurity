using UnityEngine;

// Handles the camera's vertical and horizontal rotations
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerBody; // Reference to the player's body
    [SerializeField] private float lookSensitivity = 2f;

    private float rotationX = 0f;

    public void HandleMouseLook(Vector2 mouseInput)
    {
        float mouseX = mouseInput.x * lookSensitivity;
        float mouseY = mouseInput.y * lookSensitivity;

        // Clamp vertical rotation
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply rotation
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

        
    }
}
