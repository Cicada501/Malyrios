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
    /// This method removes the Item in the slot. 
    /// Also Removes the item from the inventory item lists. This Method is Called if OnItemRemoved is invoked to update the slot correctly
    ///  </summary>
    public void RemoveItem()
    {
        Inventory.Instance.Items.Remove(Item);
        Inventory.Instance.ItemIDs.Remove(item.ItemID);
        Image img = dragNDrop.GetComponent<Image>();
        img.enabled = false;
        this.item = null;
        ActiveItemWindow.Instance.HideActiveItemInfo();
    }

    public void DropItem()
    {
        if (this.item == null) return;

        SpawnItem.Spawn(item, playerTransform.position);
        RemoveItem();
    }

    public void UseItem()
    {
        print($"Using {item.ItemName}");
        if (this.item == null) return;
        //if (!item.IsUsable) return;

        switch (item.ItemType)
        {
            //check if item is weapon/armor, if so only execute usage effect, if no weapon/armor is equipped yet
            case BaseItem.ItemTypes.Weapon when weaponSlot.Item:
                weaponSlot.SwapItems(this);
                break;
            case BaseItem.ItemTypes.Body when bodyArmorSlot.Item:
                bodyArmorSlot.SwapItems(this);
                break;
            case BaseItem.ItemTypes.Head when headArmorSlot.Item:
                headArmorSlot.SwapItems(this);
                break;
            case BaseItem.ItemTypes.Hand when handArmorSlot.Item:
                handArmorSlot.SwapItems(this);
                break;
            case BaseItem.ItemTypes.Feet when feetArmorSlot.Item:
                feetArmorSlot.SwapItems(this);
                break;
            default:
                if (item.ItemType == BaseItem.ItemTypes.Rune)
                {
                    var aps = FindObjectOfType<InventoryUI>().activePuzzleStation;
                    foreach (var slot in aps.slots)
                    {
                        //find first slot with no item and swap with the item of this slot
                        if (slot.Item == null)
                        {
                            slot.SetItem(this.item); 
                            this.RemoveItem();
                            aps.UpdateItemID(slot.transform.GetPuzzleSlotIndex(), slot.Item.ItemID);
                            break;
                        }
                    }
                }
                else
                {
                    item.ExecuteUsageEffect();
                }
                break;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragNDrop dragNDrop = eventData.pointerDrag.GetComponent<DragNDrop>();
        ISlot originSlot = dragNDrop.MySlot;

        if (originSlot.Item == null || ReferenceEquals(originSlot, this))
            return; // If the origin slot has no item, do nothing

        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (this.item != null)
        {
            SwapItems(originSlot);
        }
        else
        {
            // If this inventory slot has no item
            SetItem(originSlot.Item);


            originSlot.RemoveItem();
            originSlot.Item = null;

            eventData.pointerDrag.GetComponent<Image>().enabled = false;
            originSlot.Item = null;
        }
    }


    public void SwapItems(ISlot otherSlot)
    {
        SlotHelper.SwapItems(this, otherSlot);
    }
}