using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    public Transform player;

    [Header("Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    public float speed = 3.5f;

    [Header("Combat")]
    public float attackDistance = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    [Header("Flee")]
    public float fleeHealthThreshold = 30f;
    public float fleeDistance = 10f;

    private NavMeshAgent agent;

    private enum State
    {
        Chase,
        Attack,
        Flee
    }

    private State currentState;

    // Evento da chiamare quando il nemico muore
    public event Action<GameObject> onDeath;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        currentHealth = maxHealth;
        currentState = State.Chase;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (currentHealth <= fleeHealthThreshold)
            currentState = State.Flee;
        else if (distance <= attackDistance)
            currentState = State.Attack;
        else
            currentState = State.Chase;

        switch (currentState)
        {
            case State.Chase: Chase(); break;
            case State.Attack: Attack(); break;
            case State.Flee: Flee(); break;
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.isStopped = true;
       

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
                pm.TakeDamage(attackDamage);

            lastAttackTime = Time.time;
        }
    }

    void Flee()
    {
        agent.isStopped = false;

        Vector3 dir = (transform.position - player.position).normalized;
        Vector3 targetPos = transform.position + dir * fleeDistance;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 10f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (GestioneSFX.Instance != null)
            GestioneSFX.Instance.PlaySFX(GestioneSFX.Instance.destroy);

        onDeath?.Invoke(gameObject);

        Debug.Log("Zombie morto");
        Destroy(gameObject);
    }
}