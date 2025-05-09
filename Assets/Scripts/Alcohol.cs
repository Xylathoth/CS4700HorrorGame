using UnityEngine;

public class Alcohol : BaseConsumable
{
    public float tiltStrength = 10f;
    public float tiltSpeed = 2f;

    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        // affecting vision
        look.isDizzy = true;
        look.dizzyTiltStrength = tiltStrength;
        look.dizzyTiltSpeed = tiltSpeed;

        // affecting movement
        player.isDrifting = true;
        player.driftStrength = 0.5f;

        Debug.Log("Dizzy effect applied!");
        UIManager.Instance.ShowEffectMessage("Round 6\r\nYou drank Alcohol!");
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        // affecting vision
        look.isDizzy = false;
        look.dizzyTiltStrength = 0f;
        look.dizzyTiltSpeed = 0f;

        // affecting movement
        player.isDrifting = false;
        player.driftStrength = 0f;

        Debug.Log("Dizzy effect removed!");
    }
}
