using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public Text dialogText; // Text for displaying dialog messages
    public Text playerHPText; // Player 1 health display
    public Text enemyHPText; // Player 2 health display
    public Button actionButton1;
    public Button actionButton2;
    public Button actionButton3;
    public Button actionButton4;

    [Header("Player Stats")]
    public int playerHP = 100;
    public int enemyHP = 100;
    public int sceneToReturnTo = 0; // Set the scene index to return to

    private bool isPlayerTurn = true;

    void Start()
    {
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
        actionButton1.GetComponentInChildren<Text>().text = "Tackle";
        actionButton2.GetComponentInChildren<Text>().text = "Flame Thrower";
        actionButton3.GetComponentInChildren<Text>().text = "Water Gun";
        actionButton4.GetComponentInChildren<Text>().text = "Defend";
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
            DisplayMessage("Your turn! Choose an action.");
        }
    }

    bool CheckBattleOutcome()
    {
        if (enemyHP <= 0)
        {
            DisplayMessage("Enemy defeated! Returning to menu.");
            Invoke("ReturnToScene", 3f);
            return true;
        }
        else if (playerHP <= 0)
        {
            DisplayMessage("Player was defeated! Returning to menu.");
            Invoke("ReturnToScene", 3f);
            return true;
        }
        return false;
    }

    void ReturnToScene()
    {
        SceneManager.LoadScene(sceneToReturnTo);
    }

    void DisplayMessage(string message)
    {
        dialogText.text = message;
    }
}
