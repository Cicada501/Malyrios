﻿using System.Collections.Generic;
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
    public Stack<BaseItem> ItemStack { get; set; }
    private InventoryUI inventoryUI;
    

    public void SetItem(BaseItem item)
    {
    }

    public void RemoveItem()
    {
        child.GetComponent<Image>().enabled = false;
    }

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
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

        // Set the equip item to the item from the dragged item.
        Item = slot.Item;
        slot.ItemStack?.Clear();

        Inventory.Instance.Remove(slot.Item);

        // enable the image.
        transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;

        // Disable the image from the dragged item.
        eventData.pointerDrag.GetComponent<Image>().enabled = false;

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
            this.gridLayoutGroup.GetComponent<RectTransform>().anchoredPosition;
        
        TriggerSlotEvent();
    }

    

    public void OnTap()
    {
        Inventory.Instance.SetActiveItem(Item);
    }
}