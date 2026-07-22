using System;
using UnityEngine;

public class SandPickup : MonoBehaviour
{
    public float sandPickupValue;
    
    private void Start()
    {
        GetComponentInChildren<Animator>().Play(0, -1, UnityEngine.Random.Range(0f, 1f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerSandInventory = other.GetComponent<PlayerSandInventory>();
        if (playerSandInventory != null)
        {
            OnSandPickup(playerSandInventory);
        }
    }

    private void OnSandPickup(PlayerSandInventory playerSandInventory)
    {
        playerSandInventory.AddSandTimeToken(sandPickupValue);
        Destroy(gameObject);
    }
}
