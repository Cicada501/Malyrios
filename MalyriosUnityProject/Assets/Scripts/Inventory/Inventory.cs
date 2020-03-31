using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion


    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public int slotCount = 200;
    public List<Item> items = new List<Item>();

    public static bool itemsLoaded = false;


    //Neccecary to use OnSceneLoaded
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        items = StaticData.itemsStatic;
        
    }


    public bool Add(Item item)
    {
        if (items.Count >= slotCount)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        items.Add(item);
        
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
        return true;

    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }


}
