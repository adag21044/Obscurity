using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && menuActivated)
        {
            inventoryMenu.SetActive(false);  
            menuActivated = false;
        }
        else
        if(Input.GetKeyDown(KeyCode.Tab) && !menuActivated)
        {
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }   
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

}
