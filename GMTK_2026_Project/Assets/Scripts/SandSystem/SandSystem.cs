using System;
using UnityEngine;
using UnityEngine.UI;

public class SandSystem : MonoBehaviour
{
    [SerializeField] private float sandTimerMax;
    [SerializeField] private float sandTimerDecrementSpeed;
    [SerializeField] private Slider sandTimerSlider;

    public static event Action OnSandSystemTimeAtHalf;
    public static event Action OnSandSystemTimeAboveHalf;

    private float sandTimerCurrent;
    private bool decrementSandTimer;
    private bool hasFiredHalfEvent;

    public GameObject LavaBalls;

    void Start()
    {
        sandTimerCurrent = sandTimerMax;
        decrementSandTimer = true;
        
        sandTimerSlider.maxValue = sandTimerMax;
    }
    
    private void Update()
    {
        sandTimerSlider.value = sandTimerCurrent;
        
        if (decrementSandTimer)
        {
            sandTimerCurrent -= sandTimerDecrementSpeed * Time.deltaTime;
        }

        if (!hasFiredHalfEvent && sandTimerCurrent <= sandTimerMax * 0.5f)
        {
            hasFiredHalfEvent = true;
            OnSandSystemTimeAtHalf?.Invoke();
        }
        
        if (sandTimerCurrent <= 0 && decrementSandTimer)
        {
            decrementSandTimer = false;
            OnSandTimerExpired();
        }
    }

    public void ReplenishSandTimer(float amt)
    {
        sandTimerCurrent = Mathf.Clamp(sandTimerCurrent + amt, 0f, sandTimerMax);

        if (hasFiredHalfEvent && sandTimerCurrent > sandTimerMax * 0.5f)
        {
            hasFiredHalfEvent = false;
            OnSandSystemTimeAboveHalf?.Invoke();
        }
    }
    
    private void OnSandTimerExpired()
    {
        Instantiate(LavaBalls, new Vector3(11.91f, -22.1f, 0f), Quaternion.identity);
        Instantiate(LavaBalls, new Vector3(-11.02f, -35.16f, 0f), Quaternion.identity);
        Instantiate(LavaBalls, new Vector3(15.85f, -1.64f, 0f), Quaternion.identity);
        Instantiate(LavaBalls, new Vector3(-34.8f, 11.3f, 0f), Quaternion.identity);
        Instantiate(LavaBalls, new Vector3(-3.25f, 17.38f, 0f), Quaternion.identity);

        Destroy(gameObject);
    }
}