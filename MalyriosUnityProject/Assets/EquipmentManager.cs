using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmorData
{
    public int headArmorID;
    public int bodyArmorID;
    public int handArmorID;
    public int feetArmorID;


    public ArmorData(EquipmentManager manager)
    {
        headArmorID = manager.equippedHeadArmor.ItemID;
        bodyArmorID = manager.equippedBodyArmor.ItemID;
        handArmorID = manager.equippedHandArmor.ItemID;
        feetArmorID = manager.equippedFeetArmor.ItemID;
        
    }
    
    public ArmorData(int headID, int bodyID, int handID, int feetID)
    {
        headArmorID = headID;
        bodyArmorID = bodyID;
        handArmorID = handID;
        feetArmorID = feetID;
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
        equippedBodyArmor = bodyArmorSlot.Item as BaseArmor;
        equippedHandArmor = handArmorSlot.Item as BaseArmor;
        equippedFeetArmor = feetArmorSlot.Item as BaseArmor;
        
    }

    public ArmorData SaveArmor()
    {
        return new ArmorData(this);
    }

}
