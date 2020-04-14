using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Malyrios.Items;
using Malyrios.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] Text amountText;

    private BaseItem item;
    private Stack<BaseItem> itemStack = new Stack<BaseItem>();

    public BaseItem Item => this.item;
    
    public void SetItem(BaseItem baseItem)
    {
        this.item = baseItem;
        Image img = this.transform.GetChild(3).GetComponent<Image>();
        img.enabled = true;
        img.sprite = baseItem.Icon;
        this.transform.GetChild(3).GetComponent<DragNDrop>().Item = baseItem;
        if (baseItem.IsStackable)
        {
            this.itemStack.Push(baseItem);
        }
    }

    public bool AddItemToStack(BaseItem item)
    {
        if (this.itemStack.Count < this.item.MaxStackAmount)
        {
            this.itemStack.Push(item);
            this.amountText.gameObject.SetActive(true);
            this.amountText.text = this.itemStack.Count.ToString();
            return true;
        }
        return false;
    }

    public void OnRightMouseButtonClick()
    {
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
    
    public void OnDrop(PointerEventData eventData)
    {
    }

    // [SerializeField] Image icon;
    // [SerializeField] Button removeButton;
    // public Item item;
    // public int amount = 0;
    // [SerializeField] Transform player;
    //
    // private Stack<BaseItem> itemStack = new Stack<BaseItem>();
    //
    // private BaseItem baseItem;
    // public BaseItem Item => this.baseItem;
    //
    // /// <summary>
    // /// Update is called every frame, if the MonoBehaviour is enabled.
    // /// </summary>
    // void Update()
    // {
    //     // amountText.text = amount.ToString();
    // }
    //
    // public void SetItem(BaseItem itm)
    // {
    //     this.baseItem = itm;
    //     Debug.Log(this.baseItem);
    //
    //     if (itm is BaseWeapon weapon)
    //     {
    //         Debug.Log("Schmul");
    //     }
    //
    //     // Set item icon.
    //     this.transform.GetChild(3).GetComponent<Image>().sprite = this.baseItem.Icon;
    //
    //     // if (item.IsStackable) this.itemStack.Push(item);
    // }
    //
    // public void AddItem(Item newItem)
    // {
    //     item = newItem;
    //
    //     icon.sprite = item.Icon;
    //     icon.enabled = true;
    //     removeButton.interactable = true;
    //     amountText.gameObject.SetActive(true);
    // }
    //
    // public void ClearSlot()
    // {
    //     item = null;
    //     icon.sprite = null;
    //     icon.enabled = false;
    //     removeButton.interactable = false;
    //     amountText.gameObject.SetActive(false);
    // }
    //
    // public void OnRemoveButton()
    // {
    //     print("Remove. Amount: " + amount);
    //     if (amount > 1) InventoryUI.d--;
    //     else UIManager.Instance.HideTooltip();
    //
    //     SpawnItem.Spawn(item, player.position, 0.3f, -1.2f, 1.5f);
    //     amount--;
    //
    //     Inventory.instance.Remove(item);
    // }
    //
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     if (this.baseItem != null)
    //     {
    //         UIManager.Instance.ShowTooltip(this.transform.position, this.baseItem);
    //     }
    // }
    //
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     if (this.baseItem != null)
    //     {
    //         UIManager.Instance.HideTooltip();
    //     }
    // }
}