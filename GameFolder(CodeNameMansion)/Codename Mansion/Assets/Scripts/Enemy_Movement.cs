using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    private Animator animator;
    public bool chasing = false;
    public Vector3 startVec3;
    private EnemyCombat enemyCombat;
    private StudioEventEmitter emitter;
    private void Start() 
    {
        player = GameManager.instance.player.transform;
        animator = GetComponent<Animator>();
        enemyCombat = GetComponent<EnemyCombat>();
        if(startVec3 == Vector3.zero)
        {
            startVec3 = transform.position;
        }

        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.monsterFootstep, gameObject);
        emitter.Play();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            chasing = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            chasing = false;
        }
    }

    void Update()
    {
        UpdateSound();
        if(chasing)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(startVec3);
        }

        if(agent.velocity.magnitude > 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void PlayFootStepSound()
    {
        Debug.Log("Footstep Sound Triggered");
        if (emitter != null && emitter.EventInstance.isValid())
        {
            emitter.EventInstance.start();
        }
    }

    void OnDisable()
    {
        animator.SetBool("IsMoving", false);
        emitter.Stop();
    }

    void OnEnable()
    {
        if (emitter != null)
        {
            emitter.Play();
        }
    }

        private void UpdateSound()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            emitter.EventInstance.setParameterByName("FootstepGround", 1f);
            //if (!emitter.IsPlaying()) // Play only if it's not already playing
            //{
            //    emitter.Play();
            //}
        }
        else
        {
            emitter.EventInstance.setParameterByName("FootstepGround", 0f);
            //if (emitter.IsPlaying()) // Stop only if it's playing
            //{
            //    emitter.Stop();
            //}
        }
    }

    // Position Respawn when player dies
    public void RespawnPos()
    {
        transform.position = startVec3;
    }
}
