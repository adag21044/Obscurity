using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static bool isMouseLocked = true; // Fare kilitli mi?
    private static bool isMovementLocked = false; // Hareket kilitli mi?

    public static void LockMouse(bool lockState)
    {
        isMouseLocked = lockState;
        Cursor.visible = !lockState; // Kilitlendiğinde fare görünmez
        Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public static void LockMovement(bool lockState)
    {
        isMovementLocked = lockState;
    }

    public static bool IsMouseLocked()
    {
        return isMouseLocked;
    }

    public static bool IsMovementLocked()
    {
        return isMovementLocked;
    }

    public static void LockAllInput(bool lockState)
    {
        LockMouse(lockState);
        LockMovement(lockState);
    }

    public static void ResetInputLocks()
    {
        // Varsayılan fare ve hareket kilidini geri döndür
        LockMouse(true);
        LockMovement(false);
    }
}
