using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    public Transform player;
    public GameObject PhysicItem;
    
    Item item;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        print("PhysicItem: "+PhysicItem);
    }

    public void AddItem(Item newItem){
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot (){
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton(){
        PhysicItem.GetComponent<PickUp>().item = item;
        Instantiate(PhysicItem, player.position, Quaternion.identity);
        Inventory.instance.Remove(item);
    }
}
