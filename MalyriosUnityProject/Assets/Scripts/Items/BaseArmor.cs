using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Items/NewArmor")]
public class BaseArmor : BaseItem
{
    [Header("Armor Properties")]
    [SerializeField] private int healthBonus;
    [SerializeField] private int manaBonus;
    [SerializeField] private int strengthBonus;
    [SerializeField] private float critChanceBonus;
    [SerializeField] private float critDamageBonus;
    [SerializeField] private float hasteBonus;
    [SerializeField] private float energyBonus;
    [SerializeField] private float balanceBonus;

    public int HealthBonus => this.healthBonus;
    public int ManaBonus => this.manaBonus;
    public int StrengthBonus => this.strengthBonus;
    public float CritChanceBonus => this.critChanceBonus;
    public float CritDamageBonus => this.critDamageBonus;
    public float HasteBonus => this.hasteBonus;
    public float EnergyBonus => this.energyBonus;
    public float BalanceBonus => this.balanceBonus;

    public override void ExecuteUsageEffect()
    {
        ActiveItemWindow.Instance.activeSlot.RemoveItem();
        var slots = GameObject.FindObjectsOfType<EquipmentSlot>();
        foreach (var slot in slots)
        {
            if (slot.itemType == this.itemType)
            {
                slot.InvokeChangeArmor(this);
            }
        }
    }
    
    public string GetDescription()
    {
        string description = $"{this.description}\n\n";

        if (healthBonus > 0)
        {
            description += $"<color=red>Leben: {this.healthBonus}</color>\n";
        }

        if (manaBonus > 0)
        {
            description += $"<color=#3399ff>Mana: {this.manaBonus}</color>\n";
        }

        if (strengthBonus > 0)
        {
            description += $"<color=#804000>Stärke: {this.strengthBonus}</color>\n";
        }

        if (critChanceBonus > 0)
        {
            description += $"<color=#cca300>Krit Chance: {this.critChanceBonus}%</color>\n";
        }

        if (critDamageBonus > 0)
        {
            description += $"<color=#e6005c>Kritt Schaden: {this.critDamageBonus}</color>\n";
        }

        if (hasteBonus > 0)
        {
            description += $"<color=#00b300>Schnelligkeit: {this.hasteBonus}</color>\n";
        }

        if (energyBonus > 0)
        {
            description += $"<color=#ff3300>Intelligenz: {this.energyBonus}</color>\n";
        }

        if (balanceBonus > 0)
        {
            description += $"<color=blue>Ausgeglichenheit: {this.balanceBonus}</color>\n";
        }

        return description;
    }
}

