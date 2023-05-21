using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;

using TMPro;
using UnityEngine;

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


    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    private void Start()
    {
        Inventory.Instance.OnActiveItemSet += ShowActiveItemInfo;
    }
    private void ShowActiveItemInfo(BaseItem item)
    {
        this.gameObject.SetActive(true);

        nameText.text = item.ItemName;
        descriptionText.text = item.Description;
        print("SHOWING ACTIVE ITEM INFO");
    }

    public void HideActiveItemInfo()
    {
        this.gameObject.SetActive(false);
    }
}

