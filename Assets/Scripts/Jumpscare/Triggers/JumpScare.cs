using UnityEngine;
using UnityEngine.Video;

public class JumpScare : MonoBehaviour, ITriggerable
{
    public GameObject jumpscareObject; // Ekranda video gösteren obje
    public AudioClip scareSound; // Jumpscare sesi
    private AudioSource audioSource; // Ses kaynağı
    private VideoPlayer videoPlayer; // Video oynatıcı

    private void Awake()
    {
        if (jumpscareObject != null)
        {
            videoPlayer = jumpscareObject.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached += OnVideoEnd; // Video tamamlanınca tetiklenir
            }

            videoPlayer.errorReceived += (vp, message) =>
            {
                Debug.LogError($"VideoPlayer Error: {message}");
            };

        }
        else
        {
            Debug.LogWarning("JumpscareObject is not assigned!");
        }
    }

    public void Trigger()
    {
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(true); // Objeyi etkinleştir
        }

        if (scareSound != null)
        {
            if (audioSource == null)
            {
                audioSource = jumpscareObject.AddComponent<AudioSource>();
                audioSource.clip = scareSound;
            }
            audioSource.Play(); // Sesi çal
        }

        if (videoPlayer != null)
        {
            videoPlayer.Play(); // Videoyu başlat
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(false); // Video bittiğinde kapat
        }
    }
}
