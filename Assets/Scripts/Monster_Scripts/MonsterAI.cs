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

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        monsterLayerMask = LayerMask.GetMask("Monster");
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
        }
        else if (dist > attackRange)
        {
            attackReady = true;
        }

        // then only check flashlight for despawn
        if (!flashlight.enabled) return;

        Vector3 toMonster = transform.position - flashlight.transform.position;
        float halfAngle = flashlight.spotAngle * 0.5f;
        float angle = Vector3.Angle(flashlight.transform.forward, toMonster);

        if (toMonster.magnitude <= flashlight.range && angle <= halfAngle)
            StartCoroutine(HandleDespawn());
    }

    IEnumerator HandleDespawn()
    {
        isHidden = true;
        var renderers = GetComponentsInChildren<Renderer>();
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;
        yield return new WaitForSeconds(respawnDelay);
        transform.position = startPosition;
        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;
        isHidden = false;
    }
}
