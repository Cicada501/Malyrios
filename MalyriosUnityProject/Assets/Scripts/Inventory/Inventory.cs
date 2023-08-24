﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Malyrios.Items;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    public bool isEmpty = true;
    [SerializeField] private Button useButton;
    public BaseItem activeItem = null;
    public InventorySlot activeSlot = null;
    #region new inventory

    public List<BaseItem> Items = new List<BaseItem>();
    public List<int> ItemIDs = new List<int>();

    public event Action<BaseItem> OnItemAdded;
    public event Action<BaseItem> OnItemRemoved;
    public event Action<BaseItem> OnActiveItemSet;

    private BaseWeapon testWeapon;

    #endregion
    
    void PrintInventory()
    {
        List<string> itemNames = new List<string>();
        print($"items length: {Items.Count}");
        foreach (var item in Items)
        {
            itemNames.Add(item != null ? item.ItemName : "Null");
        }
        Debug.Log("IDs: [" + string.Join(",", ItemIDs) + "]");
        Debug.Log("Item Names: [" + string.Join(", ", itemNames) + "]");
    }

    public void RemoveAllItems()
    {
        List<BaseItem> itemsToRemove = new List<BaseItem>(Items);
        foreach (var item in itemsToRemove)
        {
            Remove(item);
        }
    }


    public void AddItem(BaseItem item)
    {
        print("Adding an item");
        ItemIDs.Add(item.ItemID);
        Items.Add(item);
        OnItemAdded?.Invoke(item);
        isEmpty = false;
    }
    
    public void Remove(BaseItem item)
    {
        print($"try to remove: {item.ItemName}");
        ItemIDs.Remove(item.ItemID);
        //Debug.Log("Item removed"+ItemIDs.Count);
        Items.Remove(item);
        OnItemRemoved?.Invoke(item);
        if (ItemIDs.Count == 0)
        {
            isEmpty = true;
        }
    }

    public void UpdateInventory(InventoryData gameDataLoadedInventoryData)
    {
        foreach (var itemID in gameDataLoadedInventoryData.itemIDs)
        {
            if(ItemDatabase.GetItem(itemID) != null)
            {
                AddItem(ItemDatabase.GetItem(itemID));
                
            }else if (ItemDatabase.GetWeapon(itemID)!= null)
            {
                AddItem(ItemDatabase.GetWeapon(itemID));
            }
            else if (ItemDatabase.GetArmor(itemID)!= null)
            {
                AddItem(ItemDatabase.GetArmor(itemID));
            }
            else 
            {
                Debug.Log($"Item with ID {itemID} not found");
            }
            
        }
    }

    public static int CountOccurrences(BaseItem item)
    {
        
        var occ = Instance.Items.Count(n => n == item);
        //print($"I found {occ} {item.ItemName}s ");
        return occ;
    }
    
    public void SetActiveItem(BaseItem item)
    {
        OnActiveItemSet?.Invoke(item);
        
        if (activeItem == null || activeItem != item)
        {
            activeItem = item;
            if (item.IsUsable)
            {
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }
        }
        else
        {
            activeItem = null;
        }
        
    }
}