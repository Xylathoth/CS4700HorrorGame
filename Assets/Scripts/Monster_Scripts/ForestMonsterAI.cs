using UnityEngine;
using UnityEngine.AI;

public class ForestMonsterAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float sightRange = 10f;
    public float chaseDuration = 5f;

    public AudioClip idleClip;
    public AudioClip chaseClip;
    private AudioSource audioSource;
    private bool isPlayingChaseClip = false;
    private Animator animator;

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private Transform player;
    private float chaseTimer = 0f;
    private bool isChasing = false;
    private Vector3 lastPatrolPosition;

    public float waitTimeAtPoint = 2f; // seconds to wait at each patrol point
    private bool isWaiting = false;
    private float waitTimer = 0f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        PlayIdleAudio();
        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (isChasing)
        {
            chaseTimer += Time.deltaTime;
            agent.SetDestination(player.position);

            if (!isPlayingChaseClip)
                PlayChaseAudio();   

            if (chaseTimer >= chaseDuration)
            {
                isChasing = false;
                chaseTimer = 0f;
                agent.SetDestination(lastPatrolPosition); // go back to last patrol point

                animator.SetBool("IsRunning", false);
                animator.SetFloat("Speed", agent.speed);

                PlayIdleAudio();
            }
        }
        else
        {

            // WAIT if paused at patrol point
            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTimeAtPoint)
                {
                    isWaiting = false;
                    GoToNextPatrolPoint();
                }
            }
            else
            {
                Patrol();

                if (PlayerInSight())
                {
                    isChasing = true;
                    chaseTimer = 0f;
                    lastPatrolPosition = transform.position;

                    animator.SetBool("IsRunning", true); // triggers Run animation
                }
            }

            //Patrol();

            //if (PlayerInSight())
            //{
            //    isChasing = true;
            //    chaseTimer = 0f;
            //    lastPatrolPosition = transform.position;
            //}
        }
    }

    //void Patrol()
    //{
    //    if (agent.remainingDistance < 0.5f && !agent.pathPending)
    //    {
    //        GoToNextPatrolPoint();
    //    }
    //}

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            isWaiting = true;
            waitTimer = 0f;
            agent.isStopped = true; // freeze in place

            animator.SetFloat("Speed", 0f); // triggers Idle animation
        }
    }



    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;


        agent.isStopped = false; // resume movement

        animator.SetBool("IsRunning", false);
        animator.SetFloat("Speed", agent.speed); // use current walking speed

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    bool PlayerInSight()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= sightRange;
    }


    void PlayIdleAudio()
    {
        isPlayingChaseClip = false;
        audioSource.clip = idleClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void PlayChaseAudio()
    {
        isPlayingChaseClip = true;
        audioSource.clip = chaseClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
