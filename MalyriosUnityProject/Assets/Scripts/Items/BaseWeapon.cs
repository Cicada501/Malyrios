using UnityEngine;

namespace Malyrios.Items
{
    [CreateAssetMenu]
    public class BaseWeapon : BaseItem, IItemDescriber
    {
        [Header("Weapon Properties")] 
        [SerializeField] private int minDamage;
        [SerializeField] private int maxDamage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private int strength;
        [SerializeField] private float critChance;
        [SerializeField] private float critDamage;

        public int MinDamage => this.minDamage;
        public int MaxDamage => this.maxDamage;
        public float AttackSpeed => this.attackSpeed;
        public int Strength => this.strength;
        public float CritChance => this.critChance;
        public float CritDamage => this.critDamage;
        
        public BaseWeapon InitItem()
        {
            base.itemID = 1;
            base.itemName = "Iron Sword";
            base.icon = GetSprite(SpriteTypes.IronSword);
            base.itemPrefab = GetItemPrefab(SpriteTypes.IronSword);
            base.itemType = ItemTypes.Weapon;
            this.minDamage = 10;
            this.maxDamage = 20;
            this.attackSpeed = 1.5f;
            this.strength = 2;
            this.critChance = 50f;
            this.critDamage = 5.0f;

            return this;
        }

        public string GetDescription()
        {
            return $"<color=green>{this.itemName}</color>\n\n" +
                   $"Damage: {this.minDamage}-{this.maxDamage}\n" +
                   $"Attack speed: {this.attackSpeed}\n" +
                   $"DPS: {(((float)this.minDamage + (float)this.MaxDamage) / 2) / this.attackSpeed:0.00}\n" +
                   $"Strength: {this.strength}\n" +
                   $"Crit chance: {this.critChance}%\n" +
                   $"Crit damage: {this.critDamage}%";
        }
    }
}

