using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int score;
    public int battles;
    public int wins;
    public int lost;
    public int heal;
    public int nukes;
    public int shield;

    // Constructor to initialize the data
    public PlayerData(int score, int battles, int wins, int lost, int heal, int nukes, int shield)
    {
        this.score = score;
        this.battles = battles;
        this.wins = wins;
        this.lost = lost;
        this.heal = heal;
        this.nukes = nukes;
        this.shield = shield;
    }

    // You can add any additional methods here if needed, like resetting values or calculating totals.
}
