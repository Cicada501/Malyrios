﻿using System;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleSlot : MonoBehaviour, IDropHandler, IOnSlotTap, ISlot
{
    BaseItem.ItemTypes itemType = BaseItem.ItemTypes.Rune;

    private GameObject child;
    private GridLayoutGroup gridLayoutGroup;
    public BaseItem Item { get; set; }
    public Stack<BaseItem> ItemStack { get; set; } //not used here, but Interface needs it
    private PuzzleStation puzzleStation;
    [SerializeField] private Image slotImage;
    

    public void SetItem(BaseItem item)
    {
        print($"SetItem of PuzzleSlot is called, what is null? item? {item.ItemName},");
        print($"item.Icon? {item.Icon!=null}");
        
        slotImage.sprite = item.Icon;
        slotImage.enabled = true;
        Item = item;
        
    }


    public void RemoveItem()
    {
        print("removing puzzleSlot item");
        slotImage.enabled = false;
        Item = null;
        int slotIndex = transform.GetSiblingIndex();
        puzzleStation.UpdateItemID(slotIndex, 0);
    }

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        child.GetComponent<DragNDrop>().MySlot = this;
    }

   
    

    private void TriggerSlotEvent()
    {
        Debug.Log("Triggered Puzzle Slot Event");
    }
    

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        // Get the slot of the dragged item.
        ISlot slot = eventData.pointerDrag.GetComponent<DragNDrop>().MySlot;

        // If the dragged item is not the same item type, you can't drag it on this item slot, so do nothing.
        if (slot.Item.ItemType != this.itemType) return;


        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Set the slot sprite to the sprite of the item.
        this.child.GetComponent<Image>().sprite =
            eventData.pointerDrag.GetComponent<Image>().sprite;

        // Set the puzzleSlot item to the item from the dragged item.
        Item = slot.Item;
        
        slot.RemoveItem();

        // enable the image.
        transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;

        // Disable the image from the dragged item.
        eventData.pointerDrag.GetComponent<Image>().enabled = false;
        
        // Update the itemIDsArray in the PuzzleStation
        int slotIndex = transform.GetSiblingIndex();
        puzzleStation.UpdateItemID(slotIndex, Item.ItemID);
        TriggerSlotEvent();
    }

    private void Update()
    {
        print($"Slot{transform.GetSiblingIndex()} contains a item? : {Item!=null}");
    }


    public void OnTap()
    {
        Inventory.Instance.SetActiveItem(Item);
    }
    
    public void SetPuzzleStation(PuzzleStation station)
    {
        puzzleStation = station;
    }
}