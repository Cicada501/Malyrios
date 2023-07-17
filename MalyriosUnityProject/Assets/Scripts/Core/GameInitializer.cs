using Malyrios.Character;
using NPCs;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private GameData gameData;
    private LevelManager levelManager;
    private GameObject player;
    private BaseAttributes baseAttributes;
    private NpcManager npcManager;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        baseAttributes = player.GetComponent<BaseAttributes>();
        npcManager = GetComponent<NpcManager>();
    }

    private void Start()
    {
        LoadAndApplyData();
    }

    private void LoadAndApplyData()
    {
        gameData.LoadData();
        levelManager.ChangeLevel(gameData.LoadedLevelName);
        npcManager.LoadNpCs(gameData.LoadedNpcData);
        if (!levelManager.spawnAtPlayerDebugLocation) {
            player.transform.position = gameData.LoadedPlayerPosition;
        }
        Inventory.Instance.UpdateInventory(gameData.LoadedInventoryData);
        //GetComponent<Decision>().UpdateDecisionData(gameData.LoadedDecisionData);
        PlayerAttack.EquippedWeaponID = gameData.LoadedEquippedWeaponID;
    }
    
}