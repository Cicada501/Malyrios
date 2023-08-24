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
        string description = $"<color=green>{this.itemName}</color>\n\n";

        if (healthBonus > 0)
        {
            description += $"Health Bonus: {this.healthBonus}\n";
        }

        if (manaBonus > 0)
        {
            description += $"Mana Bonus: {this.manaBonus}\n";
        }

        if (strengthBonus > 0)
        {
            description += $"Strength Bonus: {this.strengthBonus}\n";
        }

        if (critChanceBonus > 0)
        {
            description += $"Crit Chance Bonus: {this.critChanceBonus}%\n";
        }

        if (critDamageBonus > 0)
        {
            description += $"Crit Damage Bonus: {this.critDamageBonus}%\n";
        }

        if (hasteBonus > 0)
        {
            description += $"Haste Bonus: {this.hasteBonus}\n";
        }

        if (energyBonus > 0)
        {
            description += $"Energy Bonus: {this.energyBonus}\n";
        }

        if (balanceBonus > 0)
        {
            description += $"Balance Bonus: {this.balanceBonus}\n";
        }

        return description;
    }
}

