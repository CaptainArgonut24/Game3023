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

    [Header("Audio")]
    public AudioSource backgroundMusicSource;
    public AudioSource battleMusicSource;
    public AudioSource hitSoundSource;
    public AudioSource winSoundSource;
    public AudioSource loseSoundSource;
    public AudioClip[] preBattleSounds;
    public AudioClip[] winLineSounds;
    public AudioClip[] loseLineSounds;
    public AudioClip[] damageSounds;
    public AudioClip inactivitySound;

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
        StartCoroutine(PreBattleSequence());
    }

    IEnumerator PreBattleSequence()
    {
        PlayRandomPreBattleSound();
        DisplayMessage("Battle will start soon");
        yield return new WaitForSeconds(10);

        DisplayRandomEnemy();
        UpdateUI();
        InitializeButtons();
        UpdatePowerUpButtons();
        DisplayMessage("Battle Start! Choose an action.");
        PlayBattleMusic();
        StartCoroutine(InactivityCheck());
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
        actionButton4.onClick.AddListener(OnDefendButtonClicked);

        healButton.onClick.AddListener(UseHeal);
        nukeButton.onClick.AddListener(UseNuke);
        shieldButton.onClick.AddListener(UseShield);

        UpdateButtonTexts();
    }

    void OnAttackButtonClicked(string attackName, int damage)
    {
        if (!isPlayerTurn) return;

        enemyHP -= damage;
        PlayHitSound();
        DisplayMessage("Player used " + attackName + "! Enemy took " + damage + " damage.");
        isPlayerTurn = false;
        UpdateUI();

        if (CheckBattleOutcome()) return;
        Invoke(nameof(EnemyTurn), 2f);
    }

    void EnemyTurn()
    {
        DisplayMessage("Enemy's turn!");

        int damage = Random.Range(10, 30);
        playerHP -= damage;
        PlayDamageSound();
        DisplayMessage("Enemy dealt " + damage + " damage!");

        UpdateUI();

        if (CheckBattleOutcome()) return;

        Invoke(nameof(PlayerTurn), 2f);
    }

    void PlayerTurn()
    {
        isPlayerTurn = true;
        DisplayMessage("Your turn! Choose an action.");
    }

    void UseShield()
    {
        int shields = PlayerScore.Instance.GetShield();
        if (shields > 0)
        {
            shieldRounds = 4;
            PlayerScore.Instance.DecrementShield(1);
            DisplayMessage("Player used Shield! Protected for 4 rounds.");
            UpdatePowerUpButtons();
        }
    }

    void OnDefendButtonClicked()
    {
        if (!isPlayerTurn) return;

        DisplayMessage("Player used Defend! Reducing damage for next attack.");
        isPlayerTurn = false;

        Invoke(nameof(EnemyTurn), 2f);
    }

    void UseHeal()
    {
        int heal = PlayerScore.Instance.GetHeal();
        if (heal > 0 && playerHP < 100)
        {
            playerHP = 100;
            PlayerScore.Instance.DecrementHeal(1);
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
            PlayerScore.Instance.DecrementNukes(1);
            DisplayMessage("Player used Nuke! Enemy took massive damage.");
            UpdatePowerUpButtons();
            UpdateUI();

            if (CheckBattleOutcome()) return;
            Invoke(nameof(EnemyTurn), 2f);
        }
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
        int heal = PlayerScore.Instance.GetHeal();
        int nukes = PlayerScore.Instance.GetNukes();
        int shields = PlayerScore.Instance.GetShield();

        healButton.GetComponentInChildren<TextMeshProUGUI>().text = "Heal (" + heal + ")";
        nukeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Nuke (" + nukes + ")";
        shieldButton.GetComponentInChildren<TextMeshProUGUI>().text = "Shield (" + shields + ")";

        healButton.image.color = heal > 0 ? Color.green : Color.red;
        nukeButton.image.color = nukes > 0 ? Color.green : Color.red;
        shieldButton.image.color = shields > 0 ? Color.green : Color.red;
    }

    void PlayBattleMusic()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Pause();
        }

        if (battleMusicSource != null)
        {
            battleMusicSource.Play();
        }
    }

    void ResumeBackgroundMusic()
    {
        if (battleMusicSource != null)
        {
            battleMusicSource.Stop();
        }

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.UnPause();
        }
    }

    void EndBattle(bool playerWon)
    {
        ResumeBackgroundMusic();

        if (playerWon)
        {
            PlayWinSound();
            DisplayMessage("Congrats, you won!");
            Invoke(nameof(AddPointsAndCloseUI), 10f);
        }
        else
        {
            PlayLoseSound();
            DisplayMessage("You lost...");
            // If the player loses, do not destroy the UI object
            AddPoints();
        }
    }

    void AddPointsAndCloseUI()
    {
        AddPoints();
        // Close UI here
        if (battleUIParent != null)
        {
            Destroy(battleUIParent);
        }

        if (triggerGameObject != null)
        {
            Destroy(triggerGameObject);
        }
    }

    void AddPoints()
    {
        // Add points to UI here
    }

    bool CheckBattleOutcome()
    {
        if (enemyHP <= 0)
        {
            enemyHP = 0;
            UpdateUI();
            EndBattle(true);
            return true;
        }
        else if (playerHP <= 0)
        {
            playerHP = 0;
            UpdateUI();
            EndBattle(false);
            return true;
        }

        return false;
    }

    void DisplayMessage(string message)
    {
        dialogText.text = message;
    }

    void PlayHitSound()
    {
        if (hitSoundSource != null)
        {
            hitSoundSource.Play();
        }
    }

    void PlayDamageSound()
    {
        if (damageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, damageSounds.Length);
            AudioSource.PlayClipAtPoint(damageSounds[randomIndex], transform.position);
        }
    }

    void PlayWinSound()
    {
        if (winSoundSource != null)
        {
            winSoundSource.Play();
            PlayRandomWinLineSound();
        }
    }

    void PlayLoseSound()
    {
        if (loseSoundSource != null)
        {
            loseSoundSource.Play();
            PlayRandomLoseLineSound();
        }
    }

    void PlayRandomPreBattleSound()
    {
        if (preBattleSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, preBattleSounds.Length);
            AudioSource.PlayClipAtPoint(preBattleSounds[randomIndex], transform.position);
        }
    }

    void PlayRandomWinLineSound()
    {
        if (winLineSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, winLineSounds.Length);
            AudioSource.PlayClipAtPoint(winLineSounds[randomIndex], transform.position);
        }
    }

    void PlayRandomLoseLineSound()
    {
        if (loseLineSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, loseLineSounds.Length);
            AudioSource.PlayClipAtPoint(loseLineSounds[randomIndex], transform.position);
        }
    }

    IEnumerator InactivityCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            if (IsPlayerInactive())
            {
                PlayInactivitySound();
            }
        }
    }

    bool IsPlayerInactive()
    {
        // Add your logic to determine player inactivity here
        // For example, if no button clicks or actions are detected within a given time frame
        return true;
    }

    void PlayInactivitySound()
    {
        if (inactivitySound != null)
        {
            AudioSource.PlayClipAtPoint(inactivitySound, transform.position);
        }
    }
}
