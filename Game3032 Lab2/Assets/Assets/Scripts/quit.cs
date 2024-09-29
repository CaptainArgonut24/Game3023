using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Method to quit the game
    public void Quit()
    {
        // Log for testing in the Unity Editor
        Debug.Log("Game is exiting...");

        // If running in a built game, this will close the application
        Application.Quit();
    }
}
