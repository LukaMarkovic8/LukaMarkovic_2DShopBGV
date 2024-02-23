using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using Newtonsoft.Json;

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
    public bool blockMoving;
    public class GameData
    {
        public string playerName;
        public int playerBalance;
        //first int is type casted to int and second one is the ID unique to each item
        public List<(int, int)> itemsOwned = new List<(int, int)>();
        public List<(int, int)> itemsEquipped;
    }
    private void Awake()
    {
        dataController = this;
        // Set file path
        filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        LoadFromJson();
    }

    public void SetPlayerName(string playerName)
    {
        playerData.playerName = playerName;
        SaveToJson();
    }

    public void SetPlayerBalance(int amount)
    {
        playerData.playerBalance += amount;
        SaveToJson();
    }

    public List<int> GetItemsForSaleByCategory(ItemType type)
    {
        List<int> itemIds = new List<int>();

        foreach (var item in playerData.itemsOwned)
        {
            if (item.Item1 == (int)type)
            {
                if (!playerData.itemsEquipped.Contains(item))
                {
                    itemIds.Add(item.Item2);
                }
            }
        }
        return itemIds;
    }

    public List<int> GetOwnedItemsByCategory(ItemType type)
    {
        List<int> items = new List<int>();

        foreach (var item in playerData.itemsOwned)
        {
            if (item.Item1 == (int)type)
            {

                items.Add(item.Item2);

            }
        }
        return items;
    }

    public void UpdateEquipedItems(Item item)
    {
        for (int i = 0; i < playerData.itemsEquipped.Count; i++)
        {
            if (playerData.itemsEquipped[i].Item1 == (int)item.Type)
            {
                playerData.itemsEquipped[i] = ((int)item.Type, item.Id);
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

    public void RemoveOwnedItem(Item item)
    {
        if (playerData.itemsOwned.Contains(((int)item.Type, item.Id)))
        {
            playerData.itemsOwned.Remove(((int)item.Type, item.Id));
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
            playerData = JsonConvert.DeserializeObject<GameData>(json);
        }
        else
        {
            Debug.LogWarning("No saved data found");
        }
    }
    // Save data to JSON file

    public void SaveToJson()
    {
        string json = JsonConvert.SerializeObject(playerData);
        File.WriteAllText(filePath, json);
    }

    [ContextMenu("ResetData")]
    public void ResetData()
    {
        string cheat = "{\r\n  \"playerName\":\"Player\",\r\n  \"playerBalance\":1000,\r\n  \"itemsOwned\":[\r\n     {\r\n        \"Item1\":3,\r\n        \"Item2\":7\r\n     },\r\n     {\r\n        \"Item1\":3,\r\n        \"Item2\":11\r\n     },\r\n     {\r\n        \"Item1\":3,\r\n        \"Item2\":10\r\n     },\r\n     {\r\n        \"Item1\":3,\r\n        \"Item2\":9\r\n     }\r\n  ],\r\n  \"itemsEquipped\":[\r\n     {\r\n        \"Item1\":1,\r\n        \"Item2\":13\r\n     },\r\n     {\r\n        \"Item1\":2,\r\n        \"Item2\":7\r\n     },\r\n     {\r\n        \"Item1\":3,\r\n        \"Item2\":20\r\n     },\r\n     {\r\n        \"Item1\":4,\r\n        \"Item2\":31\r\n     },\r\n     {\r\n        \"Item1\":5,\r\n        \"Item2\":41\r\n     },\r\n     {\r\n        \"Item1\":6,\r\n        \"Item2\":1\r\n     },\r\n     {\r\n        \"Item1\":7,\r\n        \"Item2\":51\r\n     },\r\n     {\r\n        \"Item1\":8,\r\n        \"Item2\":61\r\n     }\r\n  ]\r\n}";
        File.WriteAllText(filePath, cheat);
    }

}
