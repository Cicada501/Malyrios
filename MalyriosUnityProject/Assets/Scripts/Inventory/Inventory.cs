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

    public static bool itemsLoaded = false;
    public bool isEmpty = true;

    #region new inventory

    public List<BaseItem> Items = new List<BaseItem>();
    public List<int> ItemIDs = new List<int>();

    public event Action<BaseItem> OnItemAdded;
    public event Action<BaseItem> OnItemRemoved;
    private BaseWeapon testWeapon;

    #endregion

    [SerializeField] private ItemDatabase database = null;

    private void Start()
    {
        LoadInventory();
        //Save Inventory state each second
        InvokeRepeating("SaveInventory", 2f, 2f); //2s delay, repeat every 2s
        //InvokeRepeating("PrintInventory", 1f, 1f);
        // This is just for test purposes
        testWeapon = ScriptableObject.CreateInstance<BaseWeapon>().InitItem();
        AddItem(testWeapon);
    }

    void PrintInventory()
    {
        Debug.Log(ItemIDs);
        foreach (var item in ItemIDs)
        {
            Debug.Log(item);
        }
    }

    public void AddItem(BaseItem item)
    {
        ItemIDs.Add(item.ItemID);
        Items.Add(item);
        OnItemAdded?.Invoke(item);
        isEmpty = false;
    }

    public void Remove(BaseItem item)
    {
        ItemIDs.Remove(item.ItemID);
        Items.Remove(item);
        OnItemRemoved?.Invoke(item);
        if (ItemIDs.Count == 0)
        {
            isEmpty = true;
        }
    }

    public void SaveInventory()
    {
        SaveSystem.SaveInventory(this);
    }

    public void LoadInventory()
    {
        InventoryData data = SaveSystem.LoadInventory();
        //int[] testData = new int[] {2,3,4};


        foreach (var itemID in data.itemIDs)
        {
            AddItem(database.GetItem(itemID));
        }
        //get Stack size from amout of occurences in ItemIDs
    }
}