using System;
using Malyrios.Character;
using UnityEngine;

namespace Malyrios.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/NewItem")]
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
        [SerializeField] protected bool isUsable = false;
        [SerializeField] protected ItemTypes itemType = 0;
        [SerializeField] protected GameObject itemPrefab = null;
        [SerializeField] protected int itemPrice = 0 ;


        public int ItemID => this.itemID;
        public string ItemName => this.itemName;
        public string Description => description;
        public Sprite Icon => this.icon;
        public float DropChance => this.dropChance;
        public bool IsUsable => this.isUsable;
        public ItemTypes ItemType => this.itemType;
        public GameObject ItemPrefab => this.itemPrefab;
        public int ItemPrice => this.itemPrice;


        public virtual void ExecuteUsageEffect()
        {
            ActiveItemWindow.Instance.activeSlot.RemoveItem();
            PlayerHealth health = ReferencesManager.Instance.player.GetComponent<PlayerHealth>();
            BaseAttributes baseAttributes = ReferencesManager.Instance.player.GetComponent<BaseAttributes>();
            if (this.itemName == "Red Flower")
            {
                health.Heal(80+(int)baseAttributes.Balance);
            }
            else if (this.itemName == "Schattenrose")
            {
                //Debug.Log("Hit me!");
                health.TakeDamage(50);
            }
            else if (this.itemName == "Schriftrolle des Lebens")
            {
                SaveScrolls.Instance.scrollData.HealthScrollsUsed++;
                baseAttributes.MaxHealth += 100;
                baseAttributes.CurrentHealth += 100;
                StatsWindow.Instance.UpdateStatTexts();
            }
            else if (this.itemName == "Schriftrolle der Stärke")
            {
                SaveScrolls.Instance.scrollData.StrengthScrollsUsed++;
                baseAttributes.Strength += 10;
                StatsWindow.Instance.UpdateStatTexts();
            }
            else if (this.itemName == "Schriftrolle der Intelligenz")
            {
                SaveScrolls.Instance.scrollData.IntScrollsUsed++;
                baseAttributes.Energy += 10;
                StatsWindow.Instance.UpdateStatTexts();
            }
            else if (this.itemName == "Schriftrolle der Agilität")
            {
                baseAttributes.Haste += 10;
                StatsWindow.Instance.UpdateStatTexts();
            }
            else if (this.itemName == "Schriftrolle der Ausgeglichenheit")
            {
                
                baseAttributes.Balance += 10;
                StatsWindow.Instance.UpdateStatTexts();
            }
        }
    }
}