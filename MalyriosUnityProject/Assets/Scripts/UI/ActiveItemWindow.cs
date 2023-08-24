using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActiveItemWindow : MonoBehaviour
{
    public static ActiveItemWindow Instance;

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

    private void Start()
    {
        Inventory.Instance.OnActiveItemSet += ChangeActiveItem;
    }

    void ChangeActiveItem(BaseItem item)
    {
        if (Inventory.Instance.activeItem == null)
        {
            ShowActiveItemInfo(item);
        }else if (item.name == Inventory.Instance.activeItem.name)
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

    public void HideActiveItemInfo()
    {
        window.SetActive(false);
    }
    
    public void UseButtonPressed()
    {
        Inventory.Instance.activeSlot.UseItem();
    }

    public void RemoveButtonPressed()
    {
        Inventory.Instance.activeSlot.DropItem();
    }
    
}

