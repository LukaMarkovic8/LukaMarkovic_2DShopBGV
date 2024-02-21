using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

    public enum ItemType
    {
        None,
        Hair,
        Boots
    }
public class DataController : MonoBehaviour
{
    private string filePath;

   public static DataController dataController;

    public GameData playerData;
    public class GameData
    {
        public string playerName;
        public int playerBalance;
        public List<(int,int)> itemsOwned;
        // Add other fields as needed
    }
    private void Awake()
    {
        dataController = this;
        // Set file path
        filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
    }
    void Start()
    {
        LoadFromJson();
    }


    public void SetPlayerName(string playerName)
    {
        playerData.playerName = playerName;
        SaveToJson();
    }

    public void SetPlayerBalance(int playerBalance)
    {
        playerData.playerBalance = playerBalance;
    }



    // Load data from JSON file
    public void LoadFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<GameData>(json);
            Debug.Log(playerData.playerBalance);
        }
        else
        {
            Debug.LogWarning("No saved data found");
        }
    }
    // Save data to JSON file
    public void SaveToJson()
    {           
        playerData.playerBalance = 150 + UnityEngine.Random.RandomRange(1, 522);
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to " + filePath);
    }

}
