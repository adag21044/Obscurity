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
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

    public Image itemDescripttionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if (isFull)
        {
            return quantity;
        }

        // update name
        this.itemName = itemName;
        
        // update sprite
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        
        // update description  
        this.itemDescription = itemDescription;

        // update quantity
        this.quantity += quantity;

        if(this.quantity >= maxNumberOfItems)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
            isFull = true;

            //return leftovers
            int extrItems = this.quantity - maxNumberOfItems; 
            this.quantity = maxNumberOfItems;
            return extrItems;
        }

        // update quantity text 
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescripttionImage.sprite = itemSprite;

        if(itemDescripttionImage.sprite == null)
        {
            itemDescripttionImage.sprite = emptySprite;
        }

    }

    public void OnRightClick()
    {

    }
}
