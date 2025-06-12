using UnityEngine;

public class Coin : MonoBehaviour
{
    private new ParticleSystem particleSystem; // Use 'new' to explicitly hide the inherited member
    private AudioSource audioSource;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        particleSystem.Play();
        audioSource.Play();

        Destroy(gameObject, 0.5f);
    }
}
