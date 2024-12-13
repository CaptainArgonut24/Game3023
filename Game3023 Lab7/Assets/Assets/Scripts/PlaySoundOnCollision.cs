using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public int points = 10; // Points to add when the item is collected
    public AudioClip pickupSound; // Sound to play when the item is collected
    public AudioClip[] randomSoundClips; // Array of sound clips to play randomly
    public AudioSource audioSource; // Reference to the AudioSource component
    public Transform soundSourcePosition; // Transform to specify the position of the sound source

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Add points to the player's score
            PlayerScore.Instance.AddPoints(points);

            // Increment heal value (you can change this to another stat if needed)
            PlayerScore.Instance.IncrementHeal(1); // You can modify this value as needed
            PlayerScore.Instance.IncrementShield(1);
            PlayerScore.Instance.IncrementNukes(1);

            // Play the pickup sound using the AudioSource component
            if (pickupSound != null)
            {
                audioSource.clip = pickupSound;
                audioSource.Play();
            }

            // Play a random sound clip using the AudioSource component
            if (randomSoundClips != null && randomSoundClips.Length > 0)
            {
                AudioClip randomClip = randomSoundClips[Random.Range(0, randomSoundClips.Length)];
                if (randomClip != null)
                {
                    audioSource.PlayOneShot(randomClip);
                }
            }

            // Optionally, destroy the item after collecting
            Destroy(gameObject);
        }
    }
}
