using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour, IInteractable
{
    public string requiredKeyID; // Kapıyı açmak için gereken anahtar ID
    public bool isLocked = true;
    private bool isOpen = false;
    private bool isAnimating = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public float rotationDuration = 1f;
    private InventoryManager inventoryManager;
    public GameObject wrongItemMessage; // Yanlış eşya uyarısı
    public AudioSource audioSource; 
    public AudioClip doorOpenSound;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90f, transform.eulerAngles.z);
        inventoryManager = FindObjectOfType<InventoryManager>();

        if (wrongItemMessage != null)
        {
            wrongItemMessage.SetActive(false); // Başlangıçta kapalı olsun
        }
    }

    public string GetDescription()
    {
        return isLocked ? "This door is locked! You need a key." : "Press E to open/close the door!";
    }

    public void Interact()
    {
        if (isLocked)
        {
            inventoryManager.OpenInventoryForDoor(this); 
        }
        else if (!isAnimating)
        {
            StartCoroutine(RotateDoor());
        }
    }

    public void TryUnlockWithItem(string itemID)
    {
        if (itemID == requiredKeyID)
        {
            Debug.Log($"[DoorOpener] {itemID} anahtarı kullanıldı, kapı açılıyor!");
            inventoryManager.UseItem(itemID); 
            isLocked = false;
            StartCoroutine(RotateDoor());
        }
        else
        {
            Debug.Log("[DoorOpener] Yanlış eşya seçildi! Bu kapıyı açamazsın.");
            if (wrongItemMessage != null)
            {
                StartCoroutine(ShowWrongItemMessage());
            }
        }
    }

    private IEnumerator ShowWrongItemMessage()
    {
        wrongItemMessage.SetActive(true);
        yield return new WaitForSeconds(2f); // 2 saniye sonra kapanacak
        wrongItemMessage.SetActive(false);
    }

    private IEnumerator RotateDoor()
    {
        Debug.Log("Door is opening");
        audioSource.PlayOneShot(doorOpenSound);

        isAnimating = true;
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = isOpen ? closedRotation : openRotation;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = !isOpen;
        isAnimating = false;
    }
}
