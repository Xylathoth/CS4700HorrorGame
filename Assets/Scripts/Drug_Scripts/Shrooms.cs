using UnityEngine;

public class Shrooms : BaseConsumable
{
    public float jitterAmount = 2f; // how strong the jitter should be

    // for camera snapping
    public float snapAngleRange = 90f;
    public float snapIntervalMin = 1.5f;
    public float snapIntervalMax = 4f;

    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        UIManager.Instance.ShowEffectMessage("Round 5\r\nYou took Shrooms!\r\nForest Open!");
        look.isJittery = true;
        look.jitterStrength = jitterAmount;

        look.isSnapping = true;
        look.snapAngleRange = snapAngleRange;
        look.snapIntervalMin = snapIntervalMin;
        look.snapIntervalMax = snapIntervalMax;
        look.snapTimer = 0f; // reset
        look.nextSnapTime = Random.Range(snapIntervalMin, snapIntervalMax);

        Debug.Log($"Jittery mouse look applied! Jitter strength: {jitterAmount}");


        MonsterAI[] monsters = FindObjectsOfType<MonsterAI>();
        foreach (MonsterAI monster in monsters)
        {
            monster.moveSpeed = 5f;
            monster.respawnDelay = 999f;
        }

    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        look.isJittery = false;
        look.jitterStrength = 0f;

        look.isSnapping = false;
        look.snapAngleRange = 0f;

        Debug.Log("Jittery mouse look + random snapping effect removed!");
    }
}
