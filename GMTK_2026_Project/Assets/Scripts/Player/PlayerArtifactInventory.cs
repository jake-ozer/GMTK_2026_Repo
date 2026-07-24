using System.Collections.Generic;
using UnityEngine;

public class PlayerArtifactInventory : MonoBehaviour
{
    [SerializeField] private int maxArtifacts;
    private List<Artifact> artifacts;
    
    private void Awake()
    {
        artifacts = new List<Artifact>();
    }
    
    public void AddArtifact(Artifact artifact)
    {
        artifacts.Add(artifact);
        if (artifacts.Count >= maxArtifacts)
        {
            OnAllArtifactsCollected();
        }
    }

    private void OnAllArtifactsCollected()
    {
        //game win logic here
        Debug.Log("<color=green>All artifacts collected. Game win.</color>");
    }
    
}
