using UnityEngine;
using UnityEngine.Video;

public class JumpScare : MonoBehaviour, ITriggerable
{
    public GameObject jumpscareObject; // Ekranda video gösteren obje
    public AudioClip scareSound; // Jumpscare sesi
    private AudioSource audioSource; // Ses kaynağı
    private VideoPlayer videoPlayer; // Video oynatıcı
    public GameObject[] canvasObjects; // Canvas objects 

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

    private void Start()
    {
        EventManager.GetEvent("OnJumpscareTrigger").AddListener(new JumpScareTriggerAction(this));
    }

    public void Trigger()
    {
        if (jumpscareObject != null)
        {
            DisableCanvasObjects(canvasObjects);
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

    private void DisableCanvasObjects(GameObject[] canvasObjects)
    {
        foreach (var canvasObject in canvasObjects)
        {
            canvasObject.SetActive(false);
        }
    }
}
