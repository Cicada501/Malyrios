﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Malyrios.Items;
using SaveAndLoad;
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
    
    public bool isEmpty = true;

    #region new inventory

    public List<BaseItem> Items = new List<BaseItem>();
    public List<int> ItemIDs = new List<int>();

    public event Action<BaseItem> OnItemAdded;
    public event Action<BaseItem> OnItemRemoved;
    public event Action<BaseItem> OnActiveItemSet;

    private BaseWeapon testWeapon;

    public BaseItem activeItem = null;

    #endregion
    
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
        }
        else
        {
            activeItem = null;
        }
        
    }
}