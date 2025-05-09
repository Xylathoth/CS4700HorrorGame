using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public Boolean isInverted;

    // for dizzy effect
    public bool isDrifting = false;
    public float driftStrength = 1.0f;
    private float driftTime = 0f;


    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        isInverted = false;
    }

    void Update()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isInverted)
        {
            InvertedMovement();
        }
        else
        {
            NormalMovement();
        }

        ApplyGravityAndJump();

    }


    void NormalMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // for dizzy effect
        if (isDrifting)
        {
            driftTime += Time.deltaTime * 2f; // speed of sway
            float driftOffset = Mathf.Sin(driftTime) * driftStrength;
            move += transform.right * driftOffset;
        }


        controller.Move(move * speed * Time.deltaTime);
    }

    void InvertedMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * -x + transform.forward * -z; // invert both x and z
        controller.Move(move * speed * Time.deltaTime);
    }

    void ApplyGravityAndJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (velocity.y < 0)
        {
            velocity.y += gravity * 2f * Time.deltaTime; // faster falling
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
