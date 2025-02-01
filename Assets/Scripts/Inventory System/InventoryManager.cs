using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu; // Envanter menüsü
    private bool menuActivated; // Menü açık mı?
    public ItemSlot[] itemSlot;
    private MouseController mouseController; 
    
    public ItemSO[] itemSOs;
    private DoorOpener currentDoor; // Hangi kapı için envanter açıldı
    public GameObject wrongItemMessage; // Yanlış eşya mesajı

    private void Start()
    {
        mouseController = FindObjectOfType<MouseController>();

        if (wrongItemMessage != null)
        {
            wrongItemMessage.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (menuActivated)
            {
                // Envanteri kapat
                CloseInventory();
            }
            else
            {
                // Envanteri aç
                OpenInventory();
            }
        }
    }

    public void OpenInventoryForDoor(DoorOpener door)
    {
        currentDoor = door;
        OpenInventory();
    }

    public void OpenInventory()
    {
        if (mouseController != null)
            mouseController.enabled = false; 

        InputManager.LockMouse(false); // Fareyi serbest bırak
        InputManager.LockMovement(true); // Hareketi kilitle
        inventoryMenu.SetActive(true); // Menü aktif et
        menuActivated = true;
    }

    private void CloseInventory()
    {
        if (mouseController != null)
            mouseController.enabled = true;

        InputManager.LockMouse(true); // Fareyi kilitle
        InputManager.LockMovement(false); // Hareketi serbest bırak
        inventoryMenu.SetActive(false); // Menü kapat
        menuActivated = false;
    }

    public void SelectItem(string itemID)
    {
        if (currentDoor != null)
        {
            currentDoor.TryUnlockWithItem(itemID);
            CloseInventory(); // Doğru ya da yanlış fark etmez, envanteri kapat
        }
    }

    public bool HasItem(string itemName)
    {
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.isFull && slot.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

   

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogError("Item slots are not assigned in the InventoryManager!");
            return quantity;
        }

        // **1. Aynı isimde bir item var mı kontrol et ve miktarını artır**
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull && itemSlot[i].itemName == itemName)
            {
                Debug.Log("Stacking item: " + itemName);
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return leftOverItems; // Eğer fazlalık varsa, döndür.
            }
        }

        // **2. Eğer aynı item yoksa, boş bir slot bul ve yeni item ekle**
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull) // Boş slot
            {
                Debug.Log("Adding new item to slot: " + itemName);
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return leftOverItems; // Eğer kalan varsa, döndür.
            }
        }

        // **3. Eğer tüm slotlar doluysa, kalan miktarı döndür**
        Debug.LogWarning("No empty item slots available!");
        return quantity;
    }


    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public bool UseItem(string itemName)
    {
        Debug.Log($"[InventoryManager] Trying to use item: {itemName}");

        // 1️⃣ Önce envanterdeki eşyayı bul
        ItemSlot targetSlot = null;

        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.isFull && slot.itemName == itemName && slot.quantity > 0)
            {
                targetSlot = slot;
                break;
            }
        }

        if (targetSlot == null)
        {
            Debug.LogWarning($"[InventoryManager] UseItem FAILED: {itemName} envanterde bulunamadı veya kullanılamadı.");
            return false;
        }

        // 2️⃣ İlgili `ItemSO` nesnesini bul
        ItemSO targetItemSO = null;

        foreach (ItemSO item in itemSOs)
        {
            Debug.Log($"[DEBUG] Karşılaştırma: {item.itemName} == {itemName} ?");

            if (item.itemName.Trim().ToLower() == itemName.Trim().ToLower()) 
            {
                targetItemSO = item;
                break;
            }
        }

        if (targetItemSO == null)
        {
            Debug.LogWarning($"[InventoryManager] UseItem FAILED: {itemName} için ItemSO bulunamadı!");
            return false;
        }

        // 3️⃣ Eşyayı kullan
        bool usable = targetItemSO.UseItem();

        if (usable)
        {
            Debug.Log($"[InventoryManager] {itemName} kullanıldı, kalan miktar: {targetSlot.quantity - 1}");

            targetSlot.quantity--;

            if (targetSlot.quantity <= 0)
            {
                Debug.Log($"[InventoryManager] {itemName} tükendi, slot temizleniyor.");
                targetSlot.EmptySlot();
            }
            else
            {
                targetSlot.quantityText.text = targetSlot.quantity.ToString();
            }

            return true;
        }

        Debug.LogWarning($"[InventoryManager] {itemName} kullanılamadı.");
        return false;
    }

    
}
