using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprintMultiplier = 1.8f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isSprinting;
    private bool movementEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (!movementEnabled)
        {
            return;
        }

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        isSprinting = playerInput.actions["Sprint"].IsPressed();
    }

    void FixedUpdate()
    {
        if (!movementEnabled)
        {
            return;
        }

        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        rb.linearVelocity = moveInput.normalized * currentSpeed;
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
        rb.linearVelocity = Vector2.zero;
    }
}