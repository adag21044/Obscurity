using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;

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
}
