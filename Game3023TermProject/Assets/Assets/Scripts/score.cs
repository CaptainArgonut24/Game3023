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
    public static int PU1 = 0;
    public static int PU2 = 0;
    public static int PU3 = 0;
    public static int PU4 = 0;
    public static int PU5 = 0;
    public static int PU6 = 0;
    public static int PU7 = 0;
    public static int PU8 = 0;
    public static int PU9 = 0;
    public static int PU10 = 0;
    public static int PU11 = 0;
    public static int PU12 = 0;
    public static int PU13 = 0;
    public static int PU14 = 0;
    public static int PU15 = 0;
    public static int PU16 = 0;
    public static int PU17 = 0;
    public static int PU18 = 0;
    public static int PU19 = 0;


    public static int Bat1 = 0;
    public static int Bat2 = 0;
    public static int Bat3 = 0;
    public static int Bat4 = 0;
    public static int Bat5 = 0;
    public static int Bat6 = 0;
    public static int Bat7 = 0;
    public static int Bat8 = 0;
    public static int Bat9 = 0;
    public static int Bat10 = 0;
    public static int Bat11 = 0;
    public static int Bat12 = 0;
    public static int Bat13 = 0;
    public static int Bat14 = 0;
    public static int Bat15 = 0;

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
