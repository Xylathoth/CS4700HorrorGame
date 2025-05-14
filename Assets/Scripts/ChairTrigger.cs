using UnityEngine;

public class ChairTrigger : MonoBehaviour
{
    public Rigidbody chairRigidbody;
    public float forceAmount = 5f;
    public Vector3 forceDirection = new Vector3(1f, 0f, 0f); // tweak this to push the chair over
    public AudioSource whisperAudio;

    private bool hasFallen = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasFallen) return;

        if (other.CompareTag("Player"))
        {
            if (whisperAudio != null)
                whisperAudio.Play();

            if (chairRigidbody != null)
            {
                chairRigidbody.isKinematic = false; // enable physics

                // Optional: apply directional push to tip the chair over
                chairRigidbody.AddForce(forceDirection.normalized * forceAmount, ForceMode.Impulse);
            }

            hasFallen = true;
        }
    }
}
