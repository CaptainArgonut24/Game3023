using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance; // Singleton instance
    // In PlayerScore class
    public int GetHeal() { return heal; }
    public int GetNukes() { return nukes; }
    public int GetShield() { return shield; }

    public void DecrementHeal(int amount) { heal -= amount; }
    public void DecrementNukes(int amount) { nukes -= amount; }
    public void DecrementShield(int amount) { shield -= amount; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;     // Text for displaying the score
    public TextMeshProUGUI battlesText;   // Text for displaying battles
    public TextMeshProUGUI winsText;      // Text for displaying wins
    public TextMeshProUGUI lostText;      // Text for displaying losses
    public TextMeshProUGUI accuracyText;  // Text for displaying accuracy
    public TextMeshProUGUI healText;      // Text for displaying heal
    public TextMeshProUGUI nukesText;     // Text for displaying nukes
    public TextMeshProUGUI shieldText;    // Text for displaying shields

    [Header("Player Stats")] public static int score = 0;
    public static int battles = 0;
    public static int wins = 0;
    public static int lost = 0;
    public static int heal = 0;
    public static int nukes = 0;
    public static int shield = 0;

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
        UpdateUI();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateUI();
    }

    public void IncrementBattles(int i)
    {
        battles++; // Increment battle count
        UpdateUI(); // Update UI to reflect the new battle count
    }

    public void IncrementWins(int i)
    {
        wins++;
        UpdateUI();
    }

    public void IncrementLost(int i)
    {
        lost++;
        UpdateUI();
    }

    public void IncrementHeal(int amount)
    {
        heal += amount;
        UpdateUI();
    }

    public void IncrementNukes(int i)
    {
        nukes++;
        UpdateUI();
    }

    public void IncrementShield(int i)
    {
        shield++;
        UpdateUI();
    }
    


    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (battlesText != null)
        {
            battlesText.text = "Battles: " + battles;
        }

        if (winsText != null)
        {
            winsText.text = "Wins: " + wins;
        }

        if (lostText != null)
        {
            lostText.text = "Lost: " + lost;
        }

        if (accuracyText != null)
        {
            // Avoid division by zero; calculate accuracy as wins/battles
            float accuracy = battles > 0 ? (float)wins / battles * 100 : 0f;
            accuracyText.text = "Accuracy: " + accuracy.ToString("F1") + "%";
        }

        if (healText != null)
        {
            healText.text = "Heal: " + heal;
        }

        if (nukesText != null)
        {
            nukesText.text = "Nukes: " + nukes;
        }

        if (shieldText != null)
        {
            shieldText.text = "Shield: " + shield;
        }
    }
}
