using System;
using System.Collections.Generic;
using Malyrios.Character;
using NPCs;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.UI;

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
    
    public ScrollData LoadedScrollData { get; private set; } = new ();
    public LeverDataList LoadedLeverStates { get; private set; }
 
    private LevelManager levelManager;
    private GameObject player;
    private NPCManager npcManager;
    private QuestLogWindow questLogWindow;
    private PlayerAttack playerAttack;
    private bool resetOnRestart;
    [SerializeField] private Toggle toggleResetOnRestart;
    private BaseAttributes baseAttributes;

    [SerializeField] private string startLevel;

    private void Awake()
    {
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
        if (toggleResetOnRestart != null)toggleResetOnRestart.onValueChanged.AddListener((value) => resetOnRestart = value);
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("armor",JsonUtility.ToJson(EquipmentManager.Instance.SaveArmor()));
        PlayerPrefs.SetString("puzzleStations",JsonUtility.ToJson(PuzzleStationManager.Instance.SaveStations()));
        PlayerPrefs.SetString("currentNpcStates",JsonUtility.ToJson(npcManager.SaveNpCs()));
        PlayerPrefs.SetString("currentLevelName", levelManager.GetCurrentLevelName());
        PlayerPrefs.SetString("currentPlayerPosition", JsonUtility.ToJson(player.transform.position));

        InventoryData inventoryData = new InventoryData(Inventory.Instance);
        PlayerPrefs.SetString("inventoryData", JsonUtility.ToJson(inventoryData));
        PlayerPrefs.SetInt("EquippedWeaponID", playerAttack.EquippedWeaponID);
        PlayerPrefs.SetString("QuestLog", JsonUtility.ToJson(questLogWindow.SaveQuestLog()));
        PlayerPrefs.SetString("resetOnRestart",resetOnRestart.ToString());
        
        PlayerPrefs.SetString("scrollData", JsonUtility.ToJson(SaveScrolls.Instance.scrollData));
        PlayerPrefs.SetInt("currentHealth", baseAttributes.CurrentHealth);
        PlayerPrefs.SetInt("Mana", baseAttributes.Mana);
        PlayerPrefs.SetInt("LearnedFireball", ReferencesManager.Instance.fireballButton.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("CurrentMoney", PlayerMoney.Instance.CurrentMoney );
        

        LeverDataList leverStates = new LeverDataList();
        foreach (var lever in FindObjectsOfType<PuzzleLever>())
        {
            leverStates.leverDataList
                .Add(new LeverData { state = lever.state, leverID = lever.leverID });
        }
        PlayerPrefs.SetString("leverStates", JsonUtility.ToJson(leverStates));




        PlayerPrefs.Save();
    }

    #region AudioSettings
    //Volume
    public float LoadedPlayerSoundsVolume { get; private set; } = 1f;
    public float LoadedEnemySoundsVolume { get; private set; } = 1f;
    public float LoadedMusicVolume { get; private set; } = 1f;
    public float LoadedEnvironmentVolume { get; set; }
    public float LoadedPlayerAbilitiesVolume { get; set; }
    public float LoadedInventoryVolume { get; set; }
    public float LoadedUiVolume { get; set; }
    

    
    public void SaveAudioSettings(float playerSoundsVolume, float enemySoundsVolume, float musicVolume, float environmentVolume, float playerAbilitiesVolume, float inventoryVolume, float uiVolume)
    {
        LoadedPlayerSoundsVolume = playerSoundsVolume;
        LoadedEnemySoundsVolume = enemySoundsVolume;
        LoadedMusicVolume = musicVolume;
        LoadedEnvironmentVolume = environmentVolume;
        LoadedPlayerAbilitiesVolume = playerAbilitiesVolume;
        PlayerPrefs.SetFloat("playerSoundsVolume", playerSoundsVolume);
        PlayerPrefs.SetFloat("enemySoundsVolume", enemySoundsVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("environmentVolume", environmentVolume);
        PlayerPrefs.SetFloat("playerAbilitiesVolume", playerAbilitiesVolume);
        PlayerPrefs.SetFloat("inventoryVolume", inventoryVolume);
        PlayerPrefs.SetFloat("uiVolume", uiVolume);
    }
    
    private void LoadAudioSettings()
    {
        LoadedPlayerSoundsVolume = PlayerPrefs.GetFloat("playerSoundsVolume", .5f);
        LoadedEnemySoundsVolume = PlayerPrefs.GetFloat("enemySoundsVolume", .5f);
        LoadedMusicVolume = PlayerPrefs.GetFloat("musicVolume", .5f);
        LoadedEnvironmentVolume = PlayerPrefs.GetFloat("environmentVolume", .5f);
        LoadedPlayerAbilitiesVolume = PlayerPrefs.GetFloat("playerAbilitiesVolume", .5f);
        LoadedInventoryVolume = PlayerPrefs.GetFloat("inventoryVolume", .5f);
        LoadedUiVolume = PlayerPrefs.GetFloat("uiVolume", .5f);
    }
    #endregion

    public void LoadData()
    {
        //PlayerPrefs.DeleteAll();
        resetOnRestart = PlayerPrefs.GetString("resetOnRestart","False") == "True";
        toggleResetOnRestart.isOn = resetOnRestart;
        
        if (resetOnRestart)
        {
            PlayerPrefs.DeleteAll();
        }
        //resetOnRestart = toggleResetOnRestart.isOn = false;
        
        //Level
        if (PlayerPrefs.HasKey("currentLevelName") && PlayerPrefs.GetString("currentLevelName") != "")
        {
            LoadedLevelName = PlayerPrefs.GetString("currentLevelName");
        }
        else
        {
            LoadedLevelName = startLevel;
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
        
        if (PlayerPrefs.HasKey("scrollData") && PlayerPrefs.GetString("scrollData") != "")
        {
            LoadedScrollData = JsonUtility.FromJson<ScrollData>(PlayerPrefs.GetString("scrollData"));
        }
        else
        {
            LoadedScrollData = new ScrollData();
        }
        
        baseAttributes.CurrentHealth = PlayerPrefs.HasKey("currentHealth") ? PlayerPrefs.GetInt("currentHealth") : 1000;
        baseAttributes.Mana = PlayerPrefs.HasKey("Mana") ? PlayerPrefs.GetInt("Mana") : 1000;
        ReferencesManager.Instance.fireballButton.SetActive(PlayerPrefs.GetInt("LearnedFireball") == 1 ? true : false);
        PlayerMoney.Instance.CurrentMoney = PlayerPrefs.HasKey("CurrentMoney") ? PlayerPrefs.GetInt("CurrentMoney") : 0;
        
        if (PlayerPrefs.HasKey("leverStates"))
        {
            LoadedLeverStates = JsonUtility.FromJson<LeverDataList>(PlayerPrefs.GetString("leverStates"));
        }
        else
        {
            LoadedLeverStates = new LeverDataList();
        }

        
        LoadAudioSettings();
        
        
    }



    private void OnApplicationQuit()
    {
        SaveData();
    }
}