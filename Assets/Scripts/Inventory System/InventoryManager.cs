using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu; // Envanter menüsü
    private bool menuActivated; // Menü açık mı?
    public ItemSlot[] itemSlot;
    private MouseController mouseController; 

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

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogError("Item slots are not assigned in the InventoryManager!");
            return;
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite);
                return;
            }
        }

        Debug.LogWarning("No empty item slots available!");
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
