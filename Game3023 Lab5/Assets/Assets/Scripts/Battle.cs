using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogText; // Text for displaying dialog messages
    public TextMeshProUGUI playerHPText; // Player 1 health display
    public TextMeshProUGUI enemyHPText; // Player 2 health display
    public Button actionButton1;
    public Button actionButton2;
    public Button actionButton3;
    public Button actionButton4;
    public GameObject battleUIParent; // Parent object for the battle UI
    public GameObject triggerGameObject; // The GameObject to remove when battle ends

    [Header("Enemy Visuals")]
    public List<Sprite> enemyImages; // List of enemy images as Sprites
    public Image enemyImageDisplay; // UI Image component to display the enemy image

    [Header("Player Stats")]
    public int playerHP = 100;
    public int enemyHP = 100;

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
        DisplayRandomEnemyImage(); // Display a random enemy image at the start
        UpdateUI();
        InitializeButtons();
        DisplayMessage("Battle Start! Choose an action.");
    }

    void UpdateUI()
    {
        playerHPText.text = "Player HP: " + playerHP;
        enemyHPText.text = "Enemy HP: " + enemyHP;
    }

    void InitializeButtons()
    {
        actionButton1.onClick.AddListener(() => OnAttackButtonClicked("Tackle", 20));
        actionButton2.onClick.AddListener(() => OnAttackButtonClicked("Flame Thrower", 25));
        actionButton3.onClick.AddListener(() => OnAttackButtonClicked("Water Gun", 15));
        actionButton4.onClick.AddListener(() => OnDefendButtonClicked());

        UpdateButtonTexts();
    }

    void UpdateButtonTexts()
    {
        actionButton1.GetComponentInChildren<TextMeshProUGUI>().text = "Tackle";
        actionButton2.GetComponentInChildren<TextMeshProUGUI>().text = "Flame Thrower";
        actionButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Water Gun";
        actionButton4.GetComponentInChildren<TextMeshProUGUI>().text = "Defend";
    }

    void DisplayRandomEnemyImage()
    {
        if (enemyImages.Count > 0)
        {
            // Pick a random enemy image from the list
            int randomIndex = Random.Range(0, enemyImages.Count);
            enemyImageDisplay.sprite = enemyImages[randomIndex]; // Assign the sprite
            enemyImageDisplay.enabled = true; // Ensure the Image component is enabled
        }
        else
        {
            Debug.LogWarning("Enemy image list is empty! Please assign enemy images in the Inspector.");
        }
    }

    void OnAttackButtonClicked(string attackName, int damage)
    {
        if (!isPlayerTurn) return;

        enemyHP -= damage;
        DisplayMessage("Player used " + attackName + "! Enemy took " + damage + " damage.");
        isPlayerTurn = false;
        UpdateUI();

        if (CheckBattleOutcome()) return;
        Invoke("EnemyTurn", 2f); // Wait 2 seconds before the enemy turn
    }

    void OnDefendButtonClicked()
    {
        if (!isPlayerTurn) return;

        DisplayMessage("Player used Defend! Reducing damage for next attack.");
        isPlayerTurn = false;

        // Example defense effect: reduce enemy damage on their next turn
        Invoke("EnemyTurn", 2f);
    }

    void EnemyTurn()
    {
        int enemyAttackDamage = Random.Range(10, 30);
        playerHP -= enemyAttackDamage;
        DisplayMessage("Enemy attacks! Player took " + enemyAttackDamage + " damage.");

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
            DisplayMessage("Enemy defeated!");
            Invoke("EndBattle", 3f);
            return true;
        }
        else if (playerHP <= 0)
        {
            DisplayMessage("Player was defeated!");
            Invoke("EndBattle", 3f);
            return true;
        }
        return false;
    }

    void EndBattle()
    {
        if (battleUIParent != null)
        {
            Destroy(battleUIParent); // Remove the UI by destroying its parent
        }

        if (triggerGameObject != null)
        {
            Destroy(triggerGameObject); // Remove the specified trigger object
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
