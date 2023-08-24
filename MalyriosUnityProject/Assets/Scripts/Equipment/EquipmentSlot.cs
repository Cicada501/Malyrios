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
    
    public static event Action<BaseWeapon> OnWeaponChanged;
    public static event Action<BaseArmor, BaseItem.ItemTypes> OnArmorChanged;

    #endregion

    [SerializeField] public BaseItem.ItemTypes itemType = 0;

    private GameObject child;
    private GridLayoutGroup gridLayoutGroup;

    public BaseItem Item { get; set; }
    
    private Stack<BaseItem> itemStack = new Stack<BaseItem>();
    public Stack<BaseItem> ItemStack
    {
        get => this.itemStack;
        set => this.itemStack = value;
    }
    private InventoryUI inventoryUI;
    
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
        child.GetComponent<DragNDrop>().MySlot = this;
    }
    

    public void SetItem(BaseItem item)
    {
        if (item.ItemType == BaseItem.ItemTypes.Weapon)
        {
            AddWeapon(item as BaseWeapon);
        }
    }

    public void RemoveItem()
    {
        child.GetComponent<Image>().enabled = false;
        switch (this.gameObject.name)
        {
            case "WeaponSlot":
                OnWeaponChanged?.Invoke(null);
                break;
            case "HeadArmorSlot":
                OnArmorChanged?.Invoke(null, BaseItem.ItemTypes.Head);
                break;
            case "BodyArmorSlot":
                OnArmorChanged?.Invoke(null, BaseItem.ItemTypes.Body);
                break;
            case "HandArmorSlot":
                OnArmorChanged?.Invoke(null, BaseItem.ItemTypes.Hand);
                break;
            case "FeetArmorSlot":
                OnArmorChanged?.Invoke(null, BaseItem.ItemTypes.Feet);
                break;
        }
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void LoadWeapon(int id)
    {
        //if (this.gameObject.name == "WeaponSlot")
        {
            AddWeapon(ItemDatabase.GetWeapon(id));
        }
    }
    public void AddWeapon(BaseWeapon weapon)
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = weapon.Icon;
        Item = weapon;
        transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
    }
    
    //Used if "Use" Button of active item is pressed to change Armor
    public void InvokeChangeArmor(BaseArmor armor)
    {
        AddArmor(armor);
        OnArmorChanged?.Invoke(armor, armor.ItemType);
    }

    public void AddArmor(BaseArmor armor)
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = armor.Icon;
        Item = armor;
        transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
    }

    
    //Used after Drag and Drop to Invoke Event of equipment change
    private void TriggerSlotEvent()
    {
        switch (this.itemType)
        {
            case BaseItem.ItemTypes.Weapon:
                OnWeaponChanged?.Invoke(Item as BaseWeapon);
                break;
            case BaseItem.ItemTypes.Head:
                OnArmorChanged?.Invoke(Item as BaseArmor, itemType);
                break;
            case BaseItem.ItemTypes.Body:
                OnArmorChanged?.Invoke(Item as BaseArmor, itemType);
                break;
            case BaseItem.ItemTypes.Feet:
                OnArmorChanged?.Invoke(Item as BaseArmor, itemType);
                break;
            case BaseItem.ItemTypes.Hand:
                OnArmorChanged?.Invoke(Item as BaseArmor, itemType);
                break;
            case BaseItem.ItemTypes.Plant:
                break;
            case BaseItem.ItemTypes.Other:
                break;
            case BaseItem.ItemTypes.Rune:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void InvokeChangeWeapon(BaseWeapon weapon)
    {
        if (this.gameObject.name == "WeaponSlot" && !Item )
        {
            AddWeapon(weapon);
        }

        OnWeaponChanged?.Invoke(weapon);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        // Get the slot of the dragged item.
        ISlot originSlot = eventData.pointerDrag.GetComponent<DragNDrop>().MySlot;

        // If the dragged item is not the same item type, you can't drag it on this item slot, so do nothing.
        if (originSlot.Item.ItemType != this.itemType) return;

        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // If the current equipment slot has an item
        if (Item != null)
        {
            // Swap the items
            SwapItems(originSlot);
            TriggerSlotEvent();
        }
        else
        {
            // If this equipment slot has no item, use your existing logic
            this.child.GetComponent<Image>().sprite =
                eventData.pointerDrag.GetComponent<Image>().sprite;
        
            Item = originSlot.Item;
            originSlot.RemoveItem();
        
            // enable the image.
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;

            // Disable the image from the dragged item.
            eventData.pointerDrag.GetComponent<Image>().enabled = false;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                this.gridLayoutGroup.GetComponent<RectTransform>().anchoredPosition;
        
            TriggerSlotEvent();
        }
    }


    

    public void OnTap()
    {
        Inventory.Instance.SetActiveItem(Item);
    }
    
    public void SwapItems(ISlot otherSlot)
    {
        SlotHelper.SwapItems(this, otherSlot);
    }
}