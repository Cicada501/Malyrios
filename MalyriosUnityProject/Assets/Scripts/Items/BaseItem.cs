﻿using System;
using UnityEngine;

namespace Malyrios.Items
{
    public class BaseItem : ScriptableObject
    {
        public enum SpriteTypes
        {
            RedFlower,
            IronSword
        }

        public enum ItemTypes
        {
            Weapon,
            Head,
            Body,
            Feet,
            Hand,
            Plant,
            None
        }

        [Header("Base Item Properties")] 
        [SerializeField] protected string itemName = null;
        [SerializeField] protected string description = null;
        [SerializeField] protected Sprite icon = null;
        [SerializeField] protected int sellPrice = 0;
        [SerializeField] protected int purchasePrice = 0;
        [SerializeField] protected float dropChance = 0;
        [SerializeField] protected bool isStackable = false;
        [SerializeField] protected int maxStackAmount = 0;
        [SerializeField] protected SpriteTypes spriteType = 0;
        [SerializeField] protected ItemTypes itemType = 0;
        [SerializeField] protected GameObject itemPrefab = null;

        public string ItemName => this.itemName;
        public string Description => description;
        public Sprite Icon => this.icon;
        public int SellPrice => this.sellPrice;
        public int PurchasePrice => this.purchasePrice;
        public float DropChance => this.dropChance;
        public bool IsStackable => this.isStackable;
        public int MaxStackAmount => this.maxStackAmount;
        public SpriteTypes SpriteType => this.spriteType;
        public ItemTypes ItemType => this.itemType;
        public GameObject ItemPrefab => this.itemPrefab;

        public BaseItem InitItem(string name, string description, SpriteTypes spriteType, bool isStackable = false, int maxStackAmount = 0)
        {
            this.itemName = name;
            this.description = description;
            this.icon = GetSprite(spriteType);
            this.isStackable = isStackable;
            this.maxStackAmount = maxStackAmount;
            this.itemType = ItemTypes.None;

            return this;
        }

        protected Sprite GetSprite(SpriteTypes spriteType)
        {
            switch (spriteType)
            {
                case SpriteTypes.RedFlower:
                    return ItemAssets.Instance.Flower;
                case SpriteTypes.IronSword:
                    return ItemAssets.Instance.IronSword;
                default:
                    return null;
            }
        }

        protected GameObject GetItemPrefab(SpriteTypes spriteType)
        {
            switch (spriteType)
            {
                case SpriteTypes.RedFlower:
                    return null;
                case SpriteTypes.IronSword:
                    return ItemAssets.Instance.IronSwordPrefab;
                default:
                    return null;
            }
        }
    }
}