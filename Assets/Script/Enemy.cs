using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;

    public float speed = 5f;
    public float fleeDistance = 1770f;
    public float startFleeingDistance = 1775f;

    private NavMeshAgent agent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        float distanceToPlyer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlyer < startFleeingDistance)
        {
            Flee();
        }
    }

    void Flee()
    {
        Vector3 dirToPlayer = transform.position - player.position;
        Vector3 newPos = transform.position + dirToPlayer.normalized * fleeDistance;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPos, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
