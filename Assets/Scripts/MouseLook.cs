using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public Boolean isInverted;
    public Boolean isFlipped;

    float xRotation = 0f;

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
    }

    void NormalMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

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
