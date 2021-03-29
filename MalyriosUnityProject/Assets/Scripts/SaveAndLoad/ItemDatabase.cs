using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malyrios.Items;

public class ItemDatabase : MonoBehaviour
{
[SerializeField] BaseItem[] Items;
void Start(){
    
}
public BaseItem GetItem(int id){
    //empty slots
    if(id==0)return null;

    foreach (var item in Items)
    {
        if (item.ItemID==id)
        {
            return item;
        }     
    }
    Debug.LogError("No item with iD "+id);
    return null;
    
}
}
