using UnityEngine;

public class Frame : MonoBehaviour
{
    public FrameSO frameSO; 

    public bool HasCorrectPainting()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Picture>(out var painting))
            {
                if (painting.pictureSO.index == frameSO.index)
                {
                    Debug.Log($"✅ Frame '{name}' has correct painting.");
                    return true;
                }
            }
        }

        Debug.Log($"❌ Frame '{name}' does NOT have correct painting.");
        return false;
    }

}