using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Malyrios.Items;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent = null;
    [SerializeField] GameObject inventoryUI = null;
    [SerializeField] GameObject equipmentUI = null;
    [SerializeField] GameObject activeItemInfoWindow = null;
    [SerializeField] GameObject statsWindow;
    Inventory inventory;
    private PuzzleStation activePuzzleStation;

    bool buttonPressed;

    InventorySlot[] slots;
    bool itemsLoaded = false;
    int itemCount;

    public static bool inventoryOpen = false;

    public void SetActivePuzzleStation(PuzzleStation station)
    {
        activePuzzleStation = station;
    }


    private void Awake()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        foreach (var slot in slots)
        {
            slot.Initialize();
        }
    }

    private void Start()
    {
        //called in start, because in awake the inventory instance is null. But because subscription of events must happen before the events get triggered, the script execution order of this script is set to -1
        inventory = Inventory.Instance;
        inventory.OnItemAdded += UpdateUiNew;
        inventory.OnItemRemoved += OnItemRemoved;
    }


    // Update is called once per frame
    private void Update()
    {
        
        if (!itemsLoaded)
        {
            itemsLoaded = true;
        }

    }

    private void UpdateUiNew(BaseItem item)
    {
        InventorySlot tryToStack =
            slots.FirstOrDefault(x => x.Item != null && x.Item.ItemName == item.ItemName && x.Item.IsStackable);
        if (tryToStack != null)
        {
            if (!tryToStack.AddItemToStack(item))
            {
                AddNewItem(item);
            }
        }
        else
        {
            AddNewItem(item);
        }
        
        
    }

    private void AddNewItem(BaseItem item)
    {
        InventorySlot freeSlot = slots.FirstOrDefault(x => x.Item == null);
        if (freeSlot != null)
        {
            freeSlot.Initialize();
            print("freeslot init");
            freeSlot.SetItem(item);
        }
    }

    private void OnItemRemoved(BaseItem item)
    {
        InventorySlot it = slots.FirstOrDefault(x => x.Item == item);
        if (it != null) it.RemoveSingleItem();
    }


    private int GetOccurrence(Item item, List<Item> itemList)
    {
        int occurrences = 0;
        foreach (Item _item in itemList)
        {
            if (_item == item)
            {
                occurrences++;
            }
        }

        return occurrences;
    }


    public void ChangeInventoryOpened()
    {
        inventoryOpen = !inventoryOpen;
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        equipmentUI.SetActive(inventoryUI.activeSelf); // if inventory open, then also open equipment, and on close, close equipment window
        
        //if puzzleStation is active, close it with inventory
        if (activePuzzleStation)
        {
            activePuzzleStation.ClosePuzzleWindow();
        }
        //stats- and activeItemWindow are always closed, when inventory gets opened or closed
        activeItemInfoWindow.SetActive(false);
        statsWindow.SetActive(false);
    }
}