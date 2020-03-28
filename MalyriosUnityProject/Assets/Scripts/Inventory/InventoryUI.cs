﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    public GameObject inventoryUI;

    bool buttonPressed;

    InventorySlot[] slots;
    bool itemsLoaded = false;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!itemsLoaded){
            UpdateUI();
            itemsLoaded = true;
        }

        //Dont Open/Close inventory Very often on button hold
        if(Player.inventoryInput && !buttonPressed){
            changeInventoryOpened();
            buttonPressed = true;
        }else if(!Player.inventoryInput){
            buttonPressed = false;
        }
    }

    void UpdateUI(){
        for (int i = 0; i < slots.Length; i++)
        {
            if( i < inventory.items.Count){
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void changeInventoryOpened(){
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
