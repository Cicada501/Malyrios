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

    private GridLayoutGroup test;

    private GameObject t;
    
    private void Start()
    {
        t = transform.GetChild(0).gameObject;
        BaseWeapon wp = weapon.GetComponent<BaseWeapon>();
        test = transform.parent.GetComponent<GridLayoutGroup>();

        t.GetComponent<Image>().sprite = wp.Icon;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log(gameObject.name + eventData.pointerDrag.transform.parent.name);

            if (gameObject.name != eventData.pointerDrag.transform.parent.name)
            {
                this.t.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                transform.GetChild(0).gameObject.SetActive(true);
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
                eventData.pointerDrag.SetActive(false);
            }

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                this.test.GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
