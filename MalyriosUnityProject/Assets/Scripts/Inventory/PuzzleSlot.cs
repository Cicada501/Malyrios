using System;
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
    
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        child.GetComponent<DragNDrop>().MySlot = this;
    }

    private void Update()
    {
        print(Item != null
            ? $"Slot{transform.GetPuzzleSlotIndex()} has item: {Item.ItemName}"
            : $"Slot{transform.GetPuzzleSlotIndex()} has no item");
    }

    public void SetItem(BaseItem item)
    {
        slotImage.sprite = item.Icon;
        slotImage.enabled = true;
        Item = item;
        
    }


    public void RemoveItem()
    {
        slotImage.enabled = false;
        Item = null;
        int slotIndex = transform.GetPuzzleSlotIndex();
        puzzleStation.UpdateItemID(slotIndex, 0);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }


    public void OnDrop(PointerEventData eventData)
    {
        //if this slot has a item already, do nothing
        if (eventData.pointerDrag == null) return;

        // Get the slot of the dragged item. //is this the slot, the drag starts from?
        ISlot slot = eventData.pointerDrag.GetComponent<DragNDrop>().MySlot;

        // If the dragged item is not the same item type, you can't drag it on this item slot, so do nothing.
        if (slot.Item.ItemType != this.itemType) return;


        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Wenn das Slot-Item nicht null ist (es gibt bereits ein Item im Slot)
        if (Item != null)
        {
            // Tausche die Items
            BaseItem tempItem = Item;
            int tempItemID = Item.ItemID;

            // Setze das Slot-Item
            SetItem(slot.Item);

            // Setze das Item in der Ursprungsslot
            slot.SetItem(tempItem);

            // Aktualisiere die itemIDsArray in der PuzzleStation
            int slotIndex = transform.GetPuzzleSlotIndex();
            puzzleStation.UpdateItemID(slotIndex, Item.ItemID);
            int originalSlotIndex = slot.GetTransform().GetPuzzleSlotIndex();
            puzzleStation.UpdateItemID(originalSlotIndex, tempItemID);
        }
        else
        {
            // Verarbeite den Fall, wenn kein Item im Slot ist (dein aktueller Code)
            this.child.GetComponent<Image>().sprite =
                eventData.pointerDrag.GetComponent<Image>().sprite;
            Item = slot.Item;
            slot.RemoveItem();
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            eventData.pointerDrag.GetComponent<Image>().enabled = false;
            int slotIndex = transform.GetPuzzleSlotIndex();
            puzzleStation.UpdateItemID(slotIndex, Item.ItemID);
        }
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





//Class for extension method to add new method to the transform class
public static class TransformExtensions
{
    /// <summary>
    /// Gets the index of a child object that has the component PuzzleSlot, ignoring other siblings.
    /// </summary>
    /// <param name="transform">The transform of the child object for which the custom sibling index is needed.</param>
    /// <returns>The custom sibling index of the object, or -1 if the PuzzleSlot component is not found.</returns>

    public static int GetPuzzleSlotIndex(this Transform transform)
    {
        int index = -1;
        
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform sibling = transform.parent.GetChild(i);
            if (sibling.GetComponent<PuzzleSlot>() != null)
            {
                index++;
                if (sibling == transform)
                {
                    return index;
                }
            }
        }
        
        return -1;
    }
}