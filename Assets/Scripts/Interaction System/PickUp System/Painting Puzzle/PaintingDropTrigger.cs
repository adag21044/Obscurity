// Attach this script to the Frame GameObject
using UnityEngine;

public class PaintingDropTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Painting painting = other.GetComponent<Painting>();
        Frame frame = GetComponent<Frame>();

        if (painting != null && frame != null)
        {
            frame.SetPainting(painting);
        }
    }
}
