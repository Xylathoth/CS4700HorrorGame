using UnityEngine;

public class MethPack : BaseConsumable
{
    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        UIManager.Instance.ShowEffectMessage("Round 6\r\nYou took Meth!");
        player.isInverted = true;
        look.isInverted = true;
        Debug.Log("Invert effect applied!");

        MonsterAI monster = FindObjectOfType<MonsterAI>();
        if (monster != null)
        {
            monster.moveSpeed = 6f;
            monster.respawnDelay = 2f;
        }
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        player.isInverted = false;
        look.isInverted = false;
        Debug.Log("Invert effect removed!");
    }
}

