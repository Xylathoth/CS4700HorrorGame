using UnityEngine;

public class MethPack : MonoBehaviour
{
    private bool playerInRange = false;
    private MouseLook mouseLook;
    private PlayerController playerController;

    void Update()
    {
        if (playerInRange)
        {
            ApplyInversion();
        }
    }

    void ApplyInversion()
    {
        if (mouseLook != null && playerController != null)
        {
            mouseLook.isInverted = true;
            playerController.isInverted = true;
            Debug.Log("Inversion activated!");

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("MouseLook or PlayerController missing!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone");

            // Get PlayerController directly
            playerController = other.GetComponent<PlayerController>();

            // Get MouseLook from ANY child object (doesn't matter the name)
            mouseLook = other.GetComponentInChildren<MouseLook>();

            if (mouseLook == null)
            {
                Debug.LogWarning("MouseLook component not found in Player children!");
            }

            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left trigger zone");
            playerInRange = false;
            mouseLook = null;
            playerController = null;
        }
    }
}
