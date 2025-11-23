using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] private float respawnTimer;
    [SerializeField] private int maxHealth;

    private NavMeshAgent agent;
    private Animator animator;
    public bool isDead;
    private float currentRespawnTime;
    private Enemy_Movement enemyMove;
    private CapsuleCollider capsuleCollider;
    private float defaultCenterY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMove = GetComponent<Enemy_Movement>();
        isDead = false;
        currentHealth = maxHealth;
        currentRespawnTime = respawnTimer;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        defaultCenterY = capsuleCollider.center.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            RespawnTime();
        }
    }

    public void TakeDamage(int totalDamage)
    {
        currentHealth -= totalDamage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        capsuleCollider.center.Set(capsuleCollider.center.x, 0, capsuleCollider.center.z);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.monsterDead, transform.position);
        if(agent.hasPath)
        {
            agent.ResetPath();
        }

        agent.velocity = Vector3.zero;
        animator.SetBool("IsDead", true);
        animator.SetBool("IsMoving", false);
        enemyMove.enabled = false;
        agent.enabled = false;
        //play Death Animation animator.SetBool("isDead", isDead)
        //disable enemy_movement script
    }

    private void RespawnTime()
    {
        currentRespawnTime -= Time.deltaTime;

        if(currentRespawnTime <= 0)
        {
            Respawn();
        }

        if(currentRespawnTime <= 2)
        {
            animator.SetBool("GettingUp", true);
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        capsuleCollider.center.Set(capsuleCollider.center.x, defaultCenterY, capsuleCollider.center.z);
        currentRespawnTime = respawnTimer;
        animator.SetBool("IsDead", false);
        animator.SetBool("GettingUp", false);
        agent.enabled = true;
        enemyMove.enabled = true;
        //play respawning animation
        isDead = false;
    }

    // Maxing health when player dies
    public void EnemyRespawnPlayerDeath()
    {
        currentHealth = maxHealth;
    }
}
