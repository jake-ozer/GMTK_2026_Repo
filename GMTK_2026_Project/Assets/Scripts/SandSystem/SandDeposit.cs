using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandDeposit : MonoBehaviour
{
    [SerializeField] private float timeBetweenSandDeposits;
    private SandSystem sandSystem;

    private EnemySpawningSystem ess;
    
    private void Awake()
    {
        sandSystem = FindObjectOfType<SandSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerSandInventory = other.GetComponent<PlayerSandInventory>();
        if (playerSandInventory != null)
        {
            //GROSS-----
            //this is gross but idc its a game jam
            ess = FindFirstObjectByType<EnemySpawningSystem>();
            ess.enabled = false;
            ess.SetSpawning(false);
           
            //make all ghosts disappear
            LungeEnemy[] enemies = FindObjectsByType<LungeEnemy>(FindObjectsSortMode.None);
            foreach (var e in enemies)
            {
                e.StartDying();
            }
            //GROSS-----
            
            StartCoroutine(SandDepositRoutine(playerSandInventory, other.GetComponent<PlayerMovement>()));
        }
    }

    private IEnumerator SandDepositRoutine(PlayerSandInventory playerSandInventory, PlayerMovement playerMovement)
    {
        playerMovement.DisableMovement();
        
        int numSandTokens = playerSandInventory.sandPickupTimeTokens.Count;
        for (int i = 0; i < numSandTokens; i++)
        {
            yield return new WaitForSeconds(timeBetweenSandDeposits);
            float curTime = playerSandInventory.PopSandTimeTokenFromFrontOfList();
            sandSystem.ReplenishSandTimer(curTime);
        }
        
        playerMovement.EnableMovement();
        
        ess.enabled = true;
    }
}
