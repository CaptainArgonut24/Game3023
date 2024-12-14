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
    public static int Pu1 = 0;
    public static int Pu2 = 0;
    public static int Pu3 = 0;
    public static int Pu4 = 0;
    public static int Pu5 = 0;
    public static int Pu6 = 0;
    public static int Pu7 = 0;
    public static int Pu8 = 0;
    public static int Pu9 = 0;
    public static int Pu10 = 0;
    public static int Pu11 = 0;
    public static int Pu12 = 0;
    public static int Pu13 = 0;
    public static int Pu14 = 0;
    public static int Pu15 = 0;
    public static int Pu16 = 0;
    public static int Pu17 = 0;
    public static int Pu18 = 0;
    public static int Pu19 = 0;
    public static int PowerUps = 0;


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
        UpdateUi();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateUi();
    }

    public void IncrementBattles(int i)
    {
        battles++; // Increment battle count
        UpdateUi(); // Update UI to reflect the new battle count
    }

    public void IncrementWins(int i)
    {
        wins++;
        UpdateUi();
    }

    public void IncrementLost(int i)
    {
        lost++;
        UpdateUi();
    }

    public void IncrementHeal(int amount)
    {
        heal += amount;
        UpdateUi();
    }

    public void IncrementNukes(int i)
    {
        nukes++;
        UpdateUi();
    }

    public void IncrementShield(int i)
    {
        shield++;
        UpdateUi();
    }

    public void IncrementB1(int i)
    {
        Bat1++;
        UpdateUi();
    }
    public void IncrementB2(int i)
    {
        Bat2++;
        UpdateUi();
    }
    public void IncrementB3(int i)
    {
        Bat3++;
        UpdateUi();
    }
    public void IncrementB4(int i)
    {
        Bat4++;
        UpdateUi();
    }
    public void IncrementB5(int i)
    {
        Bat5++;
        UpdateUi();
    }
    public void IncrementB6(int i)
    {
        Bat6++;
        UpdateUi();
    }
    public void IncrementB7(int i)
    {
        Bat7++;
        UpdateUi();
    }
    public void IncrementB8(int i)
    {
        Bat8++;
        UpdateUi();
    }
    public void IncrementB9(int i)
    {
        Bat9++;
        UpdateUi();
    }
    public void IncrementB10(int i)
    {
        Bat10++;
        UpdateUi();
    }
    public void IncrementB11(int i)
    {
        Bat11++;
        UpdateUi();
    }
    public void IncrementB12(int i)
    {
        Bat12++;
        UpdateUi();
    }
    public void IncrementB13(int i)
    {
        Bat13++;
        UpdateUi();
    }

    public void IncrementB14(int i)
    {
        Bat14++;
        UpdateUi();
    }
    public void IncrementB15(int i)
    {
        Bat15++;
        UpdateUi();
    }

    public void IncrementPu1(int i)
    {
        Pu1++;
        UpdateUi();
    }
    public void IncrementPu2(int i)
    {
        Pu2++;
        UpdateUi();
    }
    public void IncrementPu3(int i)
    {
        Pu3++;
        UpdateUi();
    }
    public void IncrementPu4(int i)
    {
        Pu4++;
        UpdateUi();
    }
    public void IncrementPu5(int i)
    {
        Pu5++;
        UpdateUi();
    }
    public void IncrementPu6(int i)
    {
        Pu6++;
        UpdateUi();
    }
    public void IncrementPu7(int i)
    {
        Pu7++;
        UpdateUi();
    }
    public void IncrementPu8(int i)
    {
        Pu8++;
        UpdateUi();
    }
    public void IncrementPu9(int i)
    {
        Pu9++;
        UpdateUi();
    }
    public void IncrementPu10(int i)
    {
        Pu10++;
        UpdateUi();
    }
    public void IncrementPu11(int i)
    {
        Pu11++;
        UpdateUi();
    }
    public void IncrementPu12(int i)
    {
        Pu12++;
        UpdateUi();
    }
    public void IncrementPu13(int i)
    {
        Pu13++;
        UpdateUi();
    }
    public void IncrementPu14(int i)
    {
        Pu14++;
        UpdateUi();
    }
    public void IncrementPu15(int i)
    {
        Pu15++;
        UpdateUi();
    }
    public void IncrementPu16(int i)
    {
        Pu16++;
        UpdateUi();
    }
    public void IncrementPu17(int i)
    {
        Pu17++;
        UpdateUi();
    }
    public void IncrementPu18(int i)
    {
        Pu18++;
        UpdateUi();
    }
    public void IncrementPu19(int i)
    {
        Pu19++;
        UpdateUi();
    }

    public void IncrementPowerups(int i)
    {
        PowerUps++;
        UpdateUi();
    }

    private void UpdateUi()
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
