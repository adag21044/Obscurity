using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Reference to the inventory panel
    private bool isInventoryOpen;      // Flag to check if the inventory is open

    private void Start()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("InventoryPanel atanmamış!");
        }
        else
        {
            Debug.Log($"InventoryPanel: {inventoryPanel.name}");
        }

        inventoryPanel.SetActive(false); // Hide the inventory panel
        InputManager.LockMouse(true);    // Başlangıçta fare hareketi açık
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory(); // Toggle the inventory panel
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen; // Envanteri aç/kapat
        inventoryPanel.SetActive(isInventoryOpen);

        // Envanter durumu aktif/pasif
        if (isInventoryOpen)
        {
            InputManager.LockAllInput(true); // Tüm inputları kilitle
        }
        else
        {
            InputManager.ResetInputLocks(); // Tüm inputları eski haline getir
        }
    }

    public void AddItem(Sprite itemIcon)
    {
        foreach (Transform slot in inventoryPanel.transform)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage == null)
            {
                Debug.LogError($"Slot {slot.name} içinde Image bileşeni yok!");
                continue;
            }

            if (slotImage.sprite == null)
            {
                slotImage.sprite = itemIcon; // Eşyayı ekle
                Debug.Log($"Eşya envantere eklendi: {itemIcon.name} (Slot: {slot.name})");
                return;
            }
            else
            {
                Debug.Log($"Slot {slot.name} zaten dolu: {slotImage.sprite.name}");
            }
        }

        Debug.LogWarning("Boş slot bulunamadı!");
    }



    public bool UseItem(string itemName)
    {
        foreach (Transform slot in inventoryPanel.transform)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null && slotImage.sprite != null && slotImage.sprite.name == itemName)
            {
                slotImage.sprite = null; // Eşyayı kullan
                return true;
            }
        }
        return false; // Eşya bulunamadı
    }
}
