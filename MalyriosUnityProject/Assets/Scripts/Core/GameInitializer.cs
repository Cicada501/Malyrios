using Malyrios.Character;
using NPCs;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private GameData gameData;
    private LevelManager levelManager;
    private GameObject player;
    private BaseAttributes baseAttributes;
    private NPCManager npcManager;
    private QuestLogWindow questLogWindow;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        baseAttributes = player.GetComponent<BaseAttributes>();
        npcManager = GetComponent<NPCManager>();
        questLogWindow = FindObjectOfType<QuestLogWindow>();
    }

    private void Start()
    {
        LoadAndApplyData();
    }

    private void LoadAndApplyData()
    {
        gameData.LoadData();
        npcManager.LoadNpCs(gameData.LoadedNpcData);
        levelManager.ChangeLevel(gameData.LoadedLevelName);
        if (!levelManager.spawnAtPlayerDebugLocation) {
            player.transform.position = gameData.LoadedPlayerPosition;
        }
        Inventory.Instance.UpdateInventory(gameData.LoadedInventoryData);
        //GetComponent<Decision>().UpdateDecisionData(gameData.LoadedDecisionData);
        PlayerAttack.EquippedWeaponID = gameData.LoadedEquippedWeaponID;
        questLogWindow.quests = gameData.LoadedQuestLog;
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        LoadAndApplyData();
    }
    
}