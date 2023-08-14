using System.Collections.Generic;
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
        questLogWindow = questLogWindow = ReferencesManager.Instance.questLogWindow;

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
        PlayerAttack.EquippedWeaponID = gameData.LoadedEquippedWeaponID;
        foreach (var quest in gameData.LoadedQuestLog)
        {
            questLogWindow.AddQuest(quest.questName, quest.questDescription);
        }
        questLogWindow.FixUI(10);
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        foreach (var quest in questLogWindow.quests)
        {
            questLogWindow.RemoveQuest(quest.questName);
            print($"removed:{quest.questName} ");
        }
        LoadAndApplyData();

        
    }
    
}