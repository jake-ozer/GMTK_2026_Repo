using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] footsteps;

    [Range(0.5f,4f)]
    [SerializeField] float distanceThreshold = 2.5f;

    private AudioSource source;

    private float distanceMoved = 0f;
    private Vector3 oldPosition;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (distanceMoved > distanceThreshold)
        {
            distanceMoved = 0;

            int r = Random.Range(0,footsteps.Length);
            source.PlayOneShot(footsteps[r]);

        }

        distanceMoved += Vector3.Magnitude(transform.position - oldPosition);
        oldPosition = transform.position;
    }
}
