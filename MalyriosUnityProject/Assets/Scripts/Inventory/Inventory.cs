using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
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
