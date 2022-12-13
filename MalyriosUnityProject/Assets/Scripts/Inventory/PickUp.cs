﻿using Malyrios.Items;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshProUGUI tmpText = null;
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
            
            //if (Player.interactInput)
            //{
            //    PickUpItem();
            //    //Dont pick up multiple items at once
            //    Player.interactInput = false;
            //}
        }
        else
        {
            if (this.showText)
            {
                this.tmpText.gameObject.SetActive(false);
                this.showText = false;
            }
        }
    }

    private void ShowPickUpDialog()
    {
        tmpText.text = $"Pick Up {this.baseItem.ItemName}";
        tmpText.gameObject.SetActive(true);
    }

    private void PickUpItem()
    {
        Inventory.Instance.AddItem(this.baseItem);
        Destroy(gameObject);
        //ButtonScript.InteractInput = false; //if Interactable text gets destroyed the RealeaseInteractButton() event is not triggerd anymore
        tmpText.gameObject.SetActive(false);
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