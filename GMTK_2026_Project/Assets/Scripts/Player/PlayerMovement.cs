using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 1.8f;

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;
    private bool isSprinting;
    private bool movementEnabled = true;

    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;
    private Vector2 dashDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        playerInput.actions["Dash"].performed += OnDashPerformed;
    }

    void OnDisable()
    {
        playerInput.actions["Dash"].performed -= OnDashPerformed;
    }

    void Update()
    {
        // Tick cooldown regardless of movement/dash state so it recovers consistently.
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                GetComponent<PlayerHealth>().IsInvulnerable = false;
                isDashing = false;
            }
        }

        if (!movementEnabled)
        {
            return;
        }

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        isSprinting = playerInput.actions["Sprint"].IsPressed();

        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
        }
    }

    void FixedUpdate()
    {
        if (!movementEnabled)
        {
            return;
        }

        if (isDashing)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            return;
        }

        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.linearVelocity = moveInput.normalized * currentSpeed;
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        TryDash();
    }

    private void TryDash()
    {
        if (!movementEnabled || isDashing || dashCooldownTimer > 0f)
        {
            return;
        }

        dashDirection = moveInput.sqrMagnitude > 0.01f ? moveInput.normalized : lastMoveDirection;
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
        GetComponent<PlayerHealth>().IsInvulnerable = true;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public void DisableMovement()
    {
        movementEnabled = false;
        moveInput = Vector2.zero;
        isSprinting = false;
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
    }
}