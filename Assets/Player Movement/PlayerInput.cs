using UnityEngine;

// Handles all player inputs (movement, mouse look)
public class PlayerInput : MonoBehaviour
{
    public Vector2 GetMovementInput()
    {
        // Returns the movement vector from input
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public Vector2 GetMouseInput()
    {
        // Returns the mouse input for rotation
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
