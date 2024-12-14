using UnityEngine;

public class PassageTrigger : MonoBehaviour
{
    [SerializeField] private Collider targetCollider; // Target collider to activate/deactivate
    [SerializeField] private float reactivationDelay = 1f; // Time to reactivate collider after passage

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if it's the player
        {
            Debug.Log("Player entered the trigger area.");

            // Disable the target collider
            if (targetCollider != null)
            {
                targetCollider.enabled = false;
                Debug.Log("Collider disabled.");

                // Reactivate the collider after a delay
                Invoke(nameof(ReactivateCollider), reactivationDelay);
            }
        }
    }

    private void ReactivateCollider()
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = true;
            Debug.Log("Collider reactivated.");
        }
    }
}
