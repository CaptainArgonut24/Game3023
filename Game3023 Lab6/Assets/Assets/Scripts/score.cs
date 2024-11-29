using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance; // Singleton instance

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI battlesText;
    public TextMeshProUGUI winsText;
    public TextMeshProUGUI lostText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI healText;
    public TextMeshProUGUI nukesText;
    public TextMeshProUGUI shieldText;

    [Header("Player Stats")]
    private int score = 0;
    private int battles = 0;
    private int wins = 0;
    private int lost = 0;
    private int heal = 0;
    private int nukes = 0;
    private int shield = 0;

    private void Awake()
    {
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
        LoadPlayerData(); // Load data at the start
    }

    // Public methods to update player stats
    public void AddPoints(int points)
    {
        score += points;
        UpdateUI();
    }

    public void IncrementBattles()
    {
        battles++;
        UpdateUI();
    }

    public void IncrementWins()
    {
        wins++;
        UpdateUI();
    }

    public void IncrementLost()
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

    // Method to get the current heal value
    public int GetHeal()
    {
        return heal;
    }

    // Method to get the current nukes value
    public int GetNukes()
    {
        return nukes;
    }

    // Method to get the current shield value
    public int GetShield()
    {
        return shield;
    }

    // Method to decrement nukes
    public void DecrementNukes(int amount)
    {
        nukes = Mathf.Max(0, nukes - amount); // Ensure nukes doesn't go below 0
        UpdateUI();
    }

    // Method to decrement heal
    public void DecrementHeal(int amount)
    {
        heal = Mathf.Max(0, heal - amount); // Ensure heal doesn't go below 0
        UpdateUI();
    }

    // Method to decrement shield
    public void DecrementShield(int amount)
    {
        shield = Mathf.Max(0, shield - amount); // Ensure shield doesn't go below 0
        UpdateUI();
    }

    // Load player data from PlayerPrefs or other data sources
    public void LoadPlayerData()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        battles = PlayerPrefs.GetInt("Battles", 0);
        wins = PlayerPrefs.GetInt("Wins", 0);
        lost = PlayerPrefs.GetInt("Lost", 0);
        heal = PlayerPrefs.GetInt("Heal", 0);
        nukes = PlayerPrefs.GetInt("Nukes", 0);
        shield = PlayerPrefs.GetInt("Shield", 0);

        UpdateUI(); // Update UI after loading data
    }

    // Save player data to PlayerPrefs or other data sources
    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Battles", battles);
        PlayerPrefs.SetInt("Wins", wins);
        PlayerPrefs.SetInt("Lost", lost);
        PlayerPrefs.SetInt("Heal", heal);
        PlayerPrefs.SetInt("Nukes", nukes);
        PlayerPrefs.SetInt("Shield", shield);

        PlayerPrefs.Save(); // Ensure the data is saved immediately
    }

    // Update all UI elements
    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (battlesText != null)
            battlesText.text = "Battles: " + battles;

        if (winsText != null)
            winsText.text = "Wins: " + wins;

        if (lostText != null)
            lostText.text = "Lost: " + lost;

        if (accuracyText != null)
        {
            float accuracy = battles > 0 ? (float)wins / battles * 100 : 0f;
            accuracyText.text = "Accuracy: " + accuracy.ToString("F1") + "%";
        }

        if (healText != null)
            healText.text = "Heal: " + heal;

        if (nukesText != null)
            nukesText.text = "Nukes: " + nukes;

        if (shieldText != null)
            shieldText.text = "Shield: " + shield;
    }
}
