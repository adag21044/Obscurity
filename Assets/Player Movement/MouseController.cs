using UnityEngine;

public class MouseController : MonoBehaviour
{
    private bool isMouseLocked = false; // Tracks if the cursor is locked

    void Update()
    {
        // Left mouse button click to lock and hide the cursor
        if (Input.GetMouseButtonDown(0))
        {
            LockCursor();
        }

        // Escape key to unlock and show the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center
        Cursor.visible = false; // Hide the cursor
        isMouseLocked = true;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Free the cursor
        Cursor.visible = true; // Show the cursor
        isMouseLocked = false;
    }
}
