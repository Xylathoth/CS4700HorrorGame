using System.Collections;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Light flashlight;

    [Header("Settings")]
    public float moveSpeed = 3f;
    public float respawnDelay = 10f;

    private Vector3 startPosition;
    private bool isHidden = false;

    void Start()
    {
        startPosition = transform.position;

        if (flashlight == null)
            Debug.LogError("MonsterDebugAI: flashlight reference is missing!");
        if (player == null)
            Debug.LogError("MonsterDebugAI: player   reference is missing!");
    }

    void Update()
    {
        if (isHidden) return;

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

        // Make the monster look toward the player
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        if (!flashlight.enabled)
            return;

        Vector3 toMonster = transform.position - flashlight.transform.position;
        float dist = toMonster.magnitude;
        float halfAngle = flashlight.spotAngle * 0.5f;
        float angle = Vector3.Angle(flashlight.transform.forward, toMonster);

        if (dist <= flashlight.range && angle <= halfAngle)
        {
            StartCoroutine(HandleDespawn());
        }
    }


    IEnumerator HandleDespawn()
    {
        isHidden = true;
        Debug.Log(">> DESPAWNING MONSTER");

        // grab all renderers and colliders in this monster
        var renderers = GetComponentsInChildren<Renderer>();
        var colliders = GetComponentsInChildren<Collider>();

        // disable visuals and collisions
        foreach (var r in renderers) r.enabled = false;
        foreach (var c in colliders) c.enabled = false;

        // wait the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // move it back home
        transform.position = startPosition;

        // re-enable visuals and collisions
        foreach (var r in renderers) r.enabled = true;
        foreach (var c in colliders) c.enabled = true;

        isHidden = false;
        Debug.Log(">> RESPAWNING MONSTER");
    }

}
