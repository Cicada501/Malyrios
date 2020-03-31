using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Text amountText;

    public Item item;
    public int amount = 0;

    public Transform player;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        amountText.text = amount.ToString();
    }

    public void AddItem(Item newItem){
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        amountText.gameObject.SetActive(true);
    }

    public void ClearSlot(){
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        amountText.gameObject.SetActive(false);
    }

    public void OnRemoveButton(){
        SpawnItem.Spawn(item, player.position, 0.3f, -1.2f, 1.5f);
        amount--;
        if (amount == 0){
            Inventory.instance.Remove(item);
        }
    }
}
