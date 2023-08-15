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



    public void OnDrop(PointerEventData eventData)
    {
        //if this slot has a item already, do nothing
        if (eventData.pointerDrag == null || Item!=null) return;

        // Get the slot of the dragged item. //is this the slot, the drag starts from?
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
        int slotIndex = transform.GetPuzzleSlotIndex();
        puzzleStation.UpdateItemID(slotIndex, Item.ItemID);
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