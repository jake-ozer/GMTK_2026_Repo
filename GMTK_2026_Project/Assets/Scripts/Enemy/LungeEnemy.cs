using UnityEngine;

public class LungeEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    [SerializeField] private SpriteRenderer spriteRenderer;

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

    [Header("Game Lifetime")]
    public float totalLifetime = 10f;
    public float fadeDuration = 1f;
    
    private enum State { Following, Waiting, Dashing, Dying }
    private State currentState = State.Following;

    private float stateTimer;
    private float dashCooldownTimer;
    private Vector2 dashDirection;
    private Vector2 dashTarget;
    private float lifeTimer;
    
    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;

        if (currentState != State.Dying && lifeTimer >= totalLifetime - fadeDuration)
        {
            StartDying();
        }

        if (currentState == State.Dying)
        {
            HandleDying();
            return;
        }

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
        
        if (stateTimer <= 0f)
        {
            dashCooldownTimer = dashCooldown;
            currentState = State.Following;
        }
    }

    void StartDying()
    {
        currentState = State.Dying;
        stateTimer = fadeDuration;
    }

    void HandleDying()
    {
        stateTimer -= Time.deltaTime;

        float t = fadeDuration > 0f ? Mathf.Clamp01(stateTimer / fadeDuration) : 0f;
        SetAlpha(t);

        if (stateTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void SetAlpha(float alpha)
    {
        if (spriteRenderer == null)
            return;

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentState == State.Dying) return;

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