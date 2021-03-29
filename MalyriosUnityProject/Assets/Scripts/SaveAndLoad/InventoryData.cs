using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int[] itemIDs;
    
    public InventoryData(Inventory inventory){
        itemIDs = inventory.ItemIDs.ToArray();
        /* for (int i = 0; i < inventory.Items.Count; i++)
        {
            itemIDs[i] = Inventory.Instance.Items[i].ItemID;
            Debug.Log("Added ID "+inventory.Items[i].ItemID+" to itemIDs and i is:"+i);
        } */

    }

}
