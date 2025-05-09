using UnityEngine;

public class Narcan : BaseConsumable
{
    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        // Optional: you could add a fancy effect here too
        Debug.Log("Final consumable consumed!");

        UIManager.Instance.ShowEffectMessage("You found the final item!");

        // Instead of starting a new round, end the game
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        // No effect to remove in this case
    }
}
