using System.Collections;
using UnityEngine;

public class SP_DoorOpener : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private Quaternion closedRotation;
    [SerializeField] private Quaternion openRotation;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool isAnimating = false;

    private void Start()
    {
        
        

        
    }

    public void Interact()
    {
        
        StartCoroutine(RotateDoor());
        
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

    public string GetDescription()
    {
        return "Press E to open the door!";
    }
}