using System;
using System.Collections.Generic;
using System.IO;
using Malyrios.Character;
using NPCs;
using SaveAndLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SaveData
{
    public Vector3 PlayerPosition;
    public string LevelName;
    public InventoryData Inventory;
    public int EquippedWeaponID;
    public List<NpcData> NpcData;
    public List<Quest> QuestLog;
    public List<PuzzleStationData> PuzzleStations;
    public ArmorData ArmorData;
    public ScrollData ScrollData;
    public LeverDataList LeverStates;
    public int playerMoney;
    public int PlayerHealth;
    public int PlayerMana;
    public bool LearnedFireball;

    public float MusicVolume;
    public float PlayerSoundsVolume;
    public float EnemySoundsVolume;
    public float EnvironmentVolume;
    public float PlayerAbilitiesVolume;
    public float InventoryVolume;
    public float UiVolume;
}

public class GameData : MonoBehaviour
{
    private string saveFilePath;
    private LevelManager levelManager;
    private GameObject player;
    private NPCManager npcManager;
    private QuestLogWindow questLogWindow;
    private PlayerAttack playerAttack;
    private BaseAttributes baseAttributes;

    public Vector3 LoadedPlayerPosition { get; private set; }
    public string LoadedLevelName { get; private set; }
    public InventoryData LoadedInventoryData { get; private set; }
    public int LoadedEquippedWeaponID { get; private set; }
    public List<NpcData> LoadedNpcData { get; private set; }
    public List<Quest> LoadedQuestLog { get; private set; }
    public List<PuzzleStationData> LoadedPuzzleStations { get; private set; }
    public ArmorData LoadedArmorData { get; private set; }
    public ScrollData LoadedScrollData { get; private set; } = new ScrollData();
    public LeverDataList LoadedLeverStates { get; private set; }
    public int LoadedPlayerMoney { get; private set; }
    public int LoadedPlayerCurrentHealth { get; private set; }
    public int LoadedPlayerCurrentMana { get; private set; }
    public bool LoadedLearnedFireball { get; private set; }


    public float LoadedMusicVolume { get; private set; } = 1f;
    public float LoadedPlayerSoundsVolume { get; private set; } = 1f;
    public float LoadedEnemySoundsVolume { get; private set; } = 1f;
    public float LoadedEnvironmentVolume { get; private set; } = 1f;
    public float LoadedPlayerAbilitiesVolume { get; private set; } = 1f;
    public float LoadedInventoryVolume { get; private set; } = 1f;
    public float LoadedUiVolume { get; private set; } = 1f;

    [SerializeField] private string startLevel;
    [SerializeField] private Toggle toggleResetOnRestart;
    [SerializeField] private TextMeshProUGUI saveLoadText;

    private bool resetOnRestart;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        npcManager = ReferencesManager.Instance.npcManager;
        questLogWindow = ReferencesManager.Instance.questLogWindow;
        playerAttack = ReferencesManager.Instance.playerAttack;
        baseAttributes = player.GetComponent<BaseAttributes>();
    }

    void Start()
    {
        resetOnRestart = toggleResetOnRestart.isOn;
        if (toggleResetOnRestart != null)
            toggleResetOnRestart.onValueChanged.AddListener((value) => resetOnRestart = value);
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            PlayerPosition = player.transform.position,
            LevelName = levelManager.GetCurrentLevelName(),
            Inventory = new InventoryData(Inventory.Instance),
            EquippedWeaponID = playerAttack.EquippedWeaponID,
            NpcData = npcManager.SaveNpCs().npcData,
            QuestLog = questLogWindow.SaveQuestLog().questList,
            PuzzleStations = PuzzleStationManager.Instance.SaveStations().puzzleStationDataList,
            ArmorData = EquipmentManager.Instance.SaveArmor(),
            ScrollData = SaveScrolls.Instance.scrollData,
            LeverStates = new LeverDataList(), 
            playerMoney = PlayerMoney.Instance.CurrentMoney,
            PlayerHealth = player.GetComponent<BaseAttributes>().CurrentHealth,
            PlayerMana = player.GetComponent<BaseAttributes>().Mana,
            LearnedFireball = ReferencesManager.Instance.fireballButton.activeSelf,

            MusicVolume = LoadedMusicVolume,
            PlayerSoundsVolume = LoadedPlayerSoundsVolume,
            EnemySoundsVolume = LoadedEnemySoundsVolume,
            EnvironmentVolume = LoadedEnvironmentVolume,
            PlayerAbilitiesVolume = LoadedPlayerAbilitiesVolume,
            InventoryVolume = LoadedInventoryVolume,
            UiVolume = LoadedUiVolume
        };

        string json = JsonUtility.ToJson(saveData);
        saveLoadText.text = "Saved: "+ json;
        File.WriteAllText(saveFilePath, json);

        PlayerPrefs.SetInt("resetOnRestart",resetOnRestart?1:0);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        resetOnRestart = PlayerPrefs.GetInt("resetOnRestart",0) == 1;
        toggleResetOnRestart.isOn = resetOnRestart;
        if (File.Exists(saveFilePath)&&!resetOnRestart)
        {
            string json = File.ReadAllText(saveFilePath);
            saveLoadText.text = "Loaded: "+json;
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            LoadedPlayerPosition =
                saveData.PlayerPosition != null ? saveData.PlayerPosition : new Vector3(0f, 0.5f, 0f);
            LoadedLevelName = !string.IsNullOrEmpty(saveData.LevelName) ? saveData.LevelName : startLevel;
            LoadedInventoryData =
                saveData.Inventory ?? new InventoryData(Inventory.Instance);
            LoadedEquippedWeaponID = saveData.EquippedWeaponID != 0 ? saveData.EquippedWeaponID : 0;
            LoadedNpcData = saveData.NpcData ?? new List<NpcData>();
            LoadedQuestLog = saveData.QuestLog ?? new List<Quest>();
            LoadedPuzzleStations = saveData.PuzzleStations ?? new List<PuzzleStationData>();
            LoadedArmorData = saveData.ArmorData != null ? saveData.ArmorData : new ArmorData(0, 0, 0, 0);
            LoadedScrollData = saveData.ScrollData ?? new ScrollData();
            LoadedLeverStates = saveData.LeverStates ?? new LeverDataList();
            LoadedPlayerMoney = saveData.playerMoney != 0 ? saveData.playerMoney : 0;
            LoadedPlayerCurrentHealth = saveData.PlayerHealth != 0 ? saveData.PlayerHealth : 1000;
            LoadedPlayerCurrentMana = saveData.PlayerMana != 0 ? saveData.PlayerMana : 1000;
            LoadedLearnedFireball = saveData.LearnedFireball!=null ? saveData.LearnedFireball:false;

            // Audioeinstellungen
            LoadedMusicVolume = saveData.MusicVolume > 0 ? saveData.MusicVolume : 0.5f;
            LoadedPlayerSoundsVolume = saveData.PlayerSoundsVolume > 0 ? saveData.PlayerSoundsVolume : 0.5f;
            LoadedEnemySoundsVolume = saveData.EnemySoundsVolume > 0 ? saveData.EnemySoundsVolume : 0.5f;
            LoadedEnvironmentVolume = saveData.EnvironmentVolume > 0 ? saveData.EnvironmentVolume : 0.5f;
            LoadedPlayerAbilitiesVolume = saveData.PlayerAbilitiesVolume > 0 ? saveData.PlayerAbilitiesVolume : 0.5f;
            LoadedInventoryVolume = saveData.InventoryVolume > 0 ? saveData.InventoryVolume : 0.5f;
            LoadedUiVolume = saveData.UiVolume > 0 ? saveData.UiVolume : 1f;
        }
        else
        {
            saveLoadText.text = "Es existiert keine Sicherungdatei";
            // Setze alle Daten auf Standardwerte, wenn keine SaveData vorhanden ist
            LoadedPlayerPosition = new Vector3(0f, 0.5f, 0f);
            LoadedLevelName = startLevel;
            LoadedInventoryData = new InventoryData(Inventory.Instance);
            LoadedEquippedWeaponID = 0;
            LoadedNpcData = new List<NpcData>();
            LoadedQuestLog = new List<Quest>();
            LoadedPuzzleStations = new List<PuzzleStationData>();
            LoadedArmorData = new ArmorData(0, 0, 0, 0);
            LoadedScrollData = new ScrollData();
            LoadedLeverStates = new LeverDataList();
            LoadedPlayerMoney = 0;
            LoadedPlayerCurrentHealth = 1000;
            LoadedPlayerCurrentMana = 1000;
            LoadedLearnedFireball = false;

            // Standardwerte für Audioeinstellungen
            LoadedMusicVolume = 0.5f;
            LoadedPlayerSoundsVolume = 0.5f;
            LoadedEnemySoundsVolume = 0.5f;
            LoadedEnvironmentVolume = 0.5f;
            LoadedPlayerAbilitiesVolume = 0.5f;
            LoadedInventoryVolume = 0.5f;
            LoadedUiVolume = 0.5f;
        }
    }

    public void SaveAudioSettings(float playerSoundsVolume, float enemySoundsVolume, float musicVolume,
        float environmentVolume, float playerAbilitiesVolume, float inventoryVolume, float uiVolume)
    {
        // Speichere die übergebenen Werte in den Eigenschaften
        LoadedPlayerSoundsVolume = playerSoundsVolume;
        LoadedEnemySoundsVolume = enemySoundsVolume;
        LoadedMusicVolume = musicVolume;
        LoadedEnvironmentVolume = environmentVolume;
        LoadedPlayerAbilitiesVolume = playerAbilitiesVolume;
        LoadedInventoryVolume = inventoryVolume;
        LoadedUiVolume = uiVolume;

        // Speichere das Spiel, um die neuen Einstellungen zu übernehmen
        SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}