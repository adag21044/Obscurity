using UnityEngine;
using UnityEngine.Video;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private string eventName = "OnAreaEntered";
    [SerializeField] private string videoPath;
    private IAction action;
    private IEvent currentEvent;

    private void Start()
    {
        // Event sistemini al
        currentEvent = EventManager.GetEvent(eventName);

        // VideoPlayer'ı dinamik olarak bir GameObject'e ekleyin
        var videoPlayerObject = new GameObject("VideoPlayerObject");
        var videoPlayer = videoPlayerObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.url = videoPath;           // Videonun yolu
        videoPlayer.playOnAwake = false;      // Videoyu otomatik oynatma
        videoPlayer.isLooping = false;        // Döngüye girmesini önle

        // Triggerable interface'ini kullanan VideoPlayer sınıfını hazırlayın
        var videoTriggerable = new UnityVideoPlayerAdapter(videoPlayer);
        action = new JumpScareTriggerAction(videoTriggerable);

        // Event sistemine eylemi ekle
        currentEvent.AddListener(action);
    }

    private void OnDestroy()
    {
        if (currentEvent != null && action != null)
        {
            currentEvent.RemoveListener(action);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentEvent.Notify();
        }
    }
}
