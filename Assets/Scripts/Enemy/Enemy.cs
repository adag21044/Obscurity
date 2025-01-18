using UnityEngine;

public class Enemy : MonoBehaviour, ICatch, IFollowable
{
    [SerializeField] private GameObject player; 

    public void Update()
    {
        Follow();
    }
    
    public void Catch()
    {
        // TODO: Implement the catch logic
    }

    public void Follow()
    {
        float speed = 0.15f; // Hareket hızı
        transform.position = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}