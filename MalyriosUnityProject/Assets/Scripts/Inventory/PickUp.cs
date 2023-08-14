﻿using Malyrios.Items;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour, IInteractable
{
    TextMeshProUGUI interactableText = null;
    [SerializeField] private float pickUpRadius = 0.2f;
    [SerializeField] private BaseItem baseItem = null;
    
    private LayerMask whatCanPickMeUp;
    private SpriteRenderer spriteRenderer;
    private bool showText;

    public BaseItem BaseItem
    {
        get => this.baseItem;
        set => this.baseItem = value;
    }

 

    private void Start()
    {
        interactableText = ReferencesManager.Instance.interactableText;
        this.whatCanPickMeUp = LayerMask.GetMask("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (this.baseItem != null)
        {
            spriteRenderer.sprite = this.baseItem.Icon;
        }
    }

    private void Update()
    {
        bool collectable = Physics2D.OverlapCircle(transform.position, this.pickUpRadius, this.whatCanPickMeUp);
        if (collectable)
        {
            ShowPickUpDialog();
            this.showText = true;

        }
        else
        {
            if (this.showText)
            {
                this.interactableText.gameObject.SetActive(false);
                this.showText = false;
            }
        }
    }

    private void ShowPickUpDialog()
    {
        interactableText.text = $"Pick Up {this.baseItem.ItemName}";
        interactableText.gameObject.SetActive(true);
    }

    private void PickUpItem()
    {
        Inventory.Instance.AddItem(this.baseItem);
        gameObject.SetActive(false); //only set false instead of destroying to save and load what items where picked up
        interactableText.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.pickUpRadius);
    }

    public void Interact()
    {
        PickUpItem();
    }
}