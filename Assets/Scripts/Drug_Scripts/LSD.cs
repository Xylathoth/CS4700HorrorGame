using UnityEngine;

public class LSD : BaseConsumable
{
    public float reducedFOV = 30f; // desired FOV
    private float originalFOV;
    private Camera playerCam;

    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        playerCam = look.GetComponent<Camera>();

        if (playerCam != null)
        {
            originalFOV = playerCam.fieldOfView;
            playerCam.fieldOfView = reducedFOV;
            Debug.Log($"Reduced FOV applied! New FOV: {reducedFOV}");
        }
        else
        {
            Debug.LogWarning("Camera not found on MouseLook object!");
        }
        UIManager.Instance.ShowEffectMessage("Round 3\r\nYou took LSD!");

        //MonsterAI monster = FindObjectOfType<MonsterAI>();
        //if (monster != null)
        //{
        //    monster.moveSpeed = 4f;
        //    monster.respawnDelay = 3f;
        //}

        MonsterAI[] monsters = FindObjectsOfType<MonsterAI>();
        foreach (MonsterAI monster in monsters)
        {
            monster.moveSpeed = 4f;
            monster.respawnDelay = 3f;
        }

    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        if (playerCam != null)
        {
            playerCam.fieldOfView = originalFOV;
            Debug.Log("Reduced FOV removed. Restored original FOV.");
        }
    }
}
