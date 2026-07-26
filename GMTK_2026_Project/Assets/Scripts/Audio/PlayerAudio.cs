using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] AudioClip[] playerHurtClips;
    [SerializeField] AudioClip[] depositSandClips;

    [Header("Sources")]
    [SerializeField] AudioSource dashAudioSource;
    [SerializeField] AudioSource collectSandAudioSource;
    [SerializeField] AudioSource playerHurtAudioSource;
    [SerializeField] AudioSource collectArtifactAudioSource;
    [SerializeField] AudioSource depositSandAudioSource;

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
            AudioManager.Instance.PlayPlayerHealthNotifAudio();
        }
    }

    public void PlayCollectArtifactAudio()
    {
        collectArtifactAudioSource.Play();
    }

    public void PlayDepositSandAudio()
    {
        int r = Random.Range(0,depositSandClips.Length);
        depositSandAudioSource.PlayOneShot(depositSandClips[r]);
    }
}
