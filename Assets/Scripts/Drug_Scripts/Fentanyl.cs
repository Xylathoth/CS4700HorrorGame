using UnityEngine;

public class CameraFlipConsumable : BaseConsumable
{
    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        UIManager.Instance.ShowEffectMessage("Round 7\r\nYou took Fentanyl!");
        look.isFlipped = true;
        Debug.Log("Camera flip applied (upside down).");


        MonsterAI[] monsters = FindObjectsOfType<MonsterAI>();
        foreach (MonsterAI monster in monsters)
        {
            monster.moveSpeed = 4f;
            monster.respawnDelay = 5f;

            if (monster.IsHidden())
            {
                monster.ForceRespawn();
            }
        }
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        look.isFlipped = false;
        Debug.Log("Camera flip removed (restored upright).");
    }
}
