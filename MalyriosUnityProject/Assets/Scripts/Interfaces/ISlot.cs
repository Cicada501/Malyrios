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

            slot1.RemoveItem();
            slot1.SetItem(slot2.Item);

            slot2.RemoveItem();
            slot2.SetItem(tempItem1);

        }
    }
}