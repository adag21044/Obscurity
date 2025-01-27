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

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if (quantityText == null || itemImage == null)
        {
            Debug.LogError("quantityText or itemImage is not assigned in the ItemSlot!");
            return;
        }

        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
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
