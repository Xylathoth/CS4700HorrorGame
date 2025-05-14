using UnityEngine;

public class CameraFlipConsumable : BaseConsumable
{
    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        UIManager.Instance.ShowEffectMessage("Round 7\r\nYou took Fentanyl!");
        look.isFlipped = true;
        Debug.Log("Camera flip applied (upside down).");
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        look.isFlipped = false;
        Debug.Log("Camera flip removed (restored upright).");
    }
}
