using UnityEngine;
public class JumpScare : MonoBehaviour, ITriggerable
{
    public GameObject jumpscareObject;
    public AudioClip scareSound;
    private AudioSource audioSource;

    public void Trigger()
    {
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(true);
        }

        if (scareSound != null)
        {
            if (audioSource == null)
            {
                audioSource = jumpscareObject.AddComponent<AudioSource>();
                audioSource.clip = scareSound;
            }
            audioSource.Play();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger();
        }
    }
}
