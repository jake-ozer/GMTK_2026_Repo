using UnityEngine;

public class EnemySpawningSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Circle")]
    [SerializeField] private float spawnRadius = 10f;

    [Header("Spawn Timing")]
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 5f;

    [Header("Toggle")]
    [SerializeField] private bool isSpawning = true;

    private float timer;
    private float currentInterval;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        SetNewSpawnInterval();
        
        if (isSpawning)
        {
            SpawnEnemy();
        }
    }
    
    private void OnEnable()
    {
        SandSystem.OnSandSystemTimeAtHalf += HandleHalfTime;
        SandSystem.OnSandSystemTimeAboveHalf += HandleAboveHalf;
    }

    private void OnDisable()
    {
        SandSystem.OnSandSystemTimeAtHalf -= HandleHalfTime;
        SandSystem.OnSandSystemTimeAboveHalf -= HandleAboveHalf;
    }

    private void HandleHalfTime()
    {
        SetSpawning(true);
    }

    private void HandleAboveHalf()
    {

    }

    private void Update()
    {
        if (!isSpawning || player == null || enemyPrefab == null)
            return;

        timer += Time.deltaTime;

        if (timer >= currentInterval)
        {
            SpawnEnemy();
            timer = 0f;
            SetNewSpawnInterval();
        }
    }

    private void SetNewSpawnInterval()
    {
        currentInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomPointOnCircle();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private Vector3 GetRandomPointOnCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = Mathf.Cos(angle) * spawnRadius;
        float y = Mathf.Sin(angle) * spawnRadius;

        Vector3 offset = new Vector3(x, y, 0f);
        return player.position + offset;
    }

    public void SetSpawning(bool value)
    {
        if (value && !isSpawning)
        {
            isSpawning = value;

            if (player != null && enemyPrefab != null)
            {
                SpawnEnemy();
                timer = 0f;
                SetNewSpawnInterval();
            }
        }
        else
        {
            isSpawning = value;
        }
    }

    public void ToggleSpawning()
    {
        SetSpawning(!isSpawning);
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.color = Color.red;
        
        int segments = 64;
        Vector3 prevPoint = player.position + new Vector3(spawnRadius, 0f, 0f);
        for (int i = 1; i <= segments; i++)
        {
            float angle = (i / (float)segments) * Mathf.PI * 2f;
            Vector3 newPoint = player.position + new Vector3(Mathf.Cos(angle) * spawnRadius, Mathf.Sin(angle) * spawnRadius, 0f);
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}