using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu; // Envanter menüsü
    private bool menuActivated; // Menü açık mı?
    public ItemSlot[] itemSlot;
    private MouseController mouseController; 
    
    public ItemSO[] itemSOs;

    private void Start()
    {
        mouseController = FindObjectOfType<MouseController>();
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

    private void OpenInventory()
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

    public bool UseItem(string itemName)
    {
        for(int i = 0; i < itemSOs.Length; i++)
        {
            if(itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
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
}
