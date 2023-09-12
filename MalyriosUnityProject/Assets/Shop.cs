using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Shop : MonoBehaviour
{

    public GameObject shopWindow;

    public List<BaseItem> availableItems;

    public BaseItem activeItem;

    public GameObject shopItemPrefab;

    public GameObject itemPrefabParent;

    private void Start()
    {
        ListItems();
    }

    void ListItems()
    {
        foreach (var item in availableItems)
        {
            var newItem = Instantiate(shopItemPrefab, itemPrefabParent.transform);
            var itemIcon = newItem.transform.Find("ItemIcon").GetComponent<Image>();
            var itemTitle = newItem.transform.Find("Description/ItemTitle").GetComponent<TextMeshProUGUI>();
            var itemPrice = newItem.transform.Find("Description/ItemPrice").GetComponent<TextMeshProUGUI>();

            itemIcon.sprite = item.Icon;
            itemTitle.text = item.ItemName;
            print($"itemprice: {item.ItemPrice}");
            itemPrice.text = item.ItemPrice.ToString();
        }
    }


    void Buy(BaseItem item)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
