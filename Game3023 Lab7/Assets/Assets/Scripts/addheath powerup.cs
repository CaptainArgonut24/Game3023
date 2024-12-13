using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int points = 10; // Points to add when the item is collected
    public AudioClip pickupSound; // Sound to play when the item is collected
    public string[] randomSoundLines; // Array of random sound lines

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

            // Play the pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Play a random sound line
            if (randomSoundLines != null && randomSoundLines.Length > 0)
            {
                string randomLine = randomSoundLines[Random.Range(0, randomSoundLines.Length)];
                Debug.Log(randomLine); // Replace this with a method to play the sound line if applicable
            }

            // Optionally, destroy the item after collecting
            Destroy(gameObject);
        }
    }
}
