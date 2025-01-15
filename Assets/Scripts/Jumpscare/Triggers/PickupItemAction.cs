using UnityEngine;

public class PickupItemAction : MonoBehaviour
{
    [SerializeField] private string eventName = "OnItemPicked";
    [SerializeField] private string videoPath;
    private IAction action;

    private void Start()
    {
        var videoPlayerObject = new GameObject("CustomVideoPlayer");
        var customVideoPlayer = videoPlayerObject.AddComponent<CustomVideoPlayer>();
        customVideoPlayer.Initialize(videoPath);

        action = new JumpScareTriggerAction(customVideoPlayer);
        EventManager.GetEvent(eventName).AddListener(action);
    }

    private void OnDestroy()
    {
        EventManager.GetEvent(eventName).RemoveListener(action);
    }

    public void OnPickup()
    {
        EventManager.GetEvent(eventName).Notify();
    }
}
