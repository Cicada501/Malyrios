using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;

namespace Malyrios.Core
{
    public interface  ISlot
    {
        BaseItem Item { get; set; }
        Stack<BaseItem> ItemStack { get; set; }

        void SetItem(BaseItem item);
        void RemoveItem();
        Transform GetTransform();
        
        void SwapItems(ISlot otherSlot);
    }
}

namespace Malyrios.Core
{
    public static class SlotHelper
    {
        public static void SwapItems(ISlot slot1, ISlot slot2)
        {
            // Speichere temporär die Items aus den Slots
            BaseItem tempItem1 = slot1.Item;
            Stack<BaseItem> tempStack1 = slot1.ItemStack;
            
            // Setze die Items im ersten Slot auf die Werte des zweiten Slots
            slot1.SetItem(slot2.Item);
            slot1.ItemStack = slot2.ItemStack;

            // Setze die Items im zweiten Slot auf die temporär gespeicherten Werte des ersten Slots
            slot2.SetItem(tempItem1);
            slot2.ItemStack = tempStack1;
            
        }
    }
}
