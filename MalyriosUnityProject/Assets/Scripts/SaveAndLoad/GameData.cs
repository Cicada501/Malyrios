using System;
using System.Collections.Generic;
using Malyrios.Character;
using NPCs;
using SaveAndLoad;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Vector3 LoadedPlayerPosition { get; private set; }
    public InventoryData LoadedInventoryData { get; private set; }
    public string LoadedLevelName { get; private set; }
    public int LoadedEquippedWeaponID { get; private set; }
    public List<NpcData> LoadedNpcData { get; private set; }
    public List<Quest> LoadedQuestLog { get; private set; }
    public List<PuzzleStationData> LoadedPuzzleStations { get; private set; }
    public ArmorData LoadedArmorData { get; private set; }
    
    private LevelManager levelManager;
    private GameObject player;
    private NPCManager npcManager;
    private QuestLogWindow questLogWindow;
    private PlayerAttack playerAttack;


    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        npcManager = ReferencesManager.Instance.npcManager;
        questLogWindow = ReferencesManager.Instance.questLogWindow;
        playerAttack = ReferencesManager.Instance.playerAttack;
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("armor",JsonUtility.ToJson(EquipmentManager.Instance.SaveArmor()));
        print($"Saving: {JsonUtility.ToJson(EquipmentManager.Instance.SaveArmor())}");
        PlayerPrefs.SetString("puzzleStations",JsonUtility.ToJson(PuzzleStationManager.Instance.SaveStations()));
        PlayerPrefs.SetString("currentNpcStates",JsonUtility.ToJson(npcManager.SaveNpCs()));
        PlayerPrefs.SetString("currentLevelName", levelManager.GetCurrentLevelName());
        PlayerPrefs.SetString("currentPlayerPosition", JsonUtility.ToJson(player.transform.position));

        InventoryData inventoryData = new InventoryData(Inventory.Instance);
        print($"saved inventory data: {JsonUtility.ToJson(inventoryData)}");
        PlayerPrefs.SetString("inventoryData", JsonUtility.ToJson(inventoryData));
        DecisionData decisionData = new DecisionData();
        PlayerPrefs.SetString("decisionData", JsonUtility.ToJson(decisionData));
        PlayerPrefs.SetInt("EquippedWeaponID", playerAttack.EquippedWeaponID);
        PlayerPrefs.SetString("QuestLog", JsonUtility.ToJson(questLogWindow.SaveQuestLog()));
        
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        PlayerPrefs.DeleteAll();
        
        //Level
        if (PlayerPrefs.HasKey("currentLevelName") && PlayerPrefs.GetString("currentLevelName") != "")
        {
            LoadedLevelName = PlayerPrefs.GetString("currentLevelName");
        }
        else
        {
            LoadedLevelName = "HighForest";
        }


        //Player position
        if (PlayerPrefs.HasKey("currentPlayerPosition") && PlayerPrefs.GetString("currentLevelName") != "")
        {
            LoadedPlayerPosition = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("currentPlayerPosition"));
        }
        else
        {
            LoadedPlayerPosition = new Vector3(0f, 0.5f,0f);
        }

        //Inventory data (Item List)
        if (PlayerPrefs.HasKey("inventoryData")&&PlayerPrefs.GetString("inventoryData")!="")
        {
            LoadedInventoryData = JsonUtility.FromJson<InventoryData>(PlayerPrefs.GetString("inventoryData"));
        }
        else
        {
            LoadedInventoryData = new InventoryData(Inventory.Instance);
        }
        
        //Equipped Weapon
        LoadedEquippedWeaponID = PlayerPrefs.HasKey("EquippedWeaponID") ? PlayerPrefs.GetInt("EquippedWeaponID") : 0;
        
        //NPC States
        if (PlayerPrefs.HasKey("currentNpcStates") && PlayerPrefs.GetString("currentNpcStates") != "")
        {
            var loadedNpcData = JsonUtility.FromJson<NPCManager.NpcDataList>(PlayerPrefs.GetString("currentNpcStates"));
            LoadedNpcData = loadedNpcData.npcData; //unwrap
        }
        else
        {
            LoadedNpcData = new List<NpcData>();
        }
        
        //Questlog Entries
        if (PlayerPrefs.HasKey("QuestLog") && PlayerPrefs.GetString("QuestLog") != "")
        {
            var loadedQuestLog = JsonUtility.FromJson<QuestList>(PlayerPrefs.GetString("QuestLog"));
            LoadedQuestLog = loadedQuestLog.questList;
        }
        else
        {
            LoadedQuestLog = new List<Quest>();
        }
        
        //Items in Puzzle Stations
        if (PlayerPrefs.HasKey("puzzleStations") && PlayerPrefs.GetString("puzzleStations") != "")
        {
            var loadedPuzzleStations = JsonUtility.FromJson<PuzzleStationDataList>(PlayerPrefs.GetString("puzzleStations"));
            LoadedPuzzleStations = loadedPuzzleStations.puzzleStationDataList;
        }
        else
        {
            LoadedPuzzleStations = new List<PuzzleStationData>();
        }

        //Equipped Armor
        if (PlayerPrefs.HasKey("armor") && PlayerPrefs.GetString("armor") != "")
        {
            LoadedArmorData = JsonUtility.FromJson<ArmorData>(PlayerPrefs.GetString("armor"));
        }
        else
        {
            LoadedArmorData = new ArmorData(0,0,0,0 );
            //LoadedArmorData = new ArmorData(150,160,170,180 );
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}