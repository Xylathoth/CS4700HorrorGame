using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public GameObject zombie;         // the zombie prefab in bed
    public AudioSource scareAudio;    // jumpscare scream
    public float scareDuration = 3f;  // how long the scare lasts

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            TriggerScare();
        }
    }

    void TriggerScare()
    {
        if (zombie != null)
            zombie.SetActive(true); // zombie now appears and auto-plays attack anim

        if (scareAudio != null)
            scareAudio.Play();

        Invoke(nameof(EndScare), scareDuration);
    }

    void EndScare()
    {
        if (zombie != null)
            zombie.SetActive(false); // hide again or keep if you want lingering effect
    }
}
