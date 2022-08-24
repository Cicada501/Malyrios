using System.Collections;
using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using TMPro;


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

    [SerializeField] float gravityToFall = 8;
    [SerializeField] float gravityToClimb = 2;

    [SerializeField] float attackRate = 1.5f;
    public static float nextAttackTime;
    float distToPlayer;

    [SerializeField] Transform attackPoint = null;
    [SerializeField] float attackRadius = 0.0f;
    [SerializeField] LayerMask playerLayer = 0;
    [SerializeField] TextMeshPro damageText = null;
    [SerializeField] Transform damageTextSpawn = null;

    Rigidbody2D rb;
    Transform player;

    [SerializeField] bool facingRight = true;
    Animator animator;

    [SerializeField] int maxHealth = 100;
    int currentHealth;

    bool isGrounded;
    bool isAttacking;


    bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() //-------------------------------------------------
    {
        //Damage Text rising up
        damageText.transform.position += new Vector3(10f, 10f, 0f) * Time.deltaTime;

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
            nextAttackTime = Time.time + 1f / attackRate;
        }

        //Look at Player
        if (transform.position.x > player.position.x && facingRight && !isAttacking)
        {
            EnemyFlip();
        }
        else if (transform.position.x < player.position.x && !facingRight && !isAttacking)
        {
            EnemyFlip();
        }
        
        distToPlayer = Mathf.Abs(rb.position.x - player.position.x);
        print("Dist"+distToPlayer);
        //distToPlayerY = Mathf.Abs(rb.position.y - player.position.y);
        animator.SetFloat("distToPlayer", distToPlayer);
        

        if (distToPlayer <= Enemy_run.attackRange)
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.angularVelocity = 0f;
        }
    } //----------------------END: Update -----------------------------------

    void SpawnDamage(int damage)
    {
        damageText.SetText(damage.ToString());
        Instantiate(damageText, damageTextSpawn.position, Quaternion.identity);
    }

    public void DealDamage(int damage) //where used?
    {
        Collider2D[] thatGotHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
        if (thatGotHit.Length > 0)
        {
            player.GetComponent<IHealthController>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        SpawnDamage(damage);

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
        rb.velocity = new Vector2(0f, 0f);
        rb.angularVelocity = 0f;

        rb.gravityScale = 0;

        //Disable all coliders when dead
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }

        //Disable Script after colliders (otherwise coliders dont get disabled)
        this.enabled = false;

        //DROP ITEMS
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
    }


    void EnemyFlip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // used in animations
    void Shake() //Not used yet?
    {
        CameraShake_Cinemachine.Shake(0.3f, 0.33f, 10f);
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


    //chage graviy if upwards ground before 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" && !isDead)
        {
            rb.gravityScale = gravityToClimb;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" && !isDead)
        {
            rb.gravityScale = gravityToFall;
        }
    }
}