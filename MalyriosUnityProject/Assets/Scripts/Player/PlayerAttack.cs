using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Malyrios.Character;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{


    [SerializeField] private GameObject weaponHolder = null;
    
    private bool enemyInDamagezone = false;
    private float timeForAnimPause = 0f;
    private float timeForAnimResume = 0f;

    float nextAttackTime;
    public static bool isAttacking = false;

    private BaseWeapon equippedWeapon;
    private Animator playerAnimator;
    private Animator swordAnimator;

    private BaseAttributes baseAttributes;

    int soundChoice;

    [Header("Attack Sound Properties")] [SerializeField]
    AudioSource meeleeSound1  = null;

    [SerializeField] AudioSource meeleeSound2 = null;
    [SerializeField] AudioSource meeleeSound3 = null;
    [SerializeField] AudioSource hitSound1;
    [SerializeField] AudioSource hitSound2;
    [SerializeField] AudioSource hitSound3;
    [SerializeField] AudioSource changeWeaponSound = null;
    private int hitSoundIndex = 0; 

    [SerializeField] Transform attackPoint = null;
    [SerializeField] float attackRadius = 0.5f;
    [SerializeField] LayerMask enemyLayers = 0;
    //[SerializeField] int attackDamage = 20;

    [SerializeField] Animator cameraAnimator = null;

    public int EquippedWeaponID;
    [SerializeField] private EquipmentSlot weaponSlot;

    private void Awake()
    {
        EquipmentSlot.OnWeaponChanged += OnWeaponChanged; //subscribe method to event (both same name)
    }

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        this.baseAttributes = GetComponent<BaseAttributes>();
    }

    public void LoadWeapon(int id)
    {
        if (id != 0)
        {
            EquippedWeaponID = id;
            EquipWeapon(ItemDatabase.GetWeapon(EquippedWeaponID));
            weaponSlot.LoadWeapon(id);
        }
        else
        {
            UnequipWeapon();
            if(weaponSlot.Item!=null)
            weaponSlot.RemoveItem();
        }

    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        //Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }

    private float time;

    public void TriggerAttack()
    {
        if (this.equippedWeapon == null)
        {
            print("Showing massage");
            ShowMessage.Instance.Say("Du Musst eine Waffe Ausrüsten um Anzugreifen");
            return;
        }
        //check if the attackrate allows the next attack
        if (time >= nextAttackTime)
        {

            isAttacking = false;
            if (!InventoryUI.inventoryOpen && equippedWeapon)
            {
                Attack();
                isAttacking = true;
                nextAttackTime = time + 2f / (equippedWeapon.AttackSpeed+baseAttributes.Haste/10);
            }
        }
    }
    void Update()
    {
        time = Time.time;
    }

    public void Attack()
    {

            
        playerAnimator.SetTrigger("Attack");
        swordAnimator.SetTrigger("Attack");

        #region AttackSound

        soundChoice = Random.Range(0, 2);
        if (soundChoice == 0)
        {
            meeleeSound1.Play();
        }
        else if (soundChoice == 1)
        {
            meeleeSound2.Play();
        }
        else if (soundChoice == 2)
        {
            meeleeSound3.Play();
        }

        #endregion
        
        //get list of all colliders in hit range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);
        //remember the gameobject of the collider, to only hit it once if it has multiple colliders
        List<GameObject> enemiesGotHit = new List<GameObject>();
        if (hitEnemies.Length > 0)
        {
            enemyInDamagezone = true;

            #region HitSound
            if (hitSoundIndex == 1)
            {
                hitSound1.Play();
            }
            else if (hitSoundIndex == 2)
            {
                hitSound2.Play();
            }
            else if (hitSoundIndex == 0)
            {
                hitSound3.Play();
            }
            hitSoundIndex = (hitSoundIndex + 1) % 3;
            

            #endregion
            
            foreach (Collider2D enemy in hitEnemies)
            {
                if (!enemiesGotHit.Contains(enemy.gameObject))
                {
                    float crit = Random.Range(0f, 100f);
                    int damage = Random.Range(this.equippedWeapon.MinDamage, this.equippedWeapon.MaxDamage);

                    if (crit <= this.baseAttributes.CritChance)
                    {
                        damage = ((int)(this.equippedWeapon.MaxDamage + this.equippedWeapon.MaxDamage * 0.25) * (int)(1 + (this.baseAttributes.CritDamage / 100f)));
                        //Debug.Log("Ich bin ein Crit");
                    }
                    
                    damage += this.baseAttributes.Strength;
                    
                    //apply damage to enemy
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                    enemy.GetComponent<Enemy>().PushBack();
                    
                    
                    enemiesGotHit.Add(enemy.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Checks if a weapon is equipped and if so, unequips it and equips the new one.
    /// If called with null, unequips the current weapon.
    /// </summary>
    /// <param name="weaponToEquip (can be null)"></param>
    /// <param name="weaponToEquip"></param>
    private void OnWeaponChanged([CanBeNull] BaseWeapon weaponToEquip)
    {
        if (weaponToEquip != null)
        {
            EquipWeapon(weaponToEquip);
            changeWeaponSound.Play();
        }
        else
        {
            UnequipWeapon();
        }
    }

    public void EquipWeapon(BaseWeapon weapon)
    {
        GameObject go = Instantiate(weapon.ItemPrefab, weaponHolder.transform);
        this.swordAnimator = go.GetComponent<Animator>();
        this.equippedWeapon = weapon;
        EquippedWeaponID = weapon.ItemID;
    }
    
    private void UnequipWeapon()
    {
        equippedWeapon = null;
        EquippedWeaponID = 0;
        if(weaponHolder.transform.childCount>0) Destroy(this.weaponHolder.transform.GetChild(0).gameObject);
    }

    //Quick and dirty fix for the problem that the unequipped weapon is not spawned correctly in the inventory if the slot of the new weapon and slot where the unequipped weapon goes are the same
    IEnumerator SpawnUnequippedWeaponDelayed(BaseWeapon weapon)
    {
        yield return new WaitForSeconds(0.5f);
        Inventory.Instance.AddItem(weapon);
    }

    //Draw a sphere to see the attack Range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}