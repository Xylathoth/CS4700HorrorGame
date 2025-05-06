using UnityEngine;

public class Shrooms : BaseConsumable
{
    public float jitterAmount = 2f; // how strong the jitter should be

    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        UIManager.Instance.ShowEffectMessage("Round 5\r\nYou took Shrooms!");
        look.isJittery = true;
        look.jitterStrength = jitterAmount;
        Debug.Log($"Jittery mouse look applied! Jitter strength: {jitterAmount}");
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        look.isJittery = false;
        look.jitterStrength = 0f;
        Debug.Log("Jittery mouse look removed!");
    }
}
