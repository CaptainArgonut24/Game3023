using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Check if the player's Bat1 score is 1 or more
        if (PlayerScore.Bat1 >= 1)
        {
            // Destroy this GameObject
            Destroy(gameObject);
        }
    }
}
