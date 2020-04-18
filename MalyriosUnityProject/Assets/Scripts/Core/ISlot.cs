using System.Collections.Generic;
using Malyrios.Items;

namespace Malyrios.Core
{
    public interface  ISlot
    {
        BaseItem Item { get; set; }
        Stack<BaseItem> ItemStack { get; set; }

        void SetItem(BaseItem item);
        void RemoveItem();
    }
}