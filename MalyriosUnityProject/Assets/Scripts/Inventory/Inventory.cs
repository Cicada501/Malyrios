using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int slotCount = 20;
    public List<Item> items = new List<Item>();

    void Start()
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
