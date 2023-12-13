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
            return $"{this.description}\n\n" +
                   $"<color=red>Schaden: {this.minDamage}-{this.maxDamage}</color>\n" +
                   $"<color=#00b300>Angriffsgeschwindigkeit: {this.attackSpeed}</color>\n" +
                   $"<color=#804000>Stärke: {this.strength}</color>\n" +
                   $"<color=#cca300>Kritt Chance: {this.critChance}%</color>\n" +
                   $"<color=#e6005c>Krit Schaden: {this.critDamage}</color>";
        }

        public override void ExecuteUsageEffect()
        {
            ActiveItemWindow.Instance.activeSlot.RemoveItem();
            var slots = GameObject.FindObjectsOfType<EquipmentSlot>();
            foreach (var slot in slots)
            {
                if (slot.itemType == ItemTypes.Weapon)
                {
                    slot.InvokeChangeWeapon(this);
                }
            }
        }
    }
}

