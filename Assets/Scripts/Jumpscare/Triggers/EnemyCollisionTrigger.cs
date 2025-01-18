using UnityEngine;

public class EnemyCollisionTrigger : MonoBehaviour
{
    [SerializeField] private string eventName = "OnEnemyCollision";
    [SerializeField] private GameObject jumpscareObject; 
    private IAction action;
    private IEvent currentEvent;

    private void Start()
    {
        // Get Event system
        currentEvent = EventManager.GetEvent(eventName);

        if (currentEvent == null)
        {
            Debug.LogError("CurrentEvent is null! Event name might be wrong.");
        }

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
            Debug.Log("Player collided with the enemy!");
            currentEvent.Notify();
        }
    }
}