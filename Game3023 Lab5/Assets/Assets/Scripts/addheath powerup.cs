using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int points = 10; // Points to add when the item is collected

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

            // Optionally, destroy the item after collecting
            Destroy(gameObject);
        }
    }
}
