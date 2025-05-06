using UnityEngine;

public class LSD : BaseConsumable
{
    public float reducedFOV = 30f; // Desired FOV (default is ~60)
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
