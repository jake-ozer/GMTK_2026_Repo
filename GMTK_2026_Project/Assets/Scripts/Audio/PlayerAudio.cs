using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] AudioClip[] playerHurtClips;

    [Header("Sources")]
    [SerializeField] AudioSource dashAudioSource;
    [SerializeField] AudioSource collectSandAudioSource;
    [SerializeField] AudioSource playerHurtAudioSource;

    public void PlayDashAudio()
    {
        dashAudioSource.Play();
    }

    public void PlayCollectSandAudio()
    {
        collectSandAudioSource.Play();
    }

    public void PlayPlayerHurtAudio(int currentHealth)
    {
        int r = Random.Range(0,playerHurtClips.Length);
        playerHurtAudioSource.PlayOneShot(playerHurtClips[r]);

        if (currentHealth == 1)
        {
            
        }
    }
}
