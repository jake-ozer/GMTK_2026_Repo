using UnityEngine;

public class Artifact : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var playerArtifactInventory = other.GetComponent<PlayerArtifactInventory>();
        if (playerArtifactInventory)
        {
            gameObject.SetActive(false);
            playerArtifactInventory.AddArtifact(this);
        }
    }
}
