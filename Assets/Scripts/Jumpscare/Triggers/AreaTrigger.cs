using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private string eventName = "OnAreaEntered";
    [SerializeField] private GameObject jumpscareObject; // Inspector üzerinden bağlanacak
    private IAction action;
    private IEvent currentEvent;

    private void Start()
    {
        // Event sistemini al
        currentEvent = EventManager.GetEvent(eventName);

        // JumpScare bileşenini al
        if (jumpscareObject != null)
        {
            var jumpScare = jumpscareObject.GetComponent<JumpScare>();
            if (jumpScare != null)
            {
                action = new JumpScareTriggerAction(jumpScare);
                currentEvent.AddListener(action);
            }
            else
            {
                Debug.LogError("JumpScare component not found on JumpscareObject!");
            }
        }
        else
        {
            Debug.LogError("JumpscareObject is not assigned in the Inspector!");
        }
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
