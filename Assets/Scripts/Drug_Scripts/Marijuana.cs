using UnityEngine;

public class Marijuana : BaseConsumable
{
    public float boostMultiplier = 2f;

    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        UIManager.Instance.ShowEffectMessage("Round 2\r\nYou took Marijuana!");
        player.speed *= boostMultiplier;
        Debug.Log("slow applied!");

        MonsterAI monster = FindObjectOfType<MonsterAI>();
        if (monster != null)
        {
            monster.moveSpeed = 3f;
            monster.respawnDelay = 4f;
        }


    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        player.speed /= boostMultiplier;
        Debug.Log("slow removed!");
    }
}
