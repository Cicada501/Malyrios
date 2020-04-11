using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Character
{
    public class BaseAttributes : MonoBehaviour
    {
        public static event Action<int> OnMaxHealthChanged;
        public static event Action<float> OnCurrentHealthChanged;
        public static event Action<int> OnManaChanged;
        public static event Action<float> OnStrengthChanged;
        public static event Action<float> OnCritChanceChanged;
        public static event Action<float> OnHasteChanged;
        public static event Action<float> OnEnergyChanged;

        [SerializeField] private int maxMaxHealth;
        [SerializeField] private int mana;
        [SerializeField] private float strength;
        [SerializeField] private float critChance;
        [SerializeField] private float haste;
        [SerializeField] private float energy;


        private float currentHealth;

        /// <summary>
        /// Gets or sets the max health.
        /// Also fired an event OnMaxHealthChanged.
        /// </summary>
        public int MaxHealth
        {
            get => this.maxMaxHealth;
            set
            {
                this.maxMaxHealth = value;
                OnMaxHealthChanged?.Invoke(this.maxMaxHealth);
            }
        }

        /// <summary>
        /// Gets or sets the current health.
        /// Also fired an event OnCurrentHealthChanged.
        /// </summary>
        public float CurrentHealth
        {
            get => this.currentHealth;
            set
            {
                this.currentHealth = value;
                OnCurrentHealthChanged?.Invoke(this.currentHealth);
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
            }
        }

        /// <summary>
        /// Gets or sets the Strength.
        /// Also fired an event OnStrengthChanged.
        /// </summary>
        public float Strength
        {
            get => this.strength;
            set
            {
                this.strength = value;
                OnStrengthChanged?.Invoke(this.strength);
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
            }
        }

        private void Start()
        {
            CurrentHealth = this.maxMaxHealth;
            OnMaxHealthChanged?.Invoke(this.maxMaxHealth);
        }
    }
}