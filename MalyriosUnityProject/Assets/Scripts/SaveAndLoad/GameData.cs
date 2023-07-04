using System;
using Malyrios.Character;
using SaveAndLoad;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Vector3 LoadedPlayerPosition { get; private set; }
    //public float LoadedCurrentHealth { get; private set; }
    public InventoryData LoadedInventoryData { get; private set; }
    public string LoadedLevelName { get; private set; }
    public DecisionData LoadedDecisionData { get; private set; }

    public int LoadedEquippedWeaponID { get; private set; }
    
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
        //PlayerPrefs.SetFloat("currentHealth", baseAttributes.CurrentHealth);

        InventoryData inventoryData = new InventoryData(Inventory.Instance);
        //print($"saved inventory data: {JsonUtility.ToJson(inventoryData)}");
        PlayerPrefs.SetString("inventoryData", JsonUtility.ToJson(inventoryData));
        DecisionData decisionData = new DecisionData();
        PlayerPrefs.SetString("decisionData", JsonUtility.ToJson(decisionData));
        PlayerPrefs.SetInt("EquippedWeaponID", PlayerAttack.EquippedWeaponID);
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


        PlayerPrefs.Save();
    }

    public void LoadData()
    {
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
            LoadedPlayerPosition = Vector3.zero;
        }

        // // Load current health
        // if (PlayerPrefs.HasKey("currentHealth") && PlayerPrefs.GetFloat("currentHealth") != 0f)
        // {
        //     LoadedCurrentHealth = PlayerPrefs.GetFloat("currentHealth");
        // }
        // else
        // {
        //     LoadedCurrentHealth = baseAttributes.MaxHealth;
        // }

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
        
        if (PlayerPrefs.HasKey("EquippedWeaponID")&&PlayerPrefs.GetString("EquippedWeaponID")!="")
        {
            LoadedEquippedWeaponID = PlayerPrefs.GetInt("EquippedWeaponID");
        }
        else
        {
            LoadedEquippedWeaponID = 1;
        }if (PlayerPrefs.HasKey("AttributesData") && PlayerPrefs.GetString("AttributesData") != "")
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
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}