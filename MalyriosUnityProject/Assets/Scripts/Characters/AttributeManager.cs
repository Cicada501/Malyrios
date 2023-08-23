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
   
    private BaseWeapon weapon;
    
    private void Awake()
    {
        this.baseAttributes = ReferencesManager.Instance.player.GetComponent<BaseAttributes>();
        EquipmentSlot.OnWeaponChanged += OnWeaponChanged;
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
