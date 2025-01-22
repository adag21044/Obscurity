using UnityEngine;
using System.Collections;

public class UnLockedDoorBehaviour : MonoBehaviour
{
    public Transform doorTransform;

    //Rotate the door y axis smoothly
    public void OpenDoor()
    {
        StartCoroutine(OpenDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        float elapsedTime = 0;
        float duration = 1.5f;
        Vector3 startRotation = doorTransform.eulerAngles;
        Vector3 endRotation = startRotation + new Vector3(0, 90, 0);

        while (elapsedTime < duration)
        {
            doorTransform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.eulerAngles = endRotation;
    }
}