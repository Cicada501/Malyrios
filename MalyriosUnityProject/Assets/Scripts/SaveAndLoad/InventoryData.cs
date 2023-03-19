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

            }
            else
            {
                itemIDs = Array.Empty<int>();
                //Debug.Log("Inventory is empty");
            }

            equippedWeaponID = PlayerAttack.EquippedWeaponID;
            //Debug.Log("Saved Weapon with ID: "+equippedWeaponID);
        }
    }
}