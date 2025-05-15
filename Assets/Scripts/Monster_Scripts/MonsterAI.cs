using System.Collections;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    public Transform player;
    public Light flashlight;
    public float moveSpeed = 2f;
    public float respawnDelay = 5f;
    [Tooltip("How close before monster attacks")]
    public float attackRange = 3f;

    private Animator animator;
    private bool attackReady = true;
    private Vector3 startPosition;
    private bool isHidden = false;
    private int monsterLayerMask;

    private bool isJumpscareAudioPlaying = false;
    public AudioClip jumpscareClip;
    public AudioClip deathClip;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        monsterLayerMask = LayerMask.GetMask("Monster");

        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (isHidden) return;


        // chase & face the player
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        if (dir != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
        }

        // ATTACK logic runs every frame, regardless of flashlight
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange && attackReady)
        {
            Debug.Log($"MonsterAI: In range ({dist:F2}), firing Attack trigger");
            animator.SetTrigger("Attack");
            attackReady = false;
            PlayJumpscareAudio();
        }
        else if (dist > attackRange)
        {
            attackReady = true;

            if (isJumpscareAudioPlaying)
            {
                audioSource.Stop();
                isJumpscareAudioPlaying = false;
            }
        }

        // then only check flashlight for despawn
        if (!flashlight.enabled) return;

        Vector3 toMonster = transform.position - flashlight.transform.position;
        float halfAngle = flashlight.spotAngle * 0.5f;
        float angle = Vector3.Angle(flashlight.transform.forward, toMonster);

        //if (toMonster.magnitude <= flashlight.range && angle <= halfAngle)
        //    PlayDeathAudio(); // play death audio
        //    StartCoroutine(HandleDespawn());

        if (toMonster.magnitude <= flashlight.range && angle <= halfAngle)
        {
            // Check if the flashlight is directly hitting this monster
            Ray ray = new Ray(flashlight.transform.position, toMonster.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, flashlight.range))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    PlayDeathAudio();
                    StartCoroutine(HandleDespawn());
                }
            }
        }

    }

    IEnumerator HandleDespawn(bool forceInstant = false)
    { 
        isHidden = true;
        var renderers = GetComponentsInChildren<Renderer>();
        var colliders = GetComponentsInChildren<Collider>();

        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;

        float waitTime = forceInstant ? 0f : respawnDelay;
        yield return new WaitForSeconds(respawnDelay);

        transform.position = startPosition;

        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;

        isHidden = false;
    }

    void PlayDeathAudio()
    {
        audioSource.loop = false;
        audioSource.clip = deathClip;
        audioSource.Play();
    }

    void PlayJumpscareAudio()
    {
        if (!isJumpscareAudioPlaying)
        {
            audioSource.clip = jumpscareClip;
            audioSource.loop = true;
            audioSource.Play();
            isJumpscareAudioPlaying = true;
        }
    }

    public bool IsHidden()
    {
        return isHidden;
    }

    public void ForceRespawn()
    {
        StopAllCoroutines();
        StartCoroutine(HandleDespawn(forceInstant: true));
    }

}
