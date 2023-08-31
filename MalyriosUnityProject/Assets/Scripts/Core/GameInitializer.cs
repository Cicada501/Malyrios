using System.Collections.Generic;
using Malyrios.Character;
using NPCs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    private GameData gameData;
    private LevelManager levelManager;
    private GameObject player;
    private BaseAttributes baseAttributes;
    private NPCManager npcManager;
    private QuestLogWindow questLogWindow;
    private PlayerAttack playerAttack;


    private void Awake()
    {
        gameData = GetComponent<GameData>();
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        baseAttributes = player.GetComponent<BaseAttributes>();
        npcManager = GetComponent<NPCManager>();
        questLogWindow = ReferencesManager.Instance.questLogWindow;
        playerAttack = ReferencesManager.Instance.playerAttack;

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
        playerAttack.LoadWeapon(gameData.LoadedEquippedWeaponID);
        foreach (var quest in gameData.LoadedQuestLog)
        {
            questLogWindow.AddQuest(quest.questName, quest.questDescription);
        }
        questLogWindow.FixUI(10);
        PuzzleStationManager.Instance.LoadStations();
        
        if(gameData.LoadedArmorData.headArmorID!=0)ReferencesManager.Instance.headArmorSlot.LoadArmor(gameData.LoadedArmorData.headArmorID);
        if(gameData.LoadedArmorData.bodyArmorID!=0)ReferencesManager.Instance.bodyArmorSlot.LoadArmor(gameData.LoadedArmorData.bodyArmorID);
        if(gameData.LoadedArmorData.handArmorID!=0)ReferencesManager.Instance.handArmorSlot.LoadArmor(gameData.LoadedArmorData.handArmorID);
        if(gameData.LoadedArmorData.feetArmorID!=0)ReferencesManager.Instance.feetArmorSlot.LoadArmor(gameData.LoadedArmorData.feetArmorID);
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        // foreach (var quest in questLogWindow.quests)
        // {
        //     questLogWindow.RemoveQuest(quest.questName);
        //     print($"removed:{quest.questName} ");
        // }
        //SceneManager.LoadScene(0);
        LoadAndApplyData();
        Inventory.Instance.RemoveAllItems();
        


    }
    
}