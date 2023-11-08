using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopWindow : MonoBehaviour
{
    
    private void Awake()
    {
        if(Instance != null)
        {
            print($"instance is not on {gameObject.name}");
            Debug.LogWarning("More than one instance of ShopWindow found!");
            return;
        }

        Instance = this;
        //print($"instance is on {gameObject.name}");
    }

    public static ShopWindow Instance;
    [HideInInspector]
    public Shop activeShop;
    [HideInInspector]
    public GameObject shopWindow;
    private GameObject shopItemPrefab;

    private GameObject itemPrefabParent;
    private readonly List<GameObject> instantiatedShopItemPrefabs = new();

    private void Start()
    {
        shopWindow = ReferencesManager.Instance.shopWindow;
        shopItemPrefab = ReferencesManager.Instance.shopItemPrefab;
        itemPrefabParent = ReferencesManager.Instance.shopItemPrefabParent;
    }

    void ListItems()
    {
        foreach (var item in activeShop.availableItems)
        {
            var newItem = Instantiate(shopItemPrefab, itemPrefabParent.transform);
            instantiatedShopItemPrefabs.Add(newItem);
            var itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            var itemTitle = newItem.transform.Find("Description/ItemTitle").GetComponent<TextMeshProUGUI>();
            var itemPrice = newItem.transform.Find("Description/ItemPrice").GetComponent<TextMeshProUGUI>();
            var itemButton = newItem.GetComponent<Button>(); // Hier bekommt man den Button

            itemIcon.sprite = item.Icon;
            itemTitle.text = item.ItemName;
            itemPrice.text = item.ItemPrice.ToString();

            // Hier bindet man die Methode an den Button
            itemButton.onClick.AddListener(() => SetActiveItem(item));
        }
    }
    
    public void SetActiveItem(BaseItem item)
    {
        ActiveItemWindow.Instance.SetActiveItem(item, ISlot.slotType.ShopSlot); // Du musst den korrekten Slot-Typ hier setzen, falls "ShopSlot" nicht existiert
    }


    public void Buy(BaseItem item)
    {
        if (PlayerMoney.Instance.CurrentMoney < item.ItemPrice) return;
        Inventory.Instance.AddItem(item);
        PlayerMoney.Instance.RemoveMoney(item.ItemPrice);
        SoundHolder.Instance.buyItem.Play();
    }

    public void ShowShopWindow()
    {
        ToggleShopWindow();
        ListItems();
    }

    public void ToggleShopWindow()
    {
        shopWindow.SetActive(!shopWindow.activeSelf);
        if (shopWindow.activeSelf)
        {
            var soundIndex = Random.Range(0, SoundHolder.Instance.openButton.Length);
            SoundHolder.Instance.openButton[soundIndex].Play();
            ReferencesManager.Instance.canvasUI.GetComponent<InventoryUI>().ChangeInventoryOpened(false);

        }
        else
        {
            SoundHolder.Instance.closeButton.Play();
            
            var itemsToDestroy = new List<GameObject>(instantiatedShopItemPrefabs);
            instantiatedShopItemPrefabs.Clear();
            foreach (var item in itemsToDestroy)
            {
                Destroy(item);
            }
            if(InventoryUI.inventoryOpen)ReferencesManager.Instance.canvasUI.GetComponent<InventoryUI>().ChangeInventoryOpened();
            activeShop = null;

        }
    }
}
