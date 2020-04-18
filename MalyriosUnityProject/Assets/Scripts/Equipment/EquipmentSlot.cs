﻿using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler, IOnSlotRightClick, ISlot
{
    #region Events

    public static event Action<BaseItem> OnItemSlotChanged;
    public static event Action<BaseWeapon> OnWeaponChanged;

    #endregion

    [SerializeField] private BaseItem.ItemTypes itemType;

    private GameObject child;
    private GridLayoutGroup gridLayoutGroup;

    public BaseItem Item { get; set; }
    public Stack<BaseItem> ItemStack { get; set; }

    public void SetItem(BaseItem item)
    {
    }

    public void RemoveItem()
    {
        child.GetComponent<Image>().enabled = false;
        OnWeaponChanged?.Invoke(null);
    }

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
        child.GetComponent<DragNDrop>().MySlot = this;
    }

    private void TriggerSlotEvent()
    {
        switch (this.itemType)
        {
            case BaseItem.ItemTypes.Weapon:
                OnWeaponChanged?.Invoke(Item as BaseWeapon);
                break;
            case BaseItem.ItemTypes.Head:
                break;
            case BaseItem.ItemTypes.Body:
                break;
            case BaseItem.ItemTypes.Feet:
                break;
            case BaseItem.ItemTypes.Hand:
                break;
            case BaseItem.ItemTypes.Plant:
                break;
            case BaseItem.ItemTypes.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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

    public void OnRightMouseButtonClick()
    {
    }
}