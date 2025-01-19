using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 GetMovementInput()
    {
        if (InputManager.IsMovementLocked()) return Vector2.zero; // Hareket kilitliyse giriş yapma
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public Vector2 GetMouseInput()
    {
        if (!InputManager.IsMouseLocked()) return Vector2.zero; // Fare kilitliyse giriş yapma
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
