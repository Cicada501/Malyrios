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
    [SerializeField] Transform itemsParent;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject EquipmentUI;
    Inventory inventory;

    bool buttonPressed;

    InventorySlot[] slots;
    bool itemsLoaded = false;
    int itemCount;

    public static int d;

    private void Awake()
    {
        inventory = Inventory.Instance;
        inventory.OnItemAdded += UpdateUiNew;
        inventory.OnItemRemoved += OnItemRemoved;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        itemCount = inventory.items.Count;

        // amount of items, that already existed in a Slot
        d = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!itemsLoaded)
        {
            // UpdateUI();
            itemsLoaded = true;
        }

        //Dont Open/Close inventory Very often on button hold
        if (Player.inventoryInput && !buttonPressed)
        {
            changeInventoryOpened();
            buttonPressed = true;
        }
        else if (!Player.inventoryInput)
        {
            buttonPressed = false;
        }
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

    private void UpdateUI()
    {
        // for (int i = 0; i < inventory.items.Count; i++)
        // {
        //     //gets position of items[i] in slots, if no slot has it yet, its -1
        //     int pos = Array.IndexOf(slots.Select(x => x.item).ToArray(), inventory.items[i]);
        //     int itemOccurrence = GetOccurrence(inventory.items[i], inventory.items);
        //     int firstEmptySlot = Array.IndexOf(slots.Select(x => x.item).ToArray(), null);
        //     print(firstEmptySlot);
        //     //Collected new Items
        //     if (pos == -1)
        //     {
        //         slots[firstEmptySlot].AddItem(inventory.items[i]);
        //         if (itemOccurrence > 1)
        //         {
        //             slots[firstEmptySlot].amount = itemOccurrence - 1;
        //             d += 1;
        //         }
        //         else
        //         {
        //             slots[firstEmptySlot].amount = 1;
        //         }
        //
        //         print("first");
        //     }
        //     //Collected one Item to Stack
        //     else if (itemOccurrence == slots[pos].amount + 1)
        //     {
        //         slots[pos].amount++;
        //         d++; //speichere Anzahl an items, die keinen eigenen Slot benötigen
        //         print("second");
        //     }
        //
        //     print("D: " + d + " i:" + i + " slots[i].amount: " + slots[i].amount + " Occurrence: " + itemOccurrence);
        // }
        //
        // //Clear empty slots
        // foreach (InventorySlot slot in slots)
        // {
        //     if (slot.amount == 0)
        //     {
        //         slot.ClearSlot();
        //     }
        // }
        //
        // if (inventory.items.Count == 0)
        // {
        //     foreach (InventorySlot slot in slots)
        //     {
        //         slot.ClearSlot();
        //     }
        // }
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


    public void changeInventoryOpened()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        EquipmentUI.SetActive(!EquipmentUI.activeSelf); // if invontory open, then also open equipment, and on close, close equipment window

        if (!this.inventoryUI.activeSelf)
        {
            UIManager.Instance.HideTooltip();
        }
    }
}