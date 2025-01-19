using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon; // Slot içerisindeki ikon
    private Transform originalParent; // İkonun orijinal parent objesi
    private Canvas canvas; // UI Canvas referansı

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (icon.sprite == null) return; // Boş slot taşınamaz
        originalParent = icon.transform.parent; // Orijinal parent'ı kaydet
        icon.transform.SetParent(canvas.transform); // İkonu canvas altına taşı
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (icon.sprite == null) return; // Boş slot taşınamaz
        icon.transform.position = Input.mousePosition; // İkonu fare ile hareket ettir
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.transform.SetParent(originalParent); // İkonu orijinal parent'a döndür

        // Fare altındaki objeyi kontrol et
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            InventorySlot targetSlot = hit.collider.GetComponent<InventorySlot>();
            if (targetSlot != null && targetSlot.icon.sprite == null) // Boş bir slota bırakılıyorsa
            {
                targetSlot.icon.sprite = icon.sprite; // İkonu yeni slota taşı
                icon.sprite = null; // Eski slotu boşalt
            }
        }
    }
}
