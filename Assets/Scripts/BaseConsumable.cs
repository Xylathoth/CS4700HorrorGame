using UnityEngine;

public abstract class BaseConsumable : MonoBehaviour, IConsumableEffect
{
    protected PlayerController playerController;
    protected MouseLook mouseLook;
    private bool playerInRange = false;
    protected GameManager gameManager;

    public void Initialize(GameManager manager)
    {
        gameManager = manager;
    }

    void Update()
    {
        if (playerInRange)
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.roundStartSFX);
            ApplyEffect(playerController, mouseLook);

            if (gameManager != null)
            {
                gameManager.OnConsumableConsumed(this);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            mouseLook = other.GetComponentInChildren<MouseLook>();
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerController = null;
            mouseLook = null;
        }
    }

    // Must be implemented by child classes
    public abstract void ApplyEffect(PlayerController player, MouseLook look);
    public abstract void RemoveEffect(PlayerController player, MouseLook look);
}
