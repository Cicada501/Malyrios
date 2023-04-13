using System;
using Malyrios.Character;
using SaveAndLoad;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Vector3 LoadedPlayerPosition { get; private set; }
    public float LoadedCurrentHealth { get; private set; }
    public InventoryData LoadedInventoryData { get; private set; }
    public string LoadedLevelName { get; private set; }

    private LevelManager levelManager;
    private GameObject player;
    private BaseAttributes baseAttributes;
    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        baseAttributes = player.GetComponent<BaseAttributes>();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("currentLevelName", levelManager.GetCurrentLevelName());
        PlayerPrefs.SetString("currentPlayerPosition", JsonUtility.ToJson(player.transform.position));
        PlayerPrefs.SetFloat("currentHealth", baseAttributes.CurrentHealth);

        InventoryData inventoryData = new InventoryData(Inventory.Instance);
        //print($"saved inventory data: {JsonUtility.ToJson(inventoryData)}");
        PlayerPrefs.SetString("inventoryData", JsonUtility.ToJson(inventoryData));
        
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        // Load level name
        if (PlayerPrefs.HasKey("currentLevelName"))
        {
            LoadedLevelName = PlayerPrefs.GetString("currentLevelName");
        }
        else
        {
            LoadedLevelName = "HighForest"; // Replace with your default level name
        }

        // Load player position
        if (PlayerPrefs.HasKey("currentPlayerPosition"))
        {
            LoadedPlayerPosition = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("currentPlayerPosition"));
        }
        else
        {
            LoadedPlayerPosition = Vector3.zero;
        }

        // Load current health
        if (PlayerPrefs.HasKey("currentHealth"))
        {
            LoadedCurrentHealth = PlayerPrefs.GetFloat("currentHealth");
        }

        // Load inventory data
        if (PlayerPrefs.HasKey("inventoryData"))
        {
            LoadedInventoryData = JsonUtility.FromJson<InventoryData>(PlayerPrefs.GetString("inventoryData"));
        }
        

        // else
        // {
        //     // Set up a default inventory data with an empty item list and default equipped weapon ID
        //     LoadedInventoryData = new InventoryData
        //     {
        //         itemIDs = new int[0],
        //         equippedWeaponID = 0 // Replace with your default equipped weapon ID
        //     };
        // }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}