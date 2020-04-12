using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Items
{
    public class BaseWeapon : BaseItem
    {
        [Header("Weapon Properties")] 
        [SerializeField] private int minDamange;
        [SerializeField] private int maxDamage;
        [SerializeField] private float attackSpeed;

        public int MinDamange => this.minDamange;
        public int MaxDamage => this.maxDamage;
        public float AttackSpeed => this.attackSpeed;
    }
}

