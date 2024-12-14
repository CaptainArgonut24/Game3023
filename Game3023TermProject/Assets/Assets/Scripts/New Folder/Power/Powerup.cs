using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerScore.Pu1 >= 1)
        {
            // Destroy this GameObject
            Destroy(gameObject);
        }

    }
}



