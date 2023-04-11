using Malyrios.Character;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private GameData gameData;
    private LevelManager levelManager;
    private GameObject player;
    private BaseAttributes baseAttributes;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
        levelManager = GetComponent<LevelManager>();
        player = ReferencesManager.Instance.player;
        baseAttributes = player.GetComponent<BaseAttributes>();
    }

    private void Start()
    {
        LoadAndApplyData();
    }

    private void LoadAndApplyData()
    {
        gameData.LoadData();
        
        levelManager.ChangeLevel(gameData.LoadedLevelName);
        player.transform.position = gameData.LoadedPlayerPosition;
        baseAttributes.SetHealth(gameData.LoadedCurrentHealth);

        // Apply inventory data (assuming you have a method to update your inventory with loaded data)
        Inventory.Instance.UpdateInventory(gameData.LoadedInventoryData);
    }
}