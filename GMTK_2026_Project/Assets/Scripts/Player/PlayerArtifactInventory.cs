using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerArtifactInventory : MonoBehaviour
{
    [SerializeField] private int maxArtifacts;
    [SerializeField] private TextMeshProUGUI artifactsFoundLabel;
    private List<Artifact> artifacts;

    public GameObject ClosedDoor;
    public GameObject OpenDoor;
    
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
        Destroy(ClosedDoor);
        Instantiate(OpenDoor, new Vector3(-4.419f, 43f, 0f), Quaternion.identity);
    }
    
}
