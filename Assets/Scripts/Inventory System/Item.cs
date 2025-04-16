using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName; // Name of the item
    public int quantity;
    public Sprite sprite; // Icon of the item
    [TextArea]public string itemDescription; // Description of the item 
    
    private InventoryManager inventoryManager;
    public ItemSO itemSO;

    private bool canInteract = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (itemSO == null)
        {
            Debug.LogError($"[ERROR] {itemName} için ItemSO atanmadı! Inspector'dan kontrol et.");
        }
        
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }


    public void Interact()
    {
        if (!canInteract || inventoryManager == null)
        {
            return;
        }

        int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

        if (leftOverItems <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    }

    public string GetDescription()
    {
        return "Press E to pick up " + itemName;
    }
}
