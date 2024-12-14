using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
//using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    public float[] pos;
    public int score;
    public int battles;
    public int wins;
    public int lost;
    public int heal;
    public int nukes;
    public int shield;

    public int time;


    public int PU1;
    public int PU2;
    public int PU3;
    public int PU4;
    public int PU5;
    public int PU6;
    public int PU7;
    public int PU8;
    public int PU9;
    public int PU10;
    public int PU11;
    public int PU12;
    public int PU13;
    public int PU14;
    public int PU15;
    public int PU16;
    public int PU17;
    public int PU18;
    public int PU19;


    public int Bat1;
    public int Bat2;
    public int Bat3;
    public int Bat4;
    public int Bat5;
    public int Bat6;
    public int Bat7;
    public int Bat8;
    public int Bat9;
    public int Bat10;
    public int Bat11;
    public int Bat12;
    public int Bat13;
    public int Bat14;
    public int Bat15;

    public void SaveGame()
    {
        PlayerData playerData = new PlayerData();
       // playerData.pos = new float[] {playerTransform.position.x, playerTransform.position.y, playerTransform.position.z }; 
        playerData.score = PlayerScore.score;
        playerData.battles = PlayerScore.battles;
        playerData.wins = PlayerScore.wins;
        playerData.lost = PlayerScore.lost;
        playerData.heal = PlayerScore.heal;
        playerData.nukes = PlayerScore.nukes;
        playerData.shield = PlayerScore.shield;
        playerData.PU1 = PlayerScore.PU1;
        playerData.PU2 = PlayerScore.PU2;
        playerData.PU3 = PlayerScore.PU3;
        playerData.PU4 = PlayerScore.PU4;
        playerData.PU5 = PlayerScore.PU5;
        playerData.PU6 = PlayerScore.PU6;
        playerData.PU7 = PlayerScore.PU7;
        playerData.PU8 = PlayerScore.PU8;
        playerData.PU9 = PlayerScore.PU9;
        playerData.PU10 = PlayerScore.PU10;
        playerData.PU11 = PlayerScore.PU11;
        playerData.PU12 = PlayerScore.PU12;
        playerData.PU13 = PlayerScore.PU13;
        playerData.PU14 = PlayerScore.PU14;
        playerData.PU15 = PlayerScore.PU15;
        playerData.PU16 = PlayerScore.PU16;
        playerData.PU17 = PlayerScore.PU17;
        playerData.PU18 = PlayerScore.PU18;
        playerData.PU19 = PlayerScore.PU19;
        playerData.Bat1 = PlayerScore.Bat1;
        playerData.Bat2 = PlayerScore.Bat2;
        playerData.Bat3 = PlayerScore.Bat3;
        playerData.Bat4 = PlayerScore.Bat4;
        playerData.Bat5 = PlayerScore.Bat5;
        playerData.Bat6 = PlayerScore.Bat6;
        playerData.Bat7 = PlayerScore.Bat7;
        playerData.Bat8 = PlayerScore.Bat8;
        playerData.Bat9 = PlayerScore.Bat9;
        playerData.Bat10 = PlayerScore.Bat10;
        playerData.Bat11 = PlayerScore.Bat11;
        playerData.Bat12 = PlayerScore.Bat12;
        playerData.Bat13 = PlayerScore.Bat13;
        playerData.Bat14 = PlayerScore.Bat14;
        playerData.Bat15 = PlayerScore.Bat15;


        string json = JsonUtility.ToJson(playerData);
        string path = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(path, json);



    }

    public void loadGame()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        
        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData LoadedData = JsonUtility.FromJson<PlayerData>(json);


            PlayerScore.score = LoadedData.score;
            PlayerScore.battles = LoadedData.battles;
            PlayerScore.wins = LoadedData.wins;
            PlayerScore.lost = LoadedData.lost;
            PlayerScore.heal = LoadedData.heal;
            PlayerScore.nukes = LoadedData.nukes;
            PlayerScore.shield = LoadedData.shield;
            PlayerScore.PU1 = LoadedData.PU1;
            PlayerScore.PU2 = LoadedData.PU2;
            PlayerScore.PU3 = LoadedData.PU3;
            PlayerScore.PU4 = LoadedData.PU4;
            PlayerScore.PU5 = LoadedData.PU5;
            PlayerScore.PU6 = LoadedData.PU6;
            PlayerScore.PU7 = LoadedData.PU7;
            PlayerScore.PU8 = LoadedData.PU8;
            PlayerScore.PU9 = LoadedData.PU9;
            PlayerScore.PU10 = LoadedData.PU10;
            PlayerScore.PU11 = LoadedData.PU11;
            PlayerScore.PU12 = LoadedData.PU12;
            PlayerScore.PU13 = LoadedData.PU13;
            PlayerScore.PU14 = LoadedData.PU14;
            PlayerScore.PU15 = LoadedData.PU15;
            PlayerScore.PU16 = LoadedData.PU16;
            PlayerScore.PU17 = LoadedData.PU17;
            PlayerScore.PU18 = LoadedData.PU18;
            PlayerScore.PU19 = LoadedData.PU19;
            PlayerScore.Bat1 = LoadedData.Bat1;
            PlayerScore.Bat2 = LoadedData.Bat2;
            PlayerScore.Bat3 = LoadedData.Bat3;
            PlayerScore.Bat4 = LoadedData.Bat4;
            PlayerScore.Bat5 = LoadedData.Bat5;
            PlayerScore.Bat6 = LoadedData.Bat6;
            PlayerScore.Bat7 = LoadedData.Bat7;
            PlayerScore.Bat8 = LoadedData.Bat8;
            PlayerScore.Bat9 = LoadedData.Bat9;
            PlayerScore.Bat10 = LoadedData.Bat10;
            PlayerScore.Bat11 = LoadedData.Bat11;
            PlayerScore.Bat12 = LoadedData.Bat12;
            PlayerScore.Bat13 = LoadedData.Bat13;
            PlayerScore.Bat14 = LoadedData.Bat14;
            PlayerScore.Bat15 = LoadedData.Bat15;

        }
        else
        {
            Debug.LogWarning("File not found!");
        }
    }
}
