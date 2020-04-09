using Malyrios.Items;
using Malyrios.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image icon;
    [SerializeField] Button removeButton;
    [SerializeField] Text amountText;
    public Item item;
    public int amount = 0;
    [SerializeField] Transform player;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        amountText.text = amount.ToString();
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
        amountText.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        amountText.gameObject.SetActive(false);
    }

    public void OnRemoveButton()
    {
        print("Remove. Amount: " + amount);
        if (amount > 1) InventoryUI.d--;


        SpawnItem.Spawn(item, player.position, 0.3f, -1.2f, 1.5f);
        amount--;

        Inventory.instance.Remove(item);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            UIManager.Instance.ShowTooltip(this.transform.position, this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.item != null)
        {
            UIManager.Instance.HideTooltip();
        }
    }
}
