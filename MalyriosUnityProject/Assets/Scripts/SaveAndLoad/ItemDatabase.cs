using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malyrios.Items;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] BaseItem[] Items = null;
    static BaseItem[] ItemsStatic = null;
    [SerializeField] BaseWeapon[] Weapons = null;
    static BaseWeapon[] WeaponsStatic = null;

    private void Awake()
    {
        WeaponsStatic = Weapons;
        ItemsStatic = Items;
    }

    public static BaseItem GetItem(int id)
    {
        //empty slots
        if (id == 0) return null;

        foreach (var item in ItemsStatic)
        {
            if (item.ItemID == id)
            {
                return item;
            }
        }

        Debug.LogError("No item with iD " + id+ " in the ItemDatabase (normal for Weapons)");
        return null;
    }

    public static BaseWeapon GetWeapon(int id)
    {
        //empty slots
        if (id == 0) return null;

        foreach (var weapon in WeaponsStatic)
        {
            if (weapon.ItemID == id)
            {
                return weapon;
            }
        }

        Debug.LogError("No Weapon with iD " + id+ " in the WeaponDatabase (now it gets critical)");
        return null;
    }
}