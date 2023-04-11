﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Malyrios.Character;
using Malyrios.Items;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float startFreezingTime = 0.1f;
    [SerializeField] float endFreezingTime = 0.2f;

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
    [SerializeField] AudioSource hitmarkerSound = null;
    [SerializeField] AudioSource changeWeaponSound = null;

    [SerializeField] Transform attackPoint = null;
    [SerializeField] float attackRadius = 0.5f;
    [SerializeField] LayerMask enemyLayers = 0;
    //[SerializeField] int attackDamage = 20;

    [SerializeField] Animator cameraAnimator = null;

    public static int EquippedWeaponID;
    private bool weaponLoaded = false;

    private void Awake()
    {
        EquipmentSlot.OnWeaponChanged += OnWeaponChanged; //subscribe method to event (both same name)
    }

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        this.baseAttributes = GetComponent<BaseAttributes>();
        //EquippedWeaponID = SaveSystem.LoadInventory().equippedWeaponID;
        if (EquippedWeaponID!=0)
        {
            EquipWeapon(ItemDatabase.GetWeapon(EquippedWeaponID));
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
        //check if the attackrate allows the next attack
        if (time >= nextAttackTime)
        {
            isAttacking = false;

            if (!InventoryUI.inventoryOpen)
            {
                Attack();
                isAttacking = true;
                nextAttackTime = time + 1f / equippedWeapon.AttackSpeed;
            }
        }
    }
    void Update()
    {
        time = Time.time;
    }

    public void Attack()
    {
        if (this.equippedWeapon == null) return;
        playerAnimator.SetTrigger("Attack");
        swordAnimator.SetTrigger("Attack");

        //Attack Sound
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

        //get list of all colliders in hit range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);
        //remember the gameobject of the collider, to only hit it once if it has multiple colliders
        //print("HitEnemies: " + hitEnemies.Length);
        List<GameObject> enemiesGotHit = new List<GameObject>();
        if (hitEnemies.Length > 0)
        {
            enemyInDamagezone = true;
            hitmarkerSound.Play();
            cameraAnimator.SetTrigger("EnemyHit");

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

                    //Debug.Log(damage);
                    
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                    var rb = enemy.GetComponent<Rigidbody2D>();
                    var playerTransform = GetComponent<Transform>();
                    rb.AddForce(new Vector2(playerTransform.localScale.x*300,300), ForceMode2D.Impulse);
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
        //when equipping a weapon (and not unequipping)
        if (weaponToEquip != null)
        {
            //if already a weapon equipped
            if (this.equippedWeapon != null)
            {
                Debug.Log("Unequipping weapon: " + this.equippedWeapon.ItemName);
                StartCoroutine(SpawnUnequippedWeaponDelayed(equippedWeapon));
                UnequipWeapon();
                
            }
            EquipWeapon(weaponToEquip);
            changeWeaponSound.Play();
            
        }
        //when unequipping a weapon
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
        Destroy(this.weaponHolder.transform.GetChild(0).gameObject);

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