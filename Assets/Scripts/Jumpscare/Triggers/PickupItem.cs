using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Item picked up");
            var action = GetComponent<PickupItemAction>();
            if (action != null)
            {
                action.OnPickup();
            }
            else
            {
                Debug.LogWarning("PickupItemAction component is missing!");
            }
        }
    }
}
