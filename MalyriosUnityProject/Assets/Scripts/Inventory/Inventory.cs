using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Malyrios.Core;
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

    #region new inventory

    public List<BaseItem> Items = new List<BaseItem>();
    public List<int> ItemIDs = new List<int>();

    public event Action<BaseItem> OnItemAdded;
    public event Action<BaseItem> OnItemRemoved;

    private BaseWeapon testWeapon;

    #endregion

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
        OnItemAdded?.Invoke(item);
        isEmpty = false;
    }
    
    public void Remove(BaseItem item)
    {
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
        return occ;
    }
    

}