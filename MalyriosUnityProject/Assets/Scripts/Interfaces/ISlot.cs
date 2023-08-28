using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;

namespace Malyrios.Core
{
    public interface ISlot
    {
        public enum slotType
        {
            InventorySlot,
            EquipmentSlot,
            PuzzleSlot
        }

        BaseItem Item { get; set; }
        Stack<BaseItem> ItemStack { get; set; }

        void SetItem(BaseItem item);
        void RemoveItem();
        Transform GetTransform();

        void SwapItems(ISlot otherSlot);
        void UseItem();
        void DropItem();
    }
}

namespace Malyrios.Core
{
    public static class SlotHelper
    {
        public static void SwapItems(ISlot slot1, ISlot slot2)
        {
            BaseItem tempItem1 = slot1.Item;
            Stack<BaseItem> tempStack1 = new Stack<BaseItem>(slot1.ItemStack.ToArray());

            slot1.SetItem(slot2.Item);
            slot1.ItemStack = new Stack<BaseItem>(slot2.ItemStack.ToArray());
            
            slot2.SetItem(tempItem1);
            slot2.ItemStack = tempStack1;
            
            if (slot1 is InventorySlot)
            {
                
                Inventory.Instance.Items.Remove(slot2.Item);
                Inventory.Instance.ItemIDs.Remove(slot2.Item.ItemID);
                
                Inventory.Instance.Items.Add(slot1.Item);
                Inventory.Instance.ItemIDs.Add(slot1.Item.ItemID);
            }
            if (slot2 is InventorySlot)
            {
                Inventory.Instance.Items.Remove(slot1.Item);
                Inventory.Instance.ItemIDs.Remove(slot1.Item.ItemID);
                
                Inventory.Instance.Items.Add(slot2.Item);
                Inventory.Instance.ItemIDs.Add(slot2.Item.ItemID);
            }
        }
    }
}