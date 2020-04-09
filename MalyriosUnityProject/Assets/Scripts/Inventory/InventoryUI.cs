using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Malyrios.Items;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent;
    [SerializeField] GameObject inventoryUI;
    Inventory inventory;

    bool buttonPressed;

    InventorySlot[] slots;
    bool itemsLoaded = false;
    int itemCount;

    public static int d;

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
            int itemOccurrence = GetOccurrence(inventory.items[i], inventory.items);
            int firstEmptySlot = Array.IndexOf(slots.Select(x => x.item).ToArray(), null);
            print(firstEmptySlot);
            //Collected new Items
            if (pos == -1){
                slots[firstEmptySlot].AddItem(inventory.items[i]);
                if(itemOccurrence > 1){
                    slots[firstEmptySlot].amount = itemOccurrence - 1;
                    d += 1;
                }else{
                    slots[firstEmptySlot].amount = 1;
                }
                print("first");

            }
            //Collected one Item to Stack
            else if (itemOccurrence == slots[pos].amount + 1){
                slots[pos].amount++;              
                d ++; //speichere Anzahl an items, die keinen eigenen Slot benötigen
                print("second");
            }
            print("D: "+d+ " i:"+i+" slots[i].amount: "+slots[i].amount+ " Occurrence: "+itemOccurrence);
        
        }
        //Clear empty slots
        foreach (InventorySlot slot in slots)
        {
            if(slot.amount == 0){
                slot.ClearSlot();
            }
        }
        if(inventory.items.Count == 0){
            foreach (InventorySlot slot in slots){
                slot.ClearSlot();
            }
        }


    }

    int GetOccurrence(Item item, List<Item> itemList)
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
