using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public Camera camera;
    public float InteractionDistance = 10f;
    public TextMeshProUGUI interactionText;
    public InteractObject currentInteractable;

    private void Start()
    {
        if (interactionText == null)
        {
            Debug.LogError("interactionText reference is null!");
        }
    }

    private void Update()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            InteractObject interactableObject = hit.collider.GetComponent<InteractObject>();

            if (interactableObject != null)
            {
                // Eğer aynı objeye bakıyorsak tekrar çalıştırma
                if (interactableObject == currentInteractable) return;

                // Yeni bir objeye bakıyorsak eski UI'yi temizle
                ClearCurrentInteractable();

                // Yeni objeyi ayarla ve UI'yi göster
                currentInteractable = interactableObject;
                interactionText.gameObject.SetActive(true);
                interactionText.text = currentInteractable.GetInteractionText();
                Debug.Log($"Interaction Text Set: {interactionText.text}");
            }
            else
            {
                // Eğer raycast bir InteractObject bulamazsa temizle
                ClearCurrentInteractable();
            }
        }
        else
        {
            ClearCurrentInteractable();
        }

        // E tuşuna basıldığında etkileşim tetiklenir
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable?.Interact();
        }
    }

    private void ClearCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
            interactionText.gameObject.SetActive(false); // UI'yi kapat
        }
    }
}
