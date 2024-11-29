using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance of GameManager

    [Header("Game States")]
    public bool isGameOver = false;   // Check if the game is over
    public bool isGamePaused = false; // Check if the game is paused

    [Header("UI Elements")]
    public GameObject pauseMenu;      // Reference to the pause menu
    public GameObject gameOverMenu;   // Reference to the game over menu
    public GameObject mainMenu;       // Reference to the main menu

    private void Awake()
    {
        // Set up the Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Hide menus at the start
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(true);

        // Load player data at the start
        PlayerScore.Instance.LoadPlayerData();
    }

    // Start the game (called from the main menu)
    public void StartGame()
    {
        isGameOver = false;
        isGamePaused = false;
        mainMenu.SetActive(false);    // Hide the main menu
        gameOverMenu.SetActive(false); // Hide the game over menu
        Time.timeScale = 1; // Unpause the game
    }

    // End the game (called when the game is over)
    public void EndGame()
    {
        isGameOver = true;
        gameOverMenu.SetActive(true); // Show the game over menu
        Time.timeScale = 0; // Pause the game
        PlayerScore.Instance.SavePlayerData(); // Save player data at the end of the game
    }

    // Pause or unpause the game (called when the player presses the pause button)
    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            pauseMenu.SetActive(true);  // Show the pause menu
            Time.timeScale = 0;         // Pause the game
        }
        else
        {
            pauseMenu.SetActive(false); // Hide the pause menu
            Time.timeScale = 1;         // Unpause the game
        }
    }

    // Restart the game (called from the game over menu)
    public void RestartGame()
    {
        // Reset player score and stats
        PlayerScore.Instance.SavePlayerData(); // Save current state before restarting
        PlayerScore.Instance.LoadPlayerData(); // Reload previous data (if needed)
        StartGame(); // Restart the game
    }

    // Load the main menu (called from the game over menu)
    public void LoadMainMenu()
    {
        Time.timeScale = 1; // Unpause the game if necessary
        mainMenu.SetActive(true); // Show the main menu
        gameOverMenu.SetActive(false); // Hide the game over menu
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    // Save the game at any time (can be triggered manually)
    public void SaveGame()
    {
        PlayerScore.Instance.SavePlayerData(); // Save the player's data
    }

    // Load the game from saved data (can be triggered manually)
    public void LoadGame()
    {
        PlayerScore.Instance.LoadPlayerData(); // Load the saved data
    }
}
