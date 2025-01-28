using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Name of the item
    public int quantity;
    public Sprite sprite; // Icon of the item
    [TextArea]public string itemDescription; // Description of the item 
    
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

            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            
            if (leftOverItems <= 0)
                Destroy(gameObject);
            else
                quantity = leftOverItems;

        }
    }

}
