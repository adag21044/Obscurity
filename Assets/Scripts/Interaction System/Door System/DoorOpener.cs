using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private bool isAnimating = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public float rotationDuration = 1f;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90f, transform.eulerAngles.z);
    }

    public string GetDescription()
    {
        return isOpen ? "Press E to close the door!" : "Press E to open the door!";
    }

    public void Interact()
    {
        if (!isAnimating)
        {
            StartCoroutine(RotateDoor());
        }
    }

    private IEnumerator RotateDoor()
    {
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