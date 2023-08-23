using System;
using UnityEngine;

namespace SaveAndLoad
{
    [System.Serializable]
    public class InventoryData
    {
        public int[] itemIDs;
        public InventoryData(Inventory inventory)
        {
            if (!inventory.isEmpty)
            {
                itemIDs = inventory.ItemIDs.ToArray();

            }
            else
            {
                itemIDs = Array.Empty<int>();
            }
        }
    }
}