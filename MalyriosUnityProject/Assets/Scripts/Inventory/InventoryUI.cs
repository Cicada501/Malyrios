using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Malyrios.Items;
using Malyrios.UI;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent = null;
    [SerializeField] GameObject inventoryUI = null;
    [SerializeField] GameObject EquipmentUI = null;
    Inventory inventory;

    bool buttonPressed;

    InventorySlot[] slots;
    bool itemsLoaded = false;
    int itemCount;

    public static bool inventoryOpen = false;
    public static int d;

    private void Awake()
    {
        inventory = Inventory.Instance;
        print(inventory);
        inventory.OnItemAdded += UpdateUiNew;
        inventory.OnItemRemoved += OnItemRemoved;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //itemCount = inventory.items.Count;

        // amount of items, that already existed in a Slot
        d = 0;
    }
    

    // Update is called once per frame
    private void Update()
    {
        if (!itemsLoaded)
        {
            itemsLoaded = true;
        }

        //Dont Open/Close inventory Very often on button hold
        // if (Player.inventoryInput && !buttonPressed)
        // {
        //     ChangeInventoryOpened();
        //     buttonPressed = true;
        // }
        // else if (!Player.inventoryInput)
        // {
        //     buttonPressed = false;
        // }
    }

    private void UpdateUiNew(BaseItem item)
    {
        InventorySlot tryToStack = slots.FirstOrDefault(x => x.Item != null && x.Item.ItemName == item.ItemName && x.Item.IsStackable);
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
        if (freeSlot != null) freeSlot.SetItem(item);
    }

    private void OnItemRemoved(BaseItem item)
    {
        InventorySlot it = slots.FirstOrDefault(x => x.Item == item);
        if (it != null) it.RemoveItem();
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
        EquipmentUI.SetActive(!EquipmentUI.activeSelf); // if invontory open, then also open equipment, and on close, close equipment window

        if (!this.inventoryUI.activeSelf)
        {
            UIManager.Instance.HideTooltip();
        }
    }
}