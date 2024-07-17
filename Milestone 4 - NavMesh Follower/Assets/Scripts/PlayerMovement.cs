using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Player Settings")]
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f, jumpHeight = 3f;
    Vector3 startingPos = new Vector3(3f, 1.033f, 7f);
    bool isGrounded;

    [Header("Checker Settings")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.4f;

    // PHYSICS VARIABLES
    const float gravity = -9.81f * 2f;
    Vector3 velocity;

    void LateUpdate() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // makes the velocity.y not reach very low negatives
        if (isGrounded && velocity.y < 0f) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // basic jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        // gravity logic
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // in case you fall out of bounds
        if (transform.position.y < -20f)
            transform.position = startingPos;
    }
} 
