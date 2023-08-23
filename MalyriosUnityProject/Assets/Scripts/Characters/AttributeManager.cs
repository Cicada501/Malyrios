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
        
        weaponItem = weaponSlot.GetComponent<ISlot>();
        headArmorItem = headArmorSlot.GetComponent<ISlot>();
        bodyArmorItem = bodyArmorSlot.GetComponent<ISlot>();
        handArmorItem = handArmorSlot.GetComponent<ISlot>();
        feetArmorItem = feetArmorSlot.GetComponent<ISlot>();
        
        EquipmentSlot.OnArmorChanged += OnArmorChanged;
        EquipmentSlot.OnWeaponChanged += OnWeaponChanged;
    }
    
    private void OnArmorChanged(BaseArmor newArmor)
    {
        if (newArmor.ItemType == BaseItem.ItemTypes.Head)
        {
            OnArmorSwap(ref headArmor, newArmor);
        }
        else if (newArmor.ItemType == BaseItem.ItemTypes.Body)
        {
            OnArmorSwap(ref bodyArmor, newArmor);
        }
        else if (newArmor.ItemType == BaseItem.ItemTypes.Hand)
        {
            OnArmorSwap(ref handArmor, newArmor);
        }
        else if (newArmor.ItemType == BaseItem.ItemTypes.Feet)
        {
            OnArmorSwap(ref feetArmor, newArmor);
        }
    }
    
    private void OnArmorSwap(ref BaseArmor currentArmor, BaseArmor newArmor)
    {
        if (currentArmor)
        {
            OnArmorRemove(currentArmor);
        }

        OnArmorAdd(newArmor);
        currentArmor = newArmor;
    }

    private void OnArmorAdd(BaseArmor armor)
    {
        if (!armor) return;
        this.baseAttributes.MaxHealth += armor.HealthBonus;
        this.baseAttributes.Strength += armor.StrengthBonus;
        this.baseAttributes.CritChance += armor.CritChanceBonus;
        this.baseAttributes.CritDamage += armor.CritDamageBonus;
        //...
    }

    private void OnArmorRemove(BaseArmor armor)
    {
        if (!armor) return;
        this.baseAttributes.MaxHealth -= armor.HealthBonus;
        this.baseAttributes.Strength -= armor.StrengthBonus;
        this.baseAttributes.CritChance -= armor.CritChanceBonus;
        this.baseAttributes.CritDamage -= armor.CritDamageBonus;
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
