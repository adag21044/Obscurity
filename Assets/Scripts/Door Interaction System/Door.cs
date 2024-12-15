using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string requiredKeyTag; // The required key tag
    private IDoorState currentState;
    private bool isOpened = false; // Is the door opened?
    [SerializeField] private AudioSource audioSource; // AudioSource for door sound

    private void Start()
    {
        currentState = new LockedDoorState(); // Initialize as locked
    }

    private void Update()
    {
        // Oyuncu E tuşuna bastığında etkileşim kontrolü
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        // Kapı ile oyuncu arasında bir Raycast kontrolü yapacağız
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f)) // 3 metre mesafede kontrol
        {
            if (hit.collider.gameObject == this.gameObject) // Kapıya bakıyorsa
            {
                Interact();
            }
        }
    }

    public void Interact()
    {
        // Anahtar kontrolü
        if (KeyManager.Instance.HasKey(requiredKeyTag))
        {
            UnlockDoor();
        }
        else
        {
            currentState.Interact(this);
            UIManager.Instance.DisplayMessage("The door is locked. Find the right key!");
        }
    }

    public void UnlockDoor()
    {
        Debug.Log($"The door has been unlocked with {requiredKeyTag}!");
        ChangeState(new UnlockedDoorState());
        OpenDoor();
        UIManager.Instance.DisplayMessage("You unlocked the door!");
    }

    public void OpenDoor()
    {
        if (!isOpened) // Kapı zaten açılmadıysa
        {
            isOpened = true;

            // Kapıyı -145 derece döndür
            transform.localRotation = Quaternion.Euler(0, -145, 0);

            Debug.Log("The door opens.");
            UIManager.Instance.DisplayMessage("The door opens.");

            // Kapı açılma sesi çal
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public string GetRequiredKeyTag()
    {
        return requiredKeyTag;
    }

    public void ChangeState(IDoorState newState)
    {
        currentState = newState;
        Debug.Log($"Door state changed to: {newState.GetType().Name}");
    }
}
