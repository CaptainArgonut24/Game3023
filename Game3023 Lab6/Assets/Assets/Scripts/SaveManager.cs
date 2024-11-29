using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        // Define the file path for saving/loading data
        saveFilePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    // Save the player's data
    public void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true); // Serialize the PlayerData object to JSON
        File.WriteAllText(saveFilePath, json); // Save the JSON string to a file
        Debug.Log("Player data saved.");
    }

    // Load the player's data
    public PlayerData LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath); // Read the JSON string from the file
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json); // Deserialize the JSON string into PlayerData object
            Debug.Log("Player data loaded.");
            return playerData;
        }
        else
        {
            Debug.LogWarning("No saved data found, returning default data.");
            return new PlayerData(0, 0, 0, 0, 0, 0, 0); // Return default data if no saved file exists
        }
    }
}
