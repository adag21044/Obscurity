using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] private string eventName = "OnCollisionWithPlayer";
    [SerializeField] private string videoPath;
    private IAction action;
    private IEvent currentEvent;

    private void Start()
    {
        currentEvent = EventManager.GetEvent(eventName);

        var videoPlayerObject = new GameObject("CustomVideoPlayer");
        var customVideoPlayer = videoPlayerObject.AddComponent<CustomVideoPlayer>();
        customVideoPlayer.Initialize(videoPath);

        action = new JumpScareTriggerAction(customVideoPlayer);
        currentEvent.AddListener(action);
    }

    private void OnDestroy()
    {
        if (currentEvent != null && action != null)
        {
            currentEvent.RemoveListener(action);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            currentEvent.Notify();
        }
    }
}
