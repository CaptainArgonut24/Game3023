using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro;

public class AchievementScript : MonoBehaviour
{
    [Header("TextMeshPro Objects")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winsText;
    public TextMeshProUGUI battlesText;
    public TextMeshProUGUI powerUpsText;

    [Header("Achievement Colors")]
    public Color achievedColor = Color.green;
    public Color notAchievedColor = Color.red;

    private void Update()
    {
        // Check each condition and update the text color
        scoreText.color = PlayerScore.score >= 9995 ? achievedColor : notAchievedColor;
        winsText.color = PlayerScore.wins >= 8 ? achievedColor : notAchievedColor;
        battlesText.color = PlayerScore.battles >= 15 ? achievedColor : notAchievedColor;
        powerUpsText.color = PlayerScore.PowerUps >= 20 ? achievedColor : notAchievedColor;
    }
}
