using UnityEngine;

/// <summary>
/// Represents a key that can be collected by the player.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Key : MonoBehaviour
{
    private void Reset()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify KeyManager and pass the tag of this key
            KeyManager.Instance.CollectKey(gameObject.tag);

            // Destroy the key after collection
            Destroy(gameObject);

            // Update the key icons UI
            UIManager.Instance.UpdateKeyIconsUI();

            // Display a message
            UIManager.Instance.DisplayMessage($"You collected a {gameObject.tag}!", 2f);
        }
    }
}
