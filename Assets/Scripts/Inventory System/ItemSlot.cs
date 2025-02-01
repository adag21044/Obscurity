using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems;
    [SerializeField] public TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

    public Image itemDescripttionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;
    private bool isProcessing = false;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Adds the specified item to this slot. Returns leftover items if slot capacity is exceeded.
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // If the slot is full but has a different item, do not add
        if (isFull && this.itemName != itemName)
        {
            return quantity;
        }

        // If the slot is empty, record new item data
        if (!isFull)
        {
            this.itemName = itemName;
            this.itemSprite = itemSprite;
            itemImage.sprite = itemSprite;
            this.itemDescription = itemDescription;
            quantityText.enabled = true;
        }

        // Increase current quantity
        this.quantity += quantity;

        // If capacity is exceeded
        if (this.quantity > maxNumberOfItems)
        {
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            quantityText.text = this.quantity.ToString();
            isFull = true;
            return extraItems;
        }

        // Update UI
        quantityText.text = this.quantity.ToString();
        isFull = this.quantity > 0;
        return 0;
    }

    // Handle pointer clicks on this slot
    public void OnPointerClick(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        Debug.Log($"[ItemSlot] OnPointerClick triggered for {itemName}");

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    // Left-click usage logic
    private void OnLeftClick()
    {
        if (isProcessing) return; // Re-entrancy önlemi
        isProcessing = true;

        if (thisItemSelected)
        {
            // Eşyayı kullanmak yerine kapıya seçilen item'ı gönderiyoruz.
            Debug.Log($"[ItemSlot] Attempting to select item: {itemName}");

            // Sadece seçili item'ı "kapıya anahtar olarak" veriyoruz:
            inventoryManager.SelectItem(itemName);
        }
        else
        {
            // Henüz seçili değilse, bu slot'u seçip item bilgilerini göster
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescripttionImage.sprite = itemSprite;

            // Eğer sprite boş ise fallback olarak emptySprite göster
            if (itemDescripttionImage.sprite == null)
            {
                itemDescripttionImage.sprite = emptySprite;
            }
        }

        isProcessing = false;
    }


    // Empties the slot data
    public void EmptySlot()
    {
        Debug.Log("Slot emptied!");

        quantity = 0;
        isFull = false;
        itemName = "";
        itemSprite = emptySprite;
        itemDescription = "";

        quantityText.enabled = false;
        itemImage.sprite = emptySprite;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescripttionImage.sprite = emptySprite;
    }

    // Right-click logic for permanently deleting an item
    private void OnRightClick()
    {
        Debug.Log("WARNING: You are about to delete one item permanently!");

        bool confirmDelete = ShowConfirmationDialog("Are you sure you want to delete one " + itemName + "? This cannot be undone!");

        if (confirmDelete)
        {
            Debug.Log("One item deleted permanently: " + itemName);
            RemoveSingleItemFromInventory();
        }
        else
        {
            Debug.Log("Item deletion canceled.");
        }
    }

    private bool ShowConfirmationDialog(string message)
    {
        Debug.Log(message + " (Simulating UI popup, return true to confirm)");

        // Simulate a confirmation. In a real project, you'd show a UI and wait for user input.
        return true; // Always 'true' for this example
    }

    private void RemoveSingleItemFromInventory()
    {
        Debug.Log("Removing 1 item from inventory: " + itemName);

        // If it's the last one, just clear the slot
        if (quantity == 1)
        {
            Debug.Log("Last item removed, slot will be emptied!");
            EmptySlot();
        }
        else
        {
            // Decrement by one
            quantity -= 1;
            quantityText.text = quantity.ToString();
            Debug.Log("New quantity: " + quantity);
        }
    }
}
