using UnityEngine;

// Handles the player's movement using Rigidbody
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Movement speed
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 inputDirection)
    {
        // Calculate direction based on player's rotation
        Vector3 move = transform.right * inputDirection.x + transform.forward * inputDirection.y;

        // Move the player
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

    }
}
