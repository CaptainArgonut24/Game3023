using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class TilemapTriggerUI4 : MonoBehaviour
{
    public GameObject uiElement; // Assign the UI element in the Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the tilemap has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerScore.Bat4 >= 1)
            {
                // Destroy this GameObject
                Destroy(gameObject);
            }
            else
            {
                PlayerScore.Instance.IncrementB4(1);
                // Enable the UI element when the player collides with the tilemap
                uiElement.SetActive(true);

            }

        }
    }

   
}
