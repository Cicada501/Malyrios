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

    dragNDrop.MySlot = this; // Optional, wenn die Zuweisung bereits im Start erfolgte

    this.itemStack.Push(baseItem);
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
            this.amountText.gameObject.SetActive(true);
            this.amountText.text = this.itemStack.Count.ToString();
            return true;
        }

        return false;
    }


    public void OnTap()
    {
        Inventory.Instance.SetActiveItem(this.item);
        Inventory.Instance.activeSlot = this;
    }

    /// <summary>
    /// This method removes the Item in the slot completely
    /// </summary>
    public void RemoveItem()
    {
        this.item = null;
        this.amountText.gameObject.SetActive(false);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void RemoveSingleItem()
    {
        if (this.item is BaseWeapon)
        {
            RemoveItem(); //if i try to remove a weapon (by equipping it) in the same way as the items, it throws an error saying the stack is empty
        }
        else
        {
            Inventory.Instance.Items.Remove(this.itemStack.Peek());
            this.itemStack.Pop();
            this.amountText.text = itemStack.Count.ToString();
            Inventory.Instance.ItemIDs.Remove(item.ItemID);
            if (this.itemStack.Count <= 0)
            {
                this.transform.GetChild(2).GetComponent<Image>().enabled = false;
                RemoveItem();
                ActiveItemWindow.Instance.HideActiveItemInfo();
            }
        }
    }

    public void DropItem()
    {
        if (this.item == null) return;

        SpawnItem.Spawn(item, this.playerTransform.position, 0.3f, -1.2f, 1.5f);
        RemoveSingleItem();
        
    }

    public void UseItem()
    {
        if (this.item == null) return;
        if (item.IsUsable)
        {
            item.ExecuteUsageEffect();
            RemoveSingleItem();
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
            // If this inventory slot has no item, use your existing logic
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