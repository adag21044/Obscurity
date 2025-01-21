using UnityEngine;

// Changes the color of the object to a random color
public class ColorChanger : MonoBehaviour, IInteractable
{
    private Material mat;

    private void Start()
    {
        // Get the material of the object
        mat = GetComponent<MeshRenderer>().material;
    }

    // Returns the description displayed to the player
    public string GetDescription()
    {
        return "Press E to change the color!";
    }

    // Changes the object's color
    public void Interact()
    {
        mat.color = new Color(Random.value, Random.value, Random.value);
    }

    // Returns whether this interaction is available
    public bool IsAvailable()
    {
        return true; // Always available
    }

    // Returns whether the object can be collected
    public bool isCollectable()
    {
        return false; // Not collectable
    }
}
