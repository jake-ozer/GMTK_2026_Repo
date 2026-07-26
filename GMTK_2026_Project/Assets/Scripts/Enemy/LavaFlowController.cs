using UnityEngine;
using static Unity.Cinemachine.CinemachineFreeLookModifier;

public class LavaFlowController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeLavaDamage();
            }
        }
    }
}
