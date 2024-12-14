using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int points = 10; // Points to add when the item is collected
    public AudioClip pickupSound; // Sound to play when the item is collected
    public AudioClip[] randomSoundClips; // Array of sound clips to play randomly

    private void OnTriggerEnter2D(Collider2D other)
    {


        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            if (PlayerScore.PowerUps == 20)
            {
                Destroy(gameObject);
            }
            else
            {
                // Add points to the player's score
                PlayerScore.Instance.AddPoints(points);

                // Increment heal value (you can change this to another stat if needed)
                PlayerScore.Instance.IncrementHeal(1); // You can modify this value as needed
                PlayerScore.Instance.IncrementShield(1);
                PlayerScore.Instance.IncrementNukes(1);
                PlayerScore.Instance.IncrementPowerups(1);
               

                // Play the pickup sound
                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                }

                // Play a random sound clip
                if (randomSoundClips != null && randomSoundClips.Length > 0)
                {
                    AudioClip randomClip = randomSoundClips[Random.Range(0, randomSoundClips.Length)];
                    if (randomClip != null)
                    {
                        AudioSource.PlayClipAtPoint(randomClip, transform.position);
                    }
                }

                // Optionally, destroy the item after collecting
                Destroy(gameObject);

            }

        }
    }
}
