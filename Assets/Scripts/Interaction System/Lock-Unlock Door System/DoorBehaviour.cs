using UnityEngine;
using System.Collections;

public class DoorBehaviour : MonoBehaviour
{
    public DoorStates doorState = DoorStates.Locked; // Varsayılan olarak kilitli
    public string requiredKeyName;                  // Kapıyı açmak için gereken anahtar
    public Transform doorTransform;                 // Kapının dönüş ekseni

    public void TryUnlockDoor(string selectedKey)
    {
        if (doorState == DoorStates.Locked)
        {
            if (selectedKey == requiredKeyName)
            {
                Debug.Log("Doğru anahtar seçildi! Kapı açılıyor...");
                doorState = DoorStates.Closed;
                StartCoroutine(OpenDoorCoroutine());
            }
            else
            {
                Debug.Log("Yanlış anahtar seçildi!");
            }
        }
        else
        {
            Debug.Log("Kapı zaten açık.");
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        float elapsedTime = 0f;
        float duration = 1.5f;
        Vector3 startRotation = doorTransform.eulerAngles;
        Vector3 endRotation = startRotation + new Vector3(0, 180, 0);

        while (elapsedTime < duration)
        {
            doorTransform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.eulerAngles = endRotation;
        doorState = DoorStates.Opened; // Kapının durumu açık olarak güncellenir
    }
}
