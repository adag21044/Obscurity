using UnityEngine;

[CreateAssetMenu(menuName = "Interactable")]
public class InteractableSO : ScriptableObject
{
    public string description;    // Açıklama
    public Sprite itemIcon;       // Eşya ikonu

    // Açıklama alma özelliği
    public string GetDescription => description;

    // Eşya ikonunu alma özelliği
    public Sprite GetItemIcon => itemIcon;
}
