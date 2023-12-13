using Malyrios.Character;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    [SerializeField] private GameObject headArmorSlot = null;
    [SerializeField] private GameObject bodyArmorSlot = null;
    [SerializeField] private GameObject handArmorSlot = null;
    [SerializeField] private GameObject feetArmorSlot = null;
    [SerializeField] private GameObject weaponSlot = null;

    private StatsWindow statsWindow;
    private BaseAttributes baseAttributes;

    private ISlot headArmorItem;
    private ISlot bodyArmorItem;
    private ISlot handArmorItem;
    private ISlot feetArmorItem;
    private ISlot weaponItem;
    
    private BaseWeapon weapon;
    private BaseArmor headArmor;
    private BaseArmor bodyArmor;
    private BaseArmor handArmor;
    private BaseArmor feetArmor;
    
    private void Awake()
    {
        this.baseAttributes = ReferencesManager.Instance.player.GetComponent<BaseAttributes>();
        statsWindow = ReferencesManager.Instance.statsWindow;
        
        weaponItem = weaponSlot.GetComponent<ISlot>();
        headArmorItem = headArmorSlot.GetComponent<ISlot>();
        bodyArmorItem = bodyArmorSlot.GetComponent<ISlot>();
        handArmorItem = handArmorSlot.GetComponent<ISlot>();
        feetArmorItem = feetArmorSlot.GetComponent<ISlot>();
        
        EquipmentSlot.OnArmorChanged += OnArmorChanged;
        EquipmentSlot.OnWeaponChanged += OnWeaponChanged;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newArmor">Beim Ausrüsten eines neuen Gegenstandes ist dies der Gegenstand. Beim Ablegen eines Gegenstandes ist dies null</param>
    /// <param name="type">ItemType des items das angelegt oder abgelegt wird</param>
    private void OnArmorChanged(BaseArmor newArmor, BaseItem.ItemTypes type)
    {
        //print($"OnArmorChanged({newArmor?.ItemName ?? "null"}, {type})");
        if (type == BaseItem.ItemTypes.Head) //type wird extra weiter gegeben, da im falle von Unequip newArmor = null ist und newArmor.ItemType nicht verwendet werden kann
        {
            OnArmorSwap(ref headArmor, newArmor);
        }
        else if (type == BaseItem.ItemTypes.Body)
        {
            OnArmorSwap(ref bodyArmor, newArmor);
        }
        else if (type == BaseItem.ItemTypes.Hand)
        {
            OnArmorSwap(ref handArmor, newArmor);
        }
        else if (type == BaseItem.ItemTypes.Feet)
        {
            OnArmorSwap(ref feetArmor, newArmor);
        }
    }
    
    private void OnArmorSwap(ref BaseArmor currentArmor, BaseArmor newArmor)
    {
        //print($"Swapping: {currentArmor?.ItemName ?? "null"} to {newArmor?.ItemName ?? "null"}");


        if (currentArmor)
        {
            OnArmorRemove(currentArmor);
        }

        OnArmorAdd(newArmor);
        currentArmor = newArmor;
        statsWindow.UpdateStatTexts();
    }

    private void OnArmorAdd(BaseArmor armor)
    {
        if (!armor) return;
        this.baseAttributes.MaxHealth += armor.HealthBonus;
        this.baseAttributes.Mana += armor.ManaBonus;
        this.baseAttributes.Strength += armor.StrengthBonus;
        this.baseAttributes.CritChance += armor.CritChanceBonus;
        this.baseAttributes.CritDamage += armor.CritDamageBonus;
        this.baseAttributes.Haste += armor.HasteBonus;
        this.baseAttributes.Energy += armor.EnergyBonus;
        this.baseAttributes.Balance += armor.BalanceBonus;
    }

    private void OnArmorRemove(BaseArmor armor)
    {
        //print($"removing Armor: {armor.ItemName}");
        if (!armor) return;
        this.baseAttributes.MaxHealth -= armor.HealthBonus;
        this.baseAttributes.Mana -= armor.ManaBonus;
        this.baseAttributes.CritChance -= armor.CritChanceBonus;
        this.baseAttributes.CritDamage -= armor.CritDamageBonus;
        this.baseAttributes.Balance -= armor.BalanceBonus;
        this.baseAttributes.Strength -= armor.StrengthBonus;
        this.baseAttributes.Haste -= armor.HasteBonus;
        this.baseAttributes.Energy -= armor.EnergyBonus;
    }

    private void OnWeaponChanged(BaseWeapon wpn)
    {
        if (weapon)
        {
            if (wpn)
            {
                //swapping weapons
                OnWeaponRemove(weapon);
                OnWeaponAdd(wpn);
            }
            else
            {
                //un equip current weapon
                OnWeaponRemove(weapon);
            }
        }
        else
        {
            //equip new weapon (no unequipping)
            OnWeaponAdd(wpn);
        }

    }

    private void OnWeaponAdd(BaseWeapon wpn)
    {
        if (!wpn) return;
        this.baseAttributes.Strength += wpn.Strength;
        this.baseAttributes.CritChance += wpn.CritChance;
        this.baseAttributes.CritDamage += wpn.CritDamage;

        weapon = wpn;

    }
    private void OnWeaponRemove(BaseWeapon wpn)
    {
        if (!wpn) return;
        this.baseAttributes.Strength -= wpn.Strength;
        this.baseAttributes.CritChance -= wpn.CritChance;
        this.baseAttributes.CritDamage -= wpn.CritDamage;

        weapon = null;

    }

    private void ResetAttributes()
    {
        this.baseAttributes.Strength = 0;
        this.baseAttributes.CritChance = 0;
        this.baseAttributes.CritDamage = 0;
        this.baseAttributes.Balance = 0;
        this.baseAttributes.MaxHealth = 1000;
        this.baseAttributes.Mana = 0;
        this.baseAttributes.Haste = 0;
    }
}
