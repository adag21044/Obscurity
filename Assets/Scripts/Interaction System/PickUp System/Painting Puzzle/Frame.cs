using UnityEngine;
using System;                    // Action için

public class Frame : MonoBehaviour
{
    [Tooltip("Which painting belongs to this frame?")]
    public PaintingSO correctPainting;

    private Painting currentPainting;

    // Fired every time a painting is set or removed
    public event Action<Frame> OnPaintingChanged;
    public string correctPaintingName;

    public bool HasCorrectPainting()
    {   
        foreach (Transform child in transform)
        {
            if (child.name == correctPaintingName)
            {
                Debug.Log($"✅ Frame '{name}' has correct painting '{correctPaintingName}'");
                return true;
            }
        }

        Debug.Log($"❌ Frame '{name}' does NOT have correct painting '{correctPainting.displayName}'");
        return false;
    }

    public void SetPainting(Painting painting)
    {
        // Remove previously placed painting if any
        if (currentPainting != null)
            Destroy(currentPainting.gameObject);

        currentPainting = painting;

        // Parenting
        painting.transform.SetParent(transform, false);
        painting.transform.localPosition  = Vector3.zero;
        painting.transform.localRotation  = Quaternion.identity;

            Debug.Log(
                $"[Frame] {name} → placed {painting.data.displayName} | correct? {HasCorrectPainting()}");

        // Notify subscribers
        OnPaintingChanged?.Invoke(this);
    }

    public void ClearPainting()
    {
        if (currentPainting == null) return;

        Destroy(currentPainting.gameObject);
        currentPainting = null;
        OnPaintingChanged?.Invoke(this);
    }

    private void Start()
    {
        InvokeRepeating(nameof(LogChildNames), 1f, 5f); // her 5 saniyede bir çalıştır
    }

    private void LogChildNames()
    {
        Debug.Log($"[Child Log] Frame: {name} has {transform.childCount} children.");
        
        foreach (Transform child in transform)
        {
            Debug.Log($"↪ Child: {child.name}");
        }
    }

}
