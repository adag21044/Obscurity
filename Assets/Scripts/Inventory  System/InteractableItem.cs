using UnityEngine;

// This items can be collected and added to the inventory
public class InteractableItem : MonoBehaviour, IInteractable
{
    public InteractableSO itemData; // Scriptable Object verisi

    public void Interact()
    {
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        if (inventory == null)
        {
            Debug.LogError("InventoryManager bulunamadı!");
            return;
        }

        if (itemData == null || itemData.GetItemIcon == null)
        {
            Debug.LogError("itemData veya itemIcon eksik!");
            return;
        }

        Debug.Log($"Interacted with: {itemData.GetDescription}");
        inventory.AddItem(itemData.GetItemIcon); // Eşyayı envantere ekle
        Debug.Log("Eşya envantere eklendi, obje siliniyor...");
        
        Destroy(gameObject); // Objeyi yok et
    }



    public string GetDescription()
    {
        return itemData.GetDescription;
    }

    // Returns whether the object can be collected
    public bool isCollectable()
    {
        return true;
    }
}
