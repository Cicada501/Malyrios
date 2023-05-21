using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Items
{
    [CreateAssetMenu]
    public class BaseWeapon : BaseItem, IItemDescriber, IUsable
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

        public override void Use()
        {
            var slots = GameObject.FindObjectsOfType<EquipmentSlot>();
            foreach (var slot in slots)
            {
                if (slot.Item is BaseWeapon)
                {
                    slot.InvokeChangeWeapon(this);
                }
            }
        }
    }
}

