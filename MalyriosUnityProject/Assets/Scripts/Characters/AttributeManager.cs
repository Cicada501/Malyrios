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

    //
    //
    //
    //
    private BaseWeapon weapon;
    
    private void Awake()
    {
        this.baseAttributes = ReferencesManager.Instance.player.GetComponent<BaseAttributes>();
        
        this.weaponItem = weaponSlot.GetComponent<ISlot>();
        
        EquipmentSlot.OnWeaponChanged += OnWeaponChanged;
    }

    private void OnWeaponChanged(BaseWeapon weapon)
    {
        this.weapon = weaponItem.Item as BaseWeapon;
        
        CalculateAttributes();
    }

    private void CalculateAttributes()
    {
        ResetAttributes();
        
        this.baseAttributes.Strength += weapon.Strength;
        this.baseAttributes.CritChance += weapon.CritChance;
        this.baseAttributes.CritDamage += weapon.CritDamage;
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
