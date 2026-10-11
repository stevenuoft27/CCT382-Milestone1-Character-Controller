using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // --- MOVEMENT STATS ---
    public float moveSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float gravity = -9.81f;
    public float rotationSpeed = 15f;

    // --- JUMP STATS ---
    public float jumpHeight = 2.0f;
    public float jumpBufferTime = 0.15f; // Remembers jump press for a fraction of a second
    private float jumpBufferTimer;
    private float verticalVelocity;

    // --- ROLL STATS ---
    public float rollSpeed = 9.0f;
    public float rollDuration = 0.6f;
    private bool isRolling = false;
    private float rollTimer = 0f;
    private Vector3 rollDirection;

    // --- REFERENCES ---
    public Transform cameraTransform;

    private CharacterController controller;
    private PlayerMovementInput inputScript;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputScript = GetComponent<PlayerMovementInput>();
        anim = GetComponent<Animator>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector2 input = inputScript.MoveInput;

        // Make movement relative to the camera
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camRight * input.x + camForward * input.y);

        // Ground check so we stick to the floor
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // --- JUMP BUFFERING CODE ---
        // If we press jump, start our buffer timer instead of missing the press
        if (inputScript.JumpTriggered)
        {
            jumpBufferTimer = jumpBufferTime;
            inputScript.JumpTriggered = false; // Consume the input
        }
        else if (jumpBufferTimer > 0f)
        {
            jumpBufferTimer -= Time.deltaTime;
        }
        // ---------------------------

        // ================= ROLL CODE =================
        if (isRolling)
        {
            rollTimer -= Time.deltaTime;

            Vector3 rollVelocity = (rollDirection * rollSpeed) + (Vector3.up * verticalVelocity);
            controller.Move(rollVelocity * Time.deltaTime);

            if (rollTimer <= 0f)
            {
                isRolling = false;
                anim.SetBool("IsRolling", false);
            }
            return;
        }

        if (controller.isGrounded && inputScript.RollTriggered)
        {
            isRolling = true;
            rollTimer = rollDuration;
            inputScript.RollTriggered = false;

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                rollDirection = moveDirection.normalized;
            }
            else
            {
                rollDirection = transform.forward;
            }

            transform.rotation = Quaternion.LookRotation(rollDirection);
            anim.SetBool("IsRolling", true);
            return;
        }
        // =============================================

        // Instant Jump execution using our buffer timer
        if (controller.isGrounded && jumpBufferTimer > 0f)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpBufferTimer = 0f; // Clear buffer so we don't double jump
            anim.SetTrigger("Jump");
        }

        // Apply gravity over time
        verticalVelocity += gravity * Time.deltaTime;

        // Check sprint
        float currentSpeed = moveSpeed;
        if (inputScript.SprintHeld)
        {
            currentSpeed = sprintSpeed;
        }

        // Move player
        Vector3 finalVelocity = (moveDirection * currentSpeed) + (Vector3.up * verticalVelocity);
        controller.Move(finalVelocity * Time.deltaTime);

        // Rotate character
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        anim.SetBool("IsGrounded", controller.isGrounded);
    }
}