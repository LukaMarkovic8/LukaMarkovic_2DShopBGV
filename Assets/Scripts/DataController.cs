using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public enum ItemType
{
    None,
    Head,
    Torso,
    Shoulders,
    Elbow,
    Hands,
    Pelvis,
    Legs,
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
        //first int is type casted to int and second one is the ID unique to each item
        public List<(int, int)> itemsOwned;
        public List<(int, int)> itemsEquiped;

        // Add other fields as needed
    }
    private void Awake()
    {
        dataController = this;
        // Set file path
        filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        LoadFromJson();
    }
    void Start()
    {
        //Initial data loading
    }


    public void SetPlayerName(string playerName)
    {
        playerData.playerName = playerName;
        SaveToJson();
    }

    public void SetPlayerBalance(int playerBalance)
    {
        playerData.playerBalance = playerBalance;
        SaveToJson();
    }


    public void UpdateEquipedItems(Item item)
    {
        for (int i = 0; i < playerData.itemsEquiped.Count; i++)
        {
            if (playerData.itemsEquiped[i].Item1 == (int)item.Type)
            {
                playerData.itemsEquiped[i] = ((int)item.Type, item.Id);
            }
        }
        SaveToJson();
    }

    public void AddToOwnedItems(Item item)
    {
        if (!playerData.itemsOwned.Contains(((int)item.Type, item.Id)))
        {
            playerData.itemsOwned.Add(((int)item.Type, item.Id));
        }
        SaveToJson();
    }

    // Load data from JSON file
    public void LoadFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            //Parsing
            playerData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogWarning("No saved data found");
        }
    }
    // Save data to JSON file


    public void SaveToJson()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to " + filePath);
    }

}
