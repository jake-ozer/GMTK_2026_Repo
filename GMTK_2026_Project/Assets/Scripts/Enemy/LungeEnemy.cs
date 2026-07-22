using UnityEngine;

public class LungeEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform player; 

    [Header("Follow Settings")]
    public float followSpeed = 3f;
    public float triggerDistance = 3f; 

    [Header("Dash Settings")]
    public float waitBeforeDash = 1.2f; 
    public float dashSpeed = 14f;
    public float dashDuration = 0.4f;    
    public float dashOvershoot = 2f;  

    [Header("Dash Cooldown")]
    public float dashCooldown = 3f; 

    private enum State { Following, Waiting, Dashing }
    private State currentState = State.Following;

    private float stateTimer;
    private float dashCooldownTimer;
    private Vector2 dashDirection;
    private Vector2 dashTarget;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        
        if (currentState != State.Dashing && dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        switch (currentState)
        {
            case State.Following:
                HandleFollowing();
                break;
            case State.Waiting:
                HandleWaiting();
                break;
            case State.Dashing:
                HandleDashing();
                break;
        }
    }

    void HandleFollowing()
    {
        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance <= triggerDistance && dashCooldownTimer <= 0f)
        {
            currentState = State.Waiting;
            stateTimer = waitBeforeDash;
            return;
        }

        Vector2 direction = toPlayer.normalized;
        transform.position += (Vector3)(direction * followSpeed * Time.deltaTime);
    }

    void HandleWaiting()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0f)
        {
            StartDash();
        }
    }

    void StartDash()
    {
        Vector2 toPlayer = (Vector2)player.position - (Vector2)transform.position;
        dashDirection = toPlayer.normalized;
        
        dashTarget = (Vector2)player.position + dashDirection * dashOvershoot;

        stateTimer = dashDuration;
        currentState = State.Dashing;
    }

    void HandleDashing()
    {
        transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
        stateTimer -= Time.deltaTime;

        //bool reachedTarget = Vector2.Distance(transform.position, dashTarget) < 0.1f;
        if (stateTimer <= 0f)
        {
            dashCooldownTimer = dashCooldown;
            currentState = State.Following;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.TakeDamage();
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}