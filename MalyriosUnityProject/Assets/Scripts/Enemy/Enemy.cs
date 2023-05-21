using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    [SerializeField] BaseItem dropItem = null;

    [SerializeField] BaseItem dropRareItem = null;
    [SerializeField] int dropItemChance0 = 10; //chance to drop 0 times Item
    [SerializeField] int dropItemChance1 = 60; //chance to drop 1 times Item
    [SerializeField] int dropItemChance2 = 30;
    [SerializeField] int dropRareItemChance0 = 50;
    [SerializeField] int dropRareItemChance1 = 40; //chance to drop 1 times RareItem
    [SerializeField] int dropRareItemChance2 = 10; //chance to drop 2 times RareItem

    [SerializeField] float attacksPerSecond = 1.5f;
    [SerializeField] public float attackRange;
    public float nextAttackTime;
    float distToPlayer;

    [SerializeField] Transform attackPoint = null;
    [SerializeField] float attackRadius = 0.1f;
    [SerializeField] LayerMask playerLayer = 0;
    [SerializeField] Transform damageTextSpawn;
    [SerializeField] private GameObject damagePopupText;

    Rigidbody2D rb;
    Transform player;

    [SerializeField] bool facingRight = true;
    Animator animator;

    [SerializeField] int maxHealth = 100;
    int currentHealth;

    bool isGrounded;
    bool isAttacking;
    bool isDead;

    private EnemySpawner enemySpawner;
    [SerializeField] public EnemyTypes enemyType;

    public enum EnemyTypes //Needed to respawn the enemy that died
    {
        Other,
        Werewolf,
        Rat,
        Ogre,
        Djinn,
        Mandrake,
        Boar
    }

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    private void Update() //-------------------------------------------------
    {
        //Set is Attacking if attack animation plays
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }


        //Recognize when Enemy has attacked, and set nextAttackTime
        if (animator.GetBool("Attack") == true)
        {
            nextAttackTime = Time.time + 1f / attacksPerSecond;
        }

        //Look at Player
        if (transform.position.x > player.position.x && facingRight && !isAttacking && !isDead)
        {
            EnemyFlip();
        }
        else if (transform.position.x < player.position.x && !facingRight && !isAttacking && !isDead)
        {
            EnemyFlip();
        }

        distToPlayer = Mathf.Abs(rb.position.x - player.position.x) + Mathf.Abs(rb.position.y - player.position.y);
        //distToPlayerY = Mathf.Abs(rb.position.y - player.position.y);
        animator.SetFloat("distToPlayer", distToPlayer);
    } //----------------------END: Update -----------------------------------

    IEnumerator ShowDamagePopup(int damage)
    {
        damagePopupText.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damage;
        var popupText = Instantiate(damagePopupText, damageTextSpawn.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(popupText);
    }

    public void DealDamage(int damage) //used at animation event of enemy
    {
        Collider2D[] thatGotHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
        if (thatGotHit.Length > 0)
        {
            player.GetComponent<IHealthController>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(ShowDamagePopup(damage));
        CameraShake.Instance.ShakeCamera(0.1f,0.05f);

        currentHealth -= damage;
        if (!isDead)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (currentHealth < 0.4 * maxHealth) //bedingung hinzufügen: wenn isEnraged animation existiert ..
        {
            animator.SetBool("isEnraged", true);
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);

        //dont move when dead
        /*rb.velocity = new Vector2(0f, 0f);
        rb.angularVelocity = 0f;
        rb.gravityScale = 0;*/ //enemy should not fall trough ground when collides get disabled
        Destroy(rb);
        //Disable all coliders when dead
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }

        #region dropItems

        //Item Normal
        int dropchoice = Random.Range(0, 100);
        if (dropchoice > dropItemChance0 && dropchoice <= dropItemChance1 + dropItemChance0)
        {
            SpawnItem.Spawn(dropItem, new Vector2(transform.position.x - 0.1f, transform.position.y));
        }
        else if (dropchoice > dropItemChance1 + dropItemChance0 &&
                 dropchoice <= dropItemChance2 + dropItemChance1 + dropItemChance0)
        {
            SpawnItem.Spawn(dropItem, transform.position);
            SpawnItem.Spawn(dropItem, new Vector2(transform.position.x - 0.1f, transform.position.y));
        }

        //Item Rare
        dropchoice = Random.Range(0, 100);
        if (dropchoice > dropRareItemChance0 && dropchoice <= dropRareItemChance1 + dropRareItemChance0)
        {
            SpawnItem.Spawn(dropRareItem, new Vector2(transform.position.x + 0.1f, transform.position.y));
        }
        else if (dropchoice > dropRareItemChance1 + dropRareItemChance0 &&
                 dropchoice <= dropRareItemChance2 + dropRareItemChance1 + dropRareItemChance0)
        {
            SpawnItem.Spawn(dropRareItem, new Vector2(transform.position.x + 0.2f, transform.position.y));
            SpawnItem.Spawn(dropRareItem, new Vector2(transform.position.x + 0.1f, transform.position.y));
        }

        #endregion

        //Because the enemy gets instantiated as child of the spawnpoint, transform.parent.transform can be used as spawnpoint
        enemySpawner.Respawn(enemyType, transform.parent.transform);
        print($"{enemyType} respawn Cooldown started");

        //Disable Script after colliders (otherwise colliders dont get disabled)
        this.enabled = false;
    }


    void EnemyFlip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Draw enemy attack Circle
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}