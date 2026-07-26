using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerArtifactInventory : MonoBehaviour
{
    [SerializeField] private int maxArtifacts;
    [SerializeField] private TextMeshProUGUI artifactsFoundLabel;
    private List<Artifact> artifacts;
    
    private void Awake()
    {
        artifacts = new List<Artifact>();
    }

    void Update()
    {
        artifactsFoundLabel.text = "Artifacts Found: "+artifacts.Count + "/" + maxArtifacts;
    }
    
    public void AddArtifact(Artifact artifact)
    {
        artifacts.Add(artifact);
        GetComponentInChildren<PlayerAudio>().PlayCollectArtifactAudio();
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
