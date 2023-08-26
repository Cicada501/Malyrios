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
    public DecisionData LoadedDecisionData { get; private set; }
    public int LoadedEquippedWeaponID { get; private set; }
    public List<NpcData> LoadedNpcData { get; private set; }
    public List<Quest> LoadedQuestLog { get; private set; }
    public List<PuzzleStationData> LoadedPuzzleStations { get; private set; }
    public ArmorData LoadedArmorData { get; private set; }
    
    private LevelManager levelManager;
    private GameObject player;
    private BaseAttributes baseAttributes;
    private NPCManager npcManager;
    private QuestLogWindow questLogWindow;
    private PlayerAttack playerAttack;


    private void Awake()
    {
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        baseAttributes = player.GetComponent<BaseAttributes>();
        npcManager = ReferencesManager.Instance.npcManager;
        questLogWindow = ReferencesManager.Instance.questLogWindow;
        playerAttack = ReferencesManager.Instance.playerAttack;
    }

    public void SaveData()
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
        var attrData = new BaseAttributesData
        {
            maxHealth = baseAttributes.MaxHealth,
            currentHealth = baseAttributes.CurrentHealth,
            mana = baseAttributes.Mana,
            strength = baseAttributes.Strength,
            critChance = baseAttributes.CritChance,
            critDamage = baseAttributes.CritDamage,
            haste = baseAttributes.Haste,
            energy = baseAttributes.Energy,
            balance = baseAttributes.Balance
        };
        PlayerPrefs.SetString("AttributesData", JsonUtility.ToJson(attrData));
        PlayerPrefs.SetString("QuestLog", JsonUtility.ToJson(questLogWindow.SaveQuestLog()));
        
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        PlayerPrefs.DeleteAll();
        // Load level name
        if (PlayerPrefs.HasKey("currentLevelName") && PlayerPrefs.GetString("currentLevelName") != "")
        {
            LoadedLevelName = PlayerPrefs.GetString("currentLevelName");
        }
        else
        {
            LoadedLevelName = "HighForest";
        }


        // Load player position
        if (PlayerPrefs.HasKey("currentPlayerPosition") && PlayerPrefs.GetString("currentLevelName") != "")
        {
            LoadedPlayerPosition = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("currentPlayerPosition"));
        }
        else
        {
            LoadedPlayerPosition = new Vector3(0f, 0.5f,0f);
        }

        // Load inventory data
        if (PlayerPrefs.HasKey("inventoryData")&&PlayerPrefs.GetString("inventoryData")!="")
        {
            LoadedInventoryData = JsonUtility.FromJson<InventoryData>(PlayerPrefs.GetString("inventoryData"));
        }
        else
        {
            LoadedInventoryData = new InventoryData(Inventory.Instance);
        }

        if (PlayerPrefs.HasKey("decisionData")&&PlayerPrefs.GetString("decisionData")!="")
        {
            LoadedDecisionData = JsonUtility.FromJson<DecisionData>(PlayerPrefs.GetString("decisionData"));
        }
        else
        {
            LoadedDecisionData = new DecisionData();
        }
        
        if (PlayerPrefs.HasKey("EquippedWeaponID"))
        {
            LoadedEquippedWeaponID = PlayerPrefs.GetInt("EquippedWeaponID");
        }
        else
        {
            LoadedEquippedWeaponID = 0;

        }
        
        if (PlayerPrefs.HasKey("AttributesData") && PlayerPrefs.GetString("AttributesData") != "")
        {
            var attrData = JsonUtility.FromJson<BaseAttributesData>(PlayerPrefs.GetString("AttributesData"));
            baseAttributes.MaxHealth = attrData.maxHealth;
            baseAttributes.CurrentHealth = attrData.currentHealth;
            baseAttributes.Mana = attrData.mana;
            baseAttributes.Strength = attrData.strength;
            baseAttributes.CritChance = attrData.critChance;
            baseAttributes.CritDamage = attrData.critDamage;
            baseAttributes.Haste = attrData.haste;
            baseAttributes.Energy = attrData.energy;
            baseAttributes.Balance = attrData.balance;
        }

        else
        {
            baseAttributes.MaxHealth = 1000;
            baseAttributes.CurrentHealth = 1000;
            baseAttributes.Mana = 0;
            baseAttributes.Strength = 0;
            baseAttributes.CritChance = 0;
            baseAttributes.CritDamage = 0;
            baseAttributes.Haste = 0;
            baseAttributes.Energy = 0;
            baseAttributes.Balance = 0;
        }

        if (PlayerPrefs.HasKey("currentNpcStates") && PlayerPrefs.GetString("currentNpcStates") != "")
        {
            var loadedNpcData = JsonUtility.FromJson<NPCManager.NpcDataList>(PlayerPrefs.GetString("currentNpcStates"));
            LoadedNpcData = loadedNpcData.npcData; //unwrap
        }
        else
        {
            LoadedNpcData = new List<NpcData>();
        }
        
        if (PlayerPrefs.HasKey("QuestLog") && PlayerPrefs.GetString("QuestLog") != "")
        {
            var loadedQuestLog = JsonUtility.FromJson<QuestList>(PlayerPrefs.GetString("QuestLog"));
            LoadedQuestLog = loadedQuestLog.questList;
        }
        else
        {
            LoadedQuestLog = new List<Quest>();
        }
        
        if (PlayerPrefs.HasKey("puzzleStations") && PlayerPrefs.GetString("puzzleStations") != "")
        {
            var loadedPuzzleStations = JsonUtility.FromJson<PuzzleStationDataList>(PlayerPrefs.GetString("puzzleStations"));
            LoadedPuzzleStations = loadedPuzzleStations.puzzleStationDataList;
        }
        else
        {
            LoadedPuzzleStations = new List<PuzzleStationData>();
        }

        if (PlayerPrefs.HasKey("armor") && PlayerPrefs.GetString("armor") != "")
        {
            LoadedArmorData = JsonUtility.FromJson<ArmorData>(PlayerPrefs.GetString("armor"));
        }
        else
        {
            LoadedArmorData = new ArmorData(150,160,170,180 );
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}