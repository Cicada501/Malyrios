using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler, IOnSlotTap, ISlot
{
    #region Events

    public static event Action<BaseItem> OnItemSlotChanged;
    public static event Action<BaseWeapon> OnWeaponChanged;
    public static event Action<BaseArmor> OnArmorChanged;

    #endregion

    [SerializeField] private BaseItem.ItemTypes itemType = 0;

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
        OnWeaponChanged?.Invoke(null);  //calls onWeaponChanged with null
    }

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
        child.GetComponent<DragNDrop>().MySlot = this;
        LoadWeapon();
        //inventoryUI = GameObject.Find("Canvas UI").GetComponent<InventoryUI>();
        //inventoryUI.changeInventoryOpened();
    }

   

    private void LoadWeapon()
    {
        
        if(PlayerAttack.EquippedWeaponID == 0) return;
        if (this.gameObject.name == "WeaponSlot")
        {
            AddWeapon(ItemDatabase.GetWeapon(PlayerAttack.EquippedWeaponID));
        }
    }
    public void AddWeapon(BaseWeapon weapon)
    {
        child.GetComponent<Image>().sprite = weapon.Icon;
        Item = weapon;
        transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
        //TriggerSlotEvent();
    }

    private void TriggerSlotEvent()
    {
        Debug.Log("Triggered Slot Event");
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
            case BaseItem.ItemTypes.Other:
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

    

    public void OnTap()
    {
        Inventory.Instance.SetActiveItem(Item);
    }
}

public class BaseArmor
{
}