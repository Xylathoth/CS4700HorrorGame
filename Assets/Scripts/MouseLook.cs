using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    // for inverted and flipped camera effects
    public Boolean isInverted;
    public Boolean isFlipped;

    // for camera shake effect
    public bool isJittery = false;
    public float jitterStrength = 2f;

    // for dizzy effect
    public bool isDizzy = false;
    public float dizzyTiltStrength = 10f; // degrees
    public float dizzyTiltSpeed = 2f;     // speed of sway
    private float dizzyTime = 0f;         // internal timer

    // for jitter and random snapping effect
    public bool isSnapping = false;
    public float snapIntervalMin = 1.5f;
    public float snapIntervalMax = 4f;
    public float snapAngleRange = 90f;

    [HideInInspector] public float snapTimer = 0f;
    [HideInInspector] public float nextSnapTime = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        isInverted = false;
        isFlipped = false;

    }

    void Update()
    {
        if (isInverted)
        {
            InvertedMovement();
        }
        else
        {
            NormalMovement();
        }

        if (isDizzy)
        {
            dizzyTime += Time.deltaTime * dizzyTiltSpeed;
            float zTilt = Mathf.Sin(dizzyTime) * dizzyTiltStrength;
            transform.localRotation *= Quaternion.Euler(0f, 0f, zTilt); // apply Z-axis tilt to camera
        }


        if (isSnapping)
        {
            snapTimer += Time.deltaTime;

            if (snapTimer >= nextSnapTime)
            {
                // random snap
                float yawSnap = UnityEngine.Random.Range(-snapAngleRange, snapAngleRange);
                float pitchSnap = UnityEngine.Random.Range(-10f, 10f); // small pitch change

                xRotation += pitchSnap;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                playerBody.Rotate(Vector3.up * yawSnap);

                // reset timer
                snapTimer = 0f;
                nextSnapTime = UnityEngine.Random.Range(snapIntervalMin, snapIntervalMax);

                Debug.Log($"Camera snap applied");
            }
        }

    }

    void NormalMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (isJittery)
        {
            mouseX += UnityEngine.Random.Range(-jitterStrength, jitterStrength) * Time.deltaTime;
            mouseY += UnityEngine.Random.Range(-jitterStrength, jitterStrength) * Time.deltaTime;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Quaternion baseRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if (isFlipped)
        {
            baseRotation *= Quaternion.Euler(0, 0, 180); // flip upside down
        }
        transform.localRotation = baseRotation;

        playerBody.Rotate(Vector3.up * mouseX);
    }

    void InvertedMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (isJittery)
        {
            mouseX += UnityEngine.Random.Range(-jitterStrength, jitterStrength) * Time.deltaTime;
            mouseY += UnityEngine.Random.Range(-jitterStrength, jitterStrength) * Time.deltaTime;
        }


        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Quaternion baseRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if (isFlipped)
        {
            baseRotation *= Quaternion.Euler(0, 0, 180); // flip upside down
        }
        transform.localRotation = baseRotation;


        playerBody.Rotate(Vector3.down * mouseX);
    }


}
