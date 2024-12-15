using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify KeyManager and pass the tag of this key
            KeyManager.Instance.CollectKey(gameObject.tag);

            // Destroy the key after collection
            Destroy(gameObject);
        }
    }
}
