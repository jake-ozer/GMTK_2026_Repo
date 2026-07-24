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
        //add logic here when ready
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Debug.Log("<color=magenta>SAND TIMER EXPIRED -> GAME OVER</color>");
    }
}