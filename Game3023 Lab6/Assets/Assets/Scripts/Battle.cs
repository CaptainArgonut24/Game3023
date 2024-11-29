using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI enemyHPText;
    public TextMeshProUGUI enemyNameText;
    public Button actionButton1;
    public Button actionButton2;
    public Button actionButton3;
    public Button actionButton4;
    public Button healButton;
    public Button nukeButton;
    public Button shieldButton;
    public GameObject battleUIParent;
    public GameObject triggerGameObject;

    [Header("Enemy Visuals")]
    public List<Sprite> enemyImages;
    public Image enemyImageDisplay;
    public List<string> enemyNames;

    [Header("Player Stats")]
    public int playerHP = 100;
    public int enemyHP = 100;
    private int shieldRounds = 0;

    private bool isPlayerTurn = true;
    private string[] randomDialogMessages = {
        "Keep going!",
        "You can do this!",
        "What a move!",
        "Watch out!",
        "A critical moment!"
    };

    void Start()
    {
        DisplayRandomEnemy();
        UpdateUI();
        InitializeButtons();
        UpdatePowerUpButtons();
        DisplayMessage("Battle Start! Choose an action.");
    }

    void UpdateUI()
    {
        playerHPText.text = "Player HP: " + playerHP;
        enemyHPText.text = "Enemy HP: " + enemyHP;
    }

    void DisplayRandomEnemy()
    {
        if (enemyImages.Count > 0 && enemyNames.Count > 0)
        {
            int randomIndex = Random.Range(0, enemyImages.Count);
            enemyImageDisplay.sprite = enemyImages[randomIndex];
            enemyImageDisplay.enabled = true;
            enemyNameText.text = "Enemy: " + enemyNames[randomIndex];
        }
        else
        {
            Debug.LogWarning("Ensure enemy images and names are assigned!");
        }
    }

    void InitializeButtons()
    {
        actionButton1.onClick.AddListener(() => OnAttackButtonClicked("Tackle", 20));
        actionButton2.onClick.AddListener(() => OnAttackButtonClicked("Flame Thrower", 25));
        actionButton3.onClick.AddListener(() => OnAttackButtonClicked("Water Gun", 15));
        actionButton4.onClick.AddListener(() => OnDefendButtonClicked());

        healButton.onClick.AddListener(UseHeal);
        nukeButton.onClick.AddListener(UseNuke);
        shieldButton.onClick.AddListener(UseShield);

        UpdateButtonTexts();
    }

    void UpdateButtonTexts()
    {
        actionButton1.GetComponentInChildren<TextMeshProUGUI>().text = "Tackle";
        actionButton2.GetComponentInChildren<TextMeshProUGUI>().text = "Flame Thrower";
        actionButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Water Gun";
        actionButton4.GetComponentInChildren<TextMeshProUGUI>().text = "Defend";
    }

    void UpdatePowerUpButtons()
    {
        int heal = PlayerScore.Instance.GetHeal(); // Get heal from PlayerScore
        int nukes = PlayerScore.Instance.GetNukes(); // Get nukes from PlayerScore
        int shields = PlayerScore.Instance.GetShield(); // Get shield from PlayerScore

        healButton.GetComponentInChildren<TextMeshProUGUI>().text = "Heal (" + heal + ")";
        nukeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Nuke (" + nukes + ")";
        shieldButton.GetComponentInChildren<TextMeshProUGUI>().text = "Shield (" + shields + ")";

        healButton.image.color = heal > 0 ? Color.green : Color.red;
        nukeButton.image.color = nukes > 0 ? Color.green : Color.red;
        shieldButton.image.color = shields > 0 ? Color.green : Color.red;
    }

    void OnAttackButtonClicked(string attackName, int damage)
    {
        if (!isPlayerTurn) return;

        enemyHP -= damage;
        DisplayMessage("Player used " + attackName + "! Enemy took " + damage + " damage.");
        isPlayerTurn = false;
        UpdateUI();

        if (CheckBattleOutcome()) return;
        Invoke("EnemyTurn", 2f);
    }

    void OnDefendButtonClicked()
    {
        if (!isPlayerTurn) return;

        DisplayMessage("Player used Defend! Reducing damage for next attack.");
        isPlayerTurn = false;

        Invoke("EnemyTurn", 2f);
    }

    void UseHeal()
    {
        int heal = PlayerScore.Instance.GetHeal();
        if (heal > 0 && playerHP < 100)
        {
            playerHP = 100;
            PlayerScore.Instance.DecrementHeal(1); // Decrease heal after use
            DisplayMessage("Player used Heal! Restored to full health.");
            UpdatePowerUpButtons();
            UpdateUI();
        }
    }

    void UseNuke()
    {
        int nukes = PlayerScore.Instance.GetNukes();
        if (nukes > 0)
        {
            enemyHP -= 50;
            PlayerScore.Instance.DecrementNukes(1); // Decrease nukes after use
            DisplayMessage("Player used Nuke! Enemy took massive damage.");
            UpdatePowerUpButtons();
            UpdateUI();

            if (CheckBattleOutcome()) return;
            Invoke("EnemyTurn", 2f);
        }
    }

    void UseShield()
    {
        int shields = PlayerScore.Instance.GetShield();
        if (shields > 0)
        {
            shieldRounds = 4;
            PlayerScore.Instance.DecrementShield(1); // Decrease shield after use
            DisplayMessage("Player used Shield! Protected for 4 rounds.");
            UpdatePowerUpButtons();
        }
    }

    void EnemyTurn()
    {
        if (shieldRounds > 0)
        {
            shieldRounds--;
            DisplayMessage("Enemy attacks, but Shield blocked the damage!");
        }
        else
        {
            int enemyAttackDamage = Random.Range(10, 30);
            playerHP -= enemyAttackDamage;
            DisplayMessage("Enemy attacks! Player took " + enemyAttackDamage + " damage.");
        }

        isPlayerTurn = true;
        UpdateUI();

        if (!CheckBattleOutcome())
        {
            DisplayMessage(GetRandomDialogMessage() + " Your turn! Choose an action.");
        }
    }

    bool CheckBattleOutcome()
    {
        if (enemyHP <= 0)
        {
            DisplayMessage("Enemy defeated! You win!");
            PlayerScore.Instance.IncrementWins(); // Increment wins in PlayerScore
            PlayerScore.Instance.AddPoints(169); // Add points for winning
            PlayerScore.Instance.IncrementBattles(); // Increment battles count
            Invoke("EndBattle", 3f);
            return true;
        }
        else if (playerHP <= 0)
        {
            DisplayMessage("Player was defeated! You lose!");
            PlayerScore.Instance.IncrementLost(); // Increment losses in PlayerScore
            PlayerScore.Instance.AddPoints(-100); // Deduct points for losing
            PlayerScore.Instance.IncrementBattles(); // Increment battles count
            Invoke("EndBattle", 3f);
            return true;
        }
        return false;
    }


    void EndBattle()
    {
        if (battleUIParent != null)
        {
            Destroy(battleUIParent);
        }

        if (triggerGameObject != null)
        {
            Destroy(triggerGameObject);
        }
    }

    void DisplayMessage(string message)
    {
        dialogText.text = message;
    }

    string GetRandomDialogMessage()
    {
        return randomDialogMessages[Random.Range(0, randomDialogMessages.Length)];
    }
}
