﻿using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;

namespace Malyrios.Character
{
    public class BaseAttributes : MonoBehaviour
    {
        public static event Action<int> OnMaxHealthChanged;
        public static event Action<float, int> OnCurrentHealthChanged;
        public static event Action<int> OnManaChanged;
        public static event Action<float> OnStrengthChanged;
        public static event Action<float> OnCritChanceChanged;
        public static event Action<float> OnCritDamageChanged;
        public static event Action<float> OnHasteChanged;
        public static event Action<float> OnEnergyChanged;
        public static event Action<float> OnBalanceChaned;
        public static event Action<BaseAttributes> OnBaseAttributeChanged;

        [SerializeField] private int maxHealth = 1000;
        [SerializeField] private int mana = 0;
        [SerializeField] private int strength = 0;
        [SerializeField] private float critChance = 0;
        [SerializeField] private float critDamage = 0;
        [SerializeField] private float haste = 0;
        [SerializeField] private float energy = 0;
        [SerializeField] private float balance = 0;


        private int currentHealth;

        /// <summary>
        /// Gets or sets the max health.
        /// Also fired an event OnMaxHealthChanged.
        /// </summary>
        public int MaxHealth
        {
            get => this.maxHealth;
            set
            {
                print($"set of MaxHealth is used, value is: {value}");
                this.maxHealth = value;
                OnMaxHealthChanged?.Invoke(this.maxHealth);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the current health.
        /// Also fires an event OnCurrentHealthChanged.
        /// </summary>
        public int CurrentHealth
        {
            get => this.currentHealth;
            set
            {
                this.currentHealth = value;
                OnCurrentHealthChanged?.Invoke(this.currentHealth, this.maxHealth);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the mana.
        /// Also fired an event OnManaChanged.
        /// </summary>
        public int Mana
        {
            get => this.mana;
            set
            {
                this.mana = value;
                OnManaChanged?.Invoke(this.mana);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the Strength.
        /// Also fired an event OnStrengthChanged.
        /// </summary>
        public int Strength
        {
            get => this.strength;
            set
            {
                this.strength = value;
                OnStrengthChanged?.Invoke(this.strength);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the crit chance.
        /// Also fired an event OnCritChanceChanged.
        /// </summary>
        public float CritChance
        {
            get => this.critChance;
            set
            {
                this.critChance = value;
                OnCritChanceChanged?.Invoke(this.critChance);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        public float CritDamage
        {
            get => this.critDamage;
            set
            {
                this.critDamage = value;
                OnCritDamageChanged?.Invoke(this.critDamage);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the haste.
        /// Also fired an event OnHasteChanged.
        /// </summary>
        public float Haste
        {
            get => this.haste;
            set
            {
                this.critChance = value;
                OnHasteChanged?.Invoke(this.haste);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the energy.
        /// Also fired an event OnEnergyChanged.
        /// </summary>
        public float Energy
        {
            get => this.energy;
            set
            {
                this.energy = value;
                OnEnergyChanged?.Invoke(this.energy);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the balance.
        /// Also fires an event OnBalanceChanged.
        /// </summary>
        public float Balance
        {
            get => balance;
            set
            {
                this.balance = value;
                OnBalanceChaned?.Invoke(this.balance);
                OnBaseAttributeChanged?.Invoke(this);
            }
        }

        private void Awake()
        {
            EquipmentSlot.OnItemSlotChanged += OnEquipmentSlotOnOnItemSlotChanged;
        }

        private void OnEquipmentSlotOnOnItemSlotChanged(BaseItem item)
        {
            Debug.Log("W: ");
        }

        private void Start()
        {
            //CurrentHealth = this.maxHealth;
            OnMaxHealthChanged?.Invoke(this.maxHealth);
        }

        private void Update()
        {
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public void LoadAttributes(BaseAttributes gameDataLoadedAttributes)
        {
            MaxHealth = gameDataLoadedAttributes.MaxHealth;
            Mana = gameDataLoadedAttributes.Mana;
            Strength = gameDataLoadedAttributes.Strength;
            CritChance = gameDataLoadedAttributes.CritChance;
            CritDamage = gameDataLoadedAttributes.CritDamage;
            Haste = gameDataLoadedAttributes.Haste;
            Energy = gameDataLoadedAttributes.Energy;
            Balance = gameDataLoadedAttributes.Balance;
        }
    }
}