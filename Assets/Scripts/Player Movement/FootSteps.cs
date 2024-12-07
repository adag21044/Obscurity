using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // AudioSource for footstep sounds
    [SerializeField] private float footstepDelay = 0.5f; // Delay between footstep sounds
    private float footstepTimer = 0f; // Timer to control footstep intervals

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        // Get required components
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        if (audioSource == null)
            Debug.LogError("AudioSource is not assigned to FootSteps script!");
    }

    private void Update()
    {
        // Get movement input
        Vector2 movementInput = playerInput.GetMovementInput();
        bool isMoving = movementInput.magnitude > 0.1f;

        // Handle footstep sound based on movement
        if (isMoving)
        {
            footstepTimer -= Time.deltaTime;

            if (!audioSource.isPlaying && footstepTimer <= 0f)
            {
                audioSource.Play();
                footstepTimer = footstepDelay; // Reset timer
            }
        }
        else
        {
            audioSource.Stop();
            footstepTimer = 0f; // Reset timer
        }
    }
}
