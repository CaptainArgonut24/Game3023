using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance; // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro element for displaying the score

    private int score = 0; // Current score

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
        UpdateScoreText();
    }

    public void AddPoints(int points)
    {
        score += points; // Add points to the score
        UpdateScoreText(); // Update the UI
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the score display
        }
    }
}
