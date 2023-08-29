using System;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IOnSlotTap, ISlot
{
    [SerializeField] Text amountText = null;

    private Stack<BaseItem> itemStack = new Stack<BaseItem>();
    private BaseItem item;
    private Transform playerTransform;
    private DragNDrop dragNDrop;
    private EquipmentSlot weaponSlot;
    private EquipmentSlot headArmorSlot;
    private EquipmentSlot bodyArmorSlot;
    private EquipmentSlot handArmorSlot;
    private EquipmentSlot feetArmorSlot;


    public BaseItem Item
    {
        get => this.item;
        set => this.item = value;
    }

    public Stack<BaseItem> ItemStack
    {
        get => this.itemStack;
        set => this.itemStack = value;
    }

    public void Start()
    {
        this.playerTransform = ReferencesManager.Instance.player.transform;
        this.dragNDrop = this.transform.GetChild(2).GetComponent<DragNDrop>();
        this.dragNDrop.MySlot = this;
        weaponSlot = ReferencesManager.Instance.weaponSlot;
        headArmorSlot = ReferencesManager.Instance.headArmorSlot;
        bodyArmorSlot = ReferencesManager.Instance.bodyArmorSlot;
        handArmorSlot = ReferencesManager.Instance.handArmorSlot;
        feetArmorSlot = ReferencesManager.Instance.feetArmorSlot;
    }

    public void Initialize()
    {
        this.playerTransform = ReferencesManager.Instance.player.transform;
        this.dragNDrop = this.transform.GetChild(2).GetComponent<DragNDrop>();
        this.dragNDrop.MySlot = this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="baseItem"></param>
    public void SetItem(BaseItem baseItem)
    {
        this.item = baseItem;

        Image img = dragNDrop.GetComponent<Image>();
        img.enabled = true;
        img.sprite = baseItem.Icon;
        Inventory.Instance.ItemIDs.Add(baseItem.ItemID);
        Inventory.Instance.Items.Add(baseItem);

        dragNDrop.MySlot = this; // Optional, wenn die Zuweisung bereits im Start erfolgte
        
        this.itemStack.Push(baseItem);
        
    }

    private void Update()
    {
        amountText.text = itemStack.Count.ToString();
        amountText.gameObject.SetActive(itemStack.Count > 1);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItemToStack(BaseItem item)
    {
        if (this.itemStack.Count < this.item.MaxStackAmount)
        {
            this.itemStack.Push(item);
            return true;
        }
        else
        {
            //Add Case here, where new stack is created
        }

        return false; 
    }
    



    public void OnTap()
    {
        ActiveItemWindow.Instance.SetActiveItem(this.item, ISlot.slotType.InventorySlot);
        ActiveItemWindow.Instance.activeSlot = this;
    }



    public Transform GetTransform()
    {
        return this.transform;
    }

    /// <summary>
    /// This method removes the Item in the slot. If its a stack removes one, if its a single item, removes it completely.
    /// Does not update Item List of Inventory, since it is called automatically when an item is removed from inventory.
    /// If this method is not used from Inventory.Remove(), ItemIDs and Items List needs to be updated manually
    /// </summary>
    public void RemoveItem()
    {
        Inventory.Instance.Items.Remove(this.itemStack.Peek());
        this.itemStack.Pop();
        Inventory.Instance.ItemIDs.Remove(item.ItemID);
        if (this.itemStack.Count <= 0)
        {
            Image img = dragNDrop.GetComponent<Image>();
            img.enabled = false;
            this.item = null;
            ActiveItemWindow.Instance.HideActiveItemInfo();
        }

    }

    public void DropItem()
    {
        if (this.item == null) return;

        SpawnItem.Spawn(item,playerTransform.position);
        RemoveItem();
    }

    public void UseItem()
    {
        if (this.item == null) return;
        if (!item.IsUsable) return;

        switch (item.ItemType)
        {
            //check if item is weapon, if so only execute usage effect, if no weapon is equipped yet
            case BaseItem.ItemTypes.Weapon when weaponSlot.Item:
            case BaseItem.ItemTypes.Body when bodyArmorSlot.Item:
            case BaseItem.ItemTypes.Head when headArmorSlot.Item:
            case BaseItem.ItemTypes.Hand when handArmorSlot.Item:
            case BaseItem.ItemTypes.Feet when feetArmorSlot.Item:
                return; //add debug later to tell player, that he has already a weapon equipped
            default:
                item.ExecuteUsageEffect();
                break;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragNDrop dragNDrop = eventData.pointerDrag.GetComponent<DragNDrop>();
        ISlot originSlot = dragNDrop.MySlot;

        if (originSlot.Item == null) return; // If the origin slot has no item, do nothing

        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (this.item != null)
        {
            SwapItems(originSlot);
        }
        else
        {
            // If this inventory slot has no item
            SetItem(originSlot.Item);

            if (originSlot.ItemStack != null && originSlot.ItemStack.Count > 0)
            {
                for (int i = 0; i < originSlot.ItemStack.Count - 1; i++)
                {
                    AddItemToStack(originSlot.ItemStack.Peek());
                }
            }

            originSlot.RemoveItem();
            originSlot.Item = null;
            originSlot.ItemStack?.Clear();

            eventData.pointerDrag.GetComponent<Image>().enabled = false;
            originSlot.Item = null;
        }
    }


    public void SwapItems(ISlot otherSlot)
    {
        SlotHelper.SwapItems(this, otherSlot);
    }
}