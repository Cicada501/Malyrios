using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ActiveItemWindow : MonoBehaviour
{
    public static ActiveItemWindow Instance;
    public BaseItem activeItem = null;
    public ISlot activeSlot;
    [SerializeField] private Button useButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private GameObject sellingInfo;
    [SerializeField] private TextMeshProUGUI sellPriceText;
    private ISlot.slotType activeSlotType;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of ActiveItemWindow found!");
            return;
        }

        Instance = this;
    }

    [SerializeField] private GameObject window;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image itemImage;


    void ChangeActiveItem(BaseItem item)
    {
        if (activeItem == null)
        {
            ShowActiveItemInfo(item);
        }
        else if (item.name == activeItem.name)
        {
            HideActiveItemInfo();
        }
        else
        {
            ShowActiveItemInfo(item);
        }
    }

    private void ShowActiveItemInfo(BaseItem item)
    {
        window.SetActive(true);

        if (item is BaseWeapon weapon)
        {
            nameText.text = weapon.ItemName;
            descriptionText.text = weapon.GetDescription();
            itemImage.sprite = weapon.Icon;
        }
        else if (item is BaseArmor armor)
        {
            nameText.text = armor.ItemName;
            descriptionText.text = armor.GetDescription();
            itemImage.sprite = armor.Icon;
        }
        else
        {
            nameText.text = item.ItemName;
            descriptionText.text = item.Description;
            itemImage.sprite = item.Icon;
        }
    }

    public void SetActiveItem(BaseItem item, ISlot.slotType slotType)
    {
        ChangeActiveItem(item);

        //if item is not selected
        if (activeItem == null || activeItem != item)
        {
            activeItem = item;

            //if the ActiveItemWindow appears from Selecting an Item in the Shop
            switch (slotType)
            {
                case ISlot.slotType.ShopSlot:
                    activeSlotType = ISlot.slotType.ShopSlot;
                    useButton.gameObject.SetActive(true);
                    useButton.GetComponentInChildren<TextMeshProUGUI>().text = "kaufen";
                    removeButton.gameObject.SetActive(false);
                    sellPriceText.text = ((int)(item.ItemPrice / 1.5f)).ToString();
                    sellingInfo.SetActive(true);
                    return;

                case ISlot.slotType.EquipmentSlot:
                    removeButton.gameObject.SetActive(!ShopWindow.Instance.activeShop);
                    sellingInfo.SetActive(false);
                    useButton.gameObject.SetActive(true);
                    useButton.GetComponentInChildren<TextMeshProUGUI>().text = "anlegen";
                    break;

                default:
                    if (ShopWindow.Instance.activeShop)
                    {
                        useButton.gameObject.SetActive(true);
                        useButton.GetComponentInChildren<TextMeshProUGUI>().text = "verkaufen";
                        
                        sellPriceText.text = ((int)(item.ItemPrice / 1.5f)).ToString();
                        sellingInfo.SetActive(true);
                        removeButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        sellingInfo.SetActive(false);
                        removeButton.gameObject.SetActive(true);
                        if (item.IsUsable)
                        {
                            useButton.gameObject.SetActive(true);
                            useButton.GetComponentInChildren<TextMeshProUGUI>().text = "nutzen";
                        }
                    }
                    
                    break;
            }
        }
        else
        {
            activeItem = null;
        }
    }

    public void HideActiveItemInfo()
    {
        window.SetActive(false);
    }

    public void UseButtonPressed()
    {
        if (activeSlotType == ISlot.slotType.ShopSlot)
        {
            ShopWindow.Instance.Buy(activeItem);
        }
        else
        {
            if (ShopWindow.Instance.activeShop)
            {
                Inventory.Instance.Remove(activeItem);
                PlayerMoney.Instance.AddMoney((int)(activeItem.ItemPrice / 1.5f));
            }
            else
            {
                activeSlot.UseItem();
            }
        }

        HideActiveItemInfo();
    }

    public void RemoveButtonPressed()
    {
        activeSlot.DropItem();
        HideActiveItemInfo();
    }
}