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

    private void Awake()
    {
        if(Instance != null)
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


    void ChangeActiveItem(BaseItem item, ISlot.slotType slotType)
    {
        if (activeItem == null)
        {
            ShowActiveItemInfo(item);
        }else if (item.name == activeItem.name)
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
        }else if(item is BaseArmor armor)
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
        ChangeActiveItem(item, slotType);
        
        if (activeItem == null || activeItem != item)
        {
            activeItem = item;
            if (item.IsUsable)
            {
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
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

        activeSlot.UseItem();
    }

    public void RemoveButtonPressed()
    {

        activeSlot.DropItem();
    }
    
}

