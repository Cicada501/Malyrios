using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    //public List<Item> items = new List<Item>();
    public static bool itemsLoaded = false;

    #region new inventory
    
    public List<BaseItem> Items = new List<BaseItem>();
    public List<int> ItemIDs = new List<int>();
    
    public event Action<BaseItem> OnItemAdded;
    public event Action<BaseItem> OnItemRemoved;
    private BaseWeapon testWeapon;
    
    #endregion
    
    [SerializeField] private ItemDatabase database;
    
    private void Start()
    {
        InvokeRepeating("SaveInventory", 2f, 2f);  //1s delay, repeat every 1s
        // This is just for test purposes
        testWeapon = ScriptableObject.CreateInstance<BaseWeapon>().InitItem();
        AddItem(testWeapon);
        LoadInventory();
    }

    //Neccecary to use OnSceneLoaded
    /* void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    } */

    /* void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        items = StaticData.itemsStatic;
    } */
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(BaseItem item)
    {
        ItemIDs.Add(item.ItemID);
        Items.Add(item);
        OnItemAdded?.Invoke(item);
    }

    void Update(){
        foreach (var item in ItemIDs)
        {
            print("Item ID:"+ item);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void Remove(BaseItem item)
    {
        
        ItemIDs.Remove(item.ItemID);
        Items.Remove(item);
        OnItemRemoved?.Invoke(item);
    }

    public void SaveInventory(){
        SaveSystem.saveInventory(this);
    }

    public void LoadInventory(){
        InventoryData data = SaveSystem.LoadInventory();
        //int[] testData = new int[] {2,3,4};
        foreach (var item in data.itemIDs)
        {
            AddItem(database.GetItem(item));
        }
    }

}