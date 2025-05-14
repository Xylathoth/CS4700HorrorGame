using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Player only has 1 health
    public bool alive = true;
    public int health = 1;

    private GameManager gameManager;

    private void Update()
    {

        if (health <= 0)
        {
            gameManager.DeadPlayer();
        }
    }
}
