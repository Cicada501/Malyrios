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
    
    public List<Item> items = new List<Item>();
    public static bool itemsLoaded = false;

    #region new inventory
    
    public List<BaseItem> Items = new List<BaseItem>();
    
    public event Action<BaseItem> OnItemAdded;
    public event Action<BaseItem> OnItemRemoved;
    private BaseWeapon testWeapon;
    
    #endregion
    
    
    private void Start()
    {
        // This is just for test purposes
        testWeapon = ScriptableObject.CreateInstance<BaseWeapon>().InitItem();
        AddItem(testWeapon);
    }

    //Neccecary to use OnSceneLoaded
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        items = StaticData.itemsStatic;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(BaseItem item)
    {
        Items.Add(item);
        OnItemAdded?.Invoke(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void Remove(BaseItem item)
    {
        Items.Remove(item);
        OnItemRemoved?.Invoke(item);
    }
}