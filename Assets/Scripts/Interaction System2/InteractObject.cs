using UnityEngine;
using UnityEngine.Events;

public class InteractObject : MonoBehaviour
{
    public string interactionText = "Press E to Interect";
    public UnityEvent OnInteract;

    public string GetInteractionText()
    {
        Debug.Log($"Returning interaction text: {interactionText}"); // Debug ekleyin
        return interactionText;
    }


    public void Interact()
    {
        OnInteract.Invoke();
    }
}