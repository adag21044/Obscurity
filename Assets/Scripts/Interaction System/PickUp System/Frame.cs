using UnityEngine;

public class Frame : MonoBehaviour
{
    public string correctPaintingName; // Bu çerçevede olması gereken resmin adı

    public bool HasCorrectPainting()
    {
        if (transform.childCount == 0) return false; // Çerçevenin çocuğu yoksa yanlış

        Transform placedPainting = transform.GetChild(0); // Çerçevenin içindeki ilk nesneyi al
        bool isCorrect = placedPainting.name == correctPaintingName;

        Debug.Log($"📌 Çerçeve: {gameObject.name}, Beklenen: {correctPaintingName}, Mevcut: {placedPainting.name}, Doğru mu? {isCorrect}");
        return isCorrect;
    }

    public void SetPainting(Transform painting)
    {
        painting.SetParent(transform); // Resmi çerçevenin çocuğu yap
        painting.localPosition = Vector3.zero;
        painting.localRotation = Quaternion.identity;
    }
}
