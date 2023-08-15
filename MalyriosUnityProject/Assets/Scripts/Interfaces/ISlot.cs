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
    }
}