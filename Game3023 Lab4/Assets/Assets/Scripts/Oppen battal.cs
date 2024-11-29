using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class TilemapTriggerUI : MonoBehaviour
{
    public GameObject uiElement; // Assign the UI element in the Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the tilemap has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enable the UI element when the player collides with the tilemap
            uiElement.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Disable the UI element when the player exits the collision
        if (collision.gameObject.CompareTag("Player"))
        {
            uiElement.SetActive(false);
        }
    }
}
