using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.LogError("Inventory is empty");
        }

        //If Weaponslot not empty, equippedWeaponID = ..
        //var weaponslot = GameObject.Find("WeaponSlot").GetComponent<EquipmentSlot>();
        equippedWeaponID = PlayerAttack.EquippedWeaponID;
    }
}