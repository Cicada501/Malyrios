using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject weapon;

    private GridLayoutGroup gridLayoutGroup;

    private GameObject child;
    
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        BaseWeapon wp = weapon.GetComponent<BaseWeapon>();
        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
        this.child.GetComponent<Image>().sprite = wp.Icon;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (gameObject.name != eventData.pointerDrag.transform.parent.name)
            {
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                this.child.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
                eventData.pointerDrag.GetComponent<Image>().enabled = false;
            }

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                this.gridLayoutGroup.GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
