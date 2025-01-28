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
        // Eğer slot dolu ama farklı bir item içeriyorsa ekleme yapma
        if (isFull && this.itemName != itemName)
        {
            return quantity;
        }

        // Eğer slot boşsa item bilgilerini kaydet
        if (!isFull)
        {
            this.itemName = itemName;
            this.itemSprite = itemSprite;
            itemImage.sprite = itemSprite;
            this.itemDescription = itemDescription;
            quantityText.enabled = true;
        }

        // Miktarı artır
        this.quantity += quantity;

        // Eğer maksimum kapasiteyi aşarsa
        if (this.quantity > maxNumberOfItems)
        {
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            quantityText.text = this.quantity.ToString();
            isFull = true;
            return extraItems; // Fazla itemleri döndür
        }

        // Güncel miktarı UI'da göster
        quantityText.text = this.quantity.ToString();
        isFull = this.quantity > 0; // Slot doluluk durumunu güncelle
        return 0; // Kalan item yok
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
        if(thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if(usable)
            {
                this.quantity -= 1;

                quantityText.text = this.quantity.ToString();

                if(this.quantity <= 0)
                    EmptySlot();
            }
             
        }
        else
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
            

        

    }

    private void EmptySlot()
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


    public void OnRightClick()
    {
        // Oyuncuya uyarı gösterelim
        Debug.Log("WARNING: You are about to delete one item permanently!");

        // UI üzerinden bir onay ekranı açılmalı (Şimdilik sadece Debug ile gösterelim)
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

        // Normalde bir UI penceresi açmalıyız. Şimdilik her zaman 'true' döndürelim.
        return true; // TEST için her zaman silinsin, UI eklenince kullanıcıya soracağız.
    }

    private void RemoveSingleItemFromInventory()
    {
        Debug.Log("Removing 1 item from inventory: " + itemName);

        // Eğer 1 tane kaldıysa tamamen sil
        if (quantity == 1)
        {
            Debug.Log("Last item removed, slot will be emptied!");
            EmptySlot();
        }
        else
        {
            // 1 tane eksilt
            quantity -= 1;
            quantityText.text = quantity.ToString();
            Debug.Log("New quantity: " + quantity);
        }
    }


   



}
