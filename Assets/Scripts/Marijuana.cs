using UnityEngine;

public class Marijuana : BaseConsumable
{
    public float boostMultiplier = 2f;

    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        UIManager.Instance.ShowEffectMessage("Round 2\r\nYou took Marijuana!");
        player.speed *= boostMultiplier;
        Debug.Log("Speed boost applied!");
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        player.speed /= boostMultiplier;
        Debug.Log("Speed boost removed!");
    }
}
