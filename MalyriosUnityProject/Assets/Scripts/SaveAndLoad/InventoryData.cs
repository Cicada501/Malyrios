using System;
using UnityEngine;

namespace SaveAndLoad
{
    [System.Serializable]
    public class InventoryData
    {
        public int[] itemIDs;
        public int equippedWeaponID;

        public InventoryData(Inventory inventory)
        {
            if (!inventory.isEmpty)
            {
                itemIDs = inventory.ItemIDs.ToArray();
                Debug.Log("itemIDs length: " + itemIDs.Length);
            }
            else
            {
                itemIDs = Array.Empty<int>();
                Debug.LogError("Inventory is empty");
            }

            equippedWeaponID = PlayerAttack.EquippedWeaponID;
        }
    }
}