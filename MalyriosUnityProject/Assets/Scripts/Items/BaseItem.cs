using System;
using Malyrios.Character;
using UnityEngine;

namespace Malyrios.Items
{
    public class BaseItem : ScriptableObject, IUsable
    {
        public enum ItemTypes
        {
            Weapon,
            Head,
            Body,
            Feet,
            Hand,
            Plant,
            Other,  
            Rune
        }

        [Header("Base Item Properties")] [SerializeField]
        protected int itemID = 0;

        [SerializeField] protected string itemName = null;
        [SerializeField] protected string description = null;
        [SerializeField] protected Sprite icon = null;
        [SerializeField] protected float dropChance = 0;
        [SerializeField] protected bool isStackable = false;
        [SerializeField] protected bool isUsable = false;
        [SerializeField] protected int maxStackAmount = 0;
        [SerializeField] protected ItemTypes itemType = 0;
        [SerializeField] protected GameObject itemPrefab = null;


        public int ItemID => this.itemID;
        public string ItemName => this.itemName;
        public string Description => description;
        public Sprite Icon => this.icon;
        public float DropChance => this.dropChance;
        public bool IsStackable => this.isStackable;
        public bool IsUsable => this.isUsable;
        public int MaxStackAmount => this.maxStackAmount;
        public ItemTypes ItemType => this.itemType;
        public GameObject ItemPrefab => this.itemPrefab;


        public virtual void ExecuteUsageEffect()
        {
            PlayerHealth health = ReferencesManager.Instance.player.GetComponent<PlayerHealth>();
            BaseAttributes baseAttributes = ReferencesManager.Instance.player.GetComponent<BaseAttributes>();
            if (this.itemName == "Red Flower")
            {
                health.Heal(30);
            }
            else if (this.itemName == "Schattenrose")
            {
                //Debug.Log("Hit me!");
                health.TakeDamage(50);
            }
            else if (this.itemName == "Schriftrolle des Lebens")
            {
                baseAttributes.MaxHealth += 100;
                baseAttributes.CurrentHealth += 100;
                StatsWindow.Instance.UpdateStatTexts();
            }
            else if (this.itemName == "Schriftrolle der Stärke")
            {
                baseAttributes.Strength += 20;
                StatsWindow.Instance.UpdateStatTexts();
            }
        }
    }
}