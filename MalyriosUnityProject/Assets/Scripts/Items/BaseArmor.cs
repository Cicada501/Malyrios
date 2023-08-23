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
}

