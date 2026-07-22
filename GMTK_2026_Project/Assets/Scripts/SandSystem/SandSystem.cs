using System;
using UnityEngine;
using UnityEngine.UI;

public class SandSystem : MonoBehaviour
{
    [SerializeField] private float sandTimerMax;
    [SerializeField] private float sandTimerDecrementSpeed;
    [SerializeField] private Slider sandTimerSlider;
    
    private float sandTimerCurrent;
    private bool decrementSandTimer;
    
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
        
        if (sandTimerCurrent <= 0 && decrementSandTimer)
        {
            decrementSandTimer = false;
            OnSandTimerExpired();
        }
    }

    public void ReplenishSandTimer(float amt)
    {
        sandTimerCurrent = Mathf.Clamp(sandTimerCurrent + amt, 0f, sandTimerMax);
    }
    
    private void OnSandTimerExpired()
    {
        //add logic here when ready
        Debug.Log("<color=magenta>SAND TIMER EXPIRED -> GAME OVER</color>");
    }
}
