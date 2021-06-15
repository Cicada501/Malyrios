using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int[] itemIDs;
    
    public InventoryData(Inventory inventory){
        itemIDs = inventory.ItemIDs.ToArray();
    }

}
