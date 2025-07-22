using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int damage = 10;
    public float attackRate = 1f;
    public float attackRange = 1.5f;
    public float maxHealth = 50f;

    private float currentHealth;
    private NavMeshAgent agent;
    private Transform player;
    private float lastAttackTime;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.isStopped = true;

            if (Time.time - lastAttackTime >= 1f / attackRate)
            {
                player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
