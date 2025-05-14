using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        var playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogWarning("AttackPlayer: no PlayerHealth on the Player object!");
            return;
        }

        playerHealth.health -= damage;
        Debug.Log($"AttackPlayer: dealt {damage} damage, player health now {playerHealth.health}");
    }
}
