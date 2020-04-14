using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    public static event Action OnItemSlotChanged;

    public BaseItem Item;
    
    private GridLayoutGroup gridLayoutGroup;

    private GameObject child;
    
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();

        if (Item != null)
        {
            // child.GetComponent<Image>().sprite = weapon.GetComponent<BaseWeapon>().Icon;
            // child.GetComponent<DragNDrop>().Weapon = weapon.GetComponent<BaseWeapon>();
            child.GetComponent<Image>().sprite = Item.Icon;
            child.GetComponent<DragNDrop>().Item = Item;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (gameObject.name != eventData.pointerDrag.transform.parent.name)
            {
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                this.child.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                this.child.GetComponent<DragNDrop>().Item = eventData.pointerDrag.GetComponent<DragNDrop>().Item;

                transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true; // Schmeißt Fehler, da OnDrop vor OnEndDrag ausgeführt wird.
                
                eventData.pointerDrag.GetComponent<Image>().enabled = false;

                Debug.Log($"AttackSpeed: {eventData.pointerDrag.GetComponent<DragNDrop>().Item.ItemName}");
            }

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                this.gridLayoutGroup.GetComponent<RectTransform>().anchoredPosition;
        }
    }
}