using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    [SerializeField] private float chaseSpeed = 1.5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed; // Set the speed
    }

    void Update()
    {
        agent.SetDestination(target.position);   
        agent.speed = chaseSpeed; // Set the speed
    }
}