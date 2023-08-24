using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmorData
{
    private int headArmorID;

    public ArmorData(EquipmentManager manager)
    {
        headArmorID = manager.equippedHeadArmor.ItemID;
        //...
    }
    
}
public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManager found!");
            return;
        }

        Instance = this;
    }
    

    #endregion
    public EquipmentSlot headArmorSlot;
    public EquipmentSlot bodyArmorSlot;
    public EquipmentSlot handArmorSlot;
    public EquipmentSlot feetArmorSlot;


    public BaseArmor equippedHeadArmor;
    public BaseArmor equippedBodyArmor;
    public BaseArmor equippedHandArmor;
    public BaseArmor equippedFeetArmor;

    
    private void Update()
    {
        equippedHeadArmor = headArmorSlot.Item as BaseArmor;
        //...
    }

    public ArmorData SaveArmor()
    {
        return new ArmorData(this);
    }

}
