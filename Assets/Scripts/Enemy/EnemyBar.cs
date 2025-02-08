using UnityEngine;
using DentedPixel;

public class EnemyBar : MonoBehaviour
{
    public GameObject bar;
    public int time; 
    private IEvent jumpscareEvent;
    public JumpScare jumpScare;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpscareEvent = EventManager.GetEvent("OnJumpscareTrigger");
        AnimateBar();
    }

    private void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time).setOnComplete(TriggerJumpscare);
    }

    private void TriggerJumpscare()
    {
        Debug.Log("Jumpscare triggered as bar is full!");

        if (jumpScare != null)
        {
            jumpScare.Trigger();
        }
        else
        {
            Debug.LogError("JumpScare component is not assigned in the Inspector!");
        }
    }
}
