using UnityEngine;

public class DoorObserver : MonoBehaviour
{
    private Door door;

    private void Start()
    {
        door = GetComponent<Door>();

        // Subscribe to the key updated event
        KeyManager.Instance.OnKeyUpdated += UnlockDoor;
    }

    private void UnlockDoor()
    {
        // Kapıyı kontrol et ve Unlock işlemi yap
        if (KeyManager.Instance.HasKey(door.GetRequiredKeyTag()))
        {
            door.UnlockDoor();
        }
    }

    private void OnDestroy()
    {
        // Event'ten çık
        KeyManager.Instance.OnKeyUpdated -= UnlockDoor;
    }
}
