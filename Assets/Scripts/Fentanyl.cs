using UnityEngine;

public class CameraFlipConsumable : BaseConsumable
{
    public override void ApplyEffect(PlayerController player, MouseLook look)
    {
        look.isFlipped = true;
        Debug.Log("Camera flip applied (upside down).");
    }

    public override void RemoveEffect(PlayerController player, MouseLook look)
    {
        look.isFlipped = false;
        Debug.Log("Camera flip removed (restored upright).");
    }
}


//using UnityEngine;

//public class Fentanyl : BaseConsumable
//{
//    private Quaternion originalRotation;
//    private Transform cameraTransform;

//    public override void ApplyEffect(PlayerController player, MouseLook look)
//    {
//        cameraTransform = look.transform;
//        originalRotation = cameraTransform.localRotation;

//        // Flip the camera upside down: add 180 degrees around Z-axis
//        cameraTransform.localRotation = originalRotation * Quaternion.Euler(0, 0, 180);
//        Debug.Log("Camera flip applied (upside down).");
//    }

//    public override void RemoveEffect(PlayerController player, MouseLook look)
//    {
//        if (cameraTransform != null)
//        {
//            cameraTransform.localRotation = originalRotation;
//            Debug.Log("Camera flip removed (restored upright).");
//        }
//    }
//}
