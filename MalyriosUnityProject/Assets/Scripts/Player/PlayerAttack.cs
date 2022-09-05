using System;
using System.Collections;
using System.Collections.Generic;
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

    float nextAttackTime = 0f;
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
        EquippedWeaponID = SaveSystem.LoadInventory().equippedWeaponID;
        BaseWeapon loadedWeapon = ItemDatabase.GetWeapon((EquippedWeaponID));
        equippedWeapon = loadedWeapon;
        OnWeaponChanged(loadedWeapon);
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        //Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
    void Update()
    {
        
        if (this.equippedWeapon == null) return;

        //Disable sword if animation has finished (enabled in Attack(), cause if its disabled swortAttack1Beaviour is disabled aswell)
        
        //check if the attackrate allows the next attack
        if (Time.time >= nextAttackTime)
        {
            isAttacking = false;
            if (Player.attackInput && !InventoryUI.inventoryOpen)
            {
                
                Attack();
                isAttacking = true;
                nextAttackTime = Time.time + 1f / this.equippedWeapon.AttackSpeed;
                timeForAnimPause = Time.time + startFreezingTime; //when to start freeze 
                timeForAnimResume = Time.time + endFreezingTime; //when to end freeze
            }
        }

        //if player hits an enemy, interrupt animation for a short time
        if (enemyInDamagezone)
        {
            //happens first
            if (Time.time >= timeForAnimPause && Time.time <= timeForAnimResume)
            {
                playerAnimator.enabled = false;
            }
            //happens afterwards
            else if (Time.time >= timeForAnimResume)
            {
                playerAnimator.enabled = true;
                enemyInDamagezone = false;
            }
        }


        
    }

    void Attack()
    {
        print("Attack");
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
                    enemiesGotHit.Add(enemy.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weapon"></param>
    private void OnWeaponChanged(BaseWeapon weapon)
    {
        if (this.weaponHolder.transform.transform.childCount > 0)
        {
            Destroy(this.weaponHolder.transform.GetChild(0).gameObject);
            if (!weaponLoaded)
            {
                weaponLoaded = true;
            }else
            {
                StartCoroutine(SpawnUnequippedWeapon(equippedWeapon));
            }
            
            EquippedWeaponID = 0;
        }

        if (weapon == null)
        {
            //Inventory.Instance.AddItem(equippedWeapon);
            this.equippedWeapon = null;
            return;
        }

        GameObject go = Instantiate(weapon.ItemPrefab, weaponHolder.transform);
        this.swordAnimator = go.GetComponent<Animator>();
        this.equippedWeapon = weapon;
        EquippedWeaponID = weapon.ItemID;
        
    }


    //Quick and dirty fix for the problem that the unequipped weapon is not spawned correctly in the inventory if the slot of the new weapon and slot where the unequipped weapon goes are the same
    IEnumerator SpawnUnequippedWeapon(BaseWeapon weapon)
    {
        yield return new WaitForSeconds(0.1f);
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