using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName; // Name of the item
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite; // Icon of the item
    
    private InventoryManager inventoryManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager is not found!");
                return;
            }

            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
        }
    }

}
