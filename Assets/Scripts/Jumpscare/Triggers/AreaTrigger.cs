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

        // JumpScare nesnesini oluştur ve ilgili GameObject'i bağla
        var jumpScare = new JumpScare
        {
            jumpscareObject = GameObject.Find("JumpscareObject") // Objeyi sahneden bul veya prefab olarak yükle
        };

        action = new JumpScareTriggerAction(jumpScare);

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
