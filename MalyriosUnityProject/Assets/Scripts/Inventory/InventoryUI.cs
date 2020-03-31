using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    public GameObject inventoryUI;

    bool buttonPressed;

    InventorySlot[] slots;
    bool itemsLoaded = false;
    int itemCount;

    int d;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        itemCount = inventory.items.Count;

        // amount of items, that already existed in a Slot
        d = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!itemsLoaded)
        {
            UpdateUI();
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

    void UpdateUI()
    {

        for (int i = 0; i < inventory.items.Count; i++){
            
            //gets position of items[i] in slots, if no slot has it yet, its -1
            int pos = Array.IndexOf(slots.Select(x => x.item).ToArray(), inventory.items[i]);
            int itemOccurrence = GetOccurrences(inventory.items[i], inventory.items);

            //Collected new Item
            if (pos == -1){
                slots[i-d].AddItem(inventory.items[i]);
                slots[i-d].amount = 1;

            }
            //Collected Item to Stack
            else if (itemOccurrence == slots[pos].amount + 1){
                slots[pos].amount++;              
                d ++; //speichere Anzahl an items, die keinen eigenen Slot benötigen
            }

        }

    }

    int GetOccurrences(Item item, List<Item> itemList)
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
    }
}
