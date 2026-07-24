using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSandInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sandTokenText;
    
    //each entry is a float for how much time each sand token is worth
    public List<float> sandPickupTimeTokens;

    private void Start()
    {
        
    }
    
    private void Update()
    {
        sandTokenText.text = "x"+ sandPickupTimeTokens.Count;
    }
    
    public void AddSandTimeToken(float time)
    {
        sandPickupTimeTokens.Add(time);
    }

    public float PopSandTimeTokenFromFrontOfList()
    {
        if (sandPickupTimeTokens.Count == 0)
        {
            Debug.LogWarning("Tried to pop from empty sand token list.");
            return 0f;
        }

        float cur = sandPickupTimeTokens[0];
        sandPickupTimeTokens.RemoveAt(0);
        return cur;
    }
}
