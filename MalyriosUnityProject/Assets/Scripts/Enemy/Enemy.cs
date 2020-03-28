﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float gravityToFall = 8;
    public float gravityToClimb = 2;


    public float attackRate = 1.5f;
    public static float nextAttackTime;
    float distToPlayer;
    float distToPlayerY;

    public Transform attackPoint;
    public float attackRadius;
    public LayerMask playerLayer;

    Rigidbody2D rb;
    Transform player;

    [SerializeField]
    bool facingRight = true;
    Animator animator;
    public int maxHealth = 100;
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

    private void Update()//-------------------------------------------------
    {

        //print("Time: "+ Time.time+ "NextAttackAt: "+ nextAttackTime);
        //print("isAttacking"+isAttacking);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack")||animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            isAttacking = true;

        }else{
            isAttacking = false;
        }
        if (distToPlayerY > 8)
        {
            distToPlayer = 0;
        }

        //Recognize when Enemy has attacked, and set nextAttackTime
        if (animator.GetBool("Attack") == true)
        {
        

            nextAttackTime = Time.time + 1f / attackRate;
        }

        //Look at Player
        if (transform.position.x > player.position.x && facingRight && !isAttacking)
        {
            enemyFlip();
        }
        else if (transform.position.x < player.position.x && !facingRight && !isAttacking)
        {
            enemyFlip();
        }

        distToPlayer = Mathf.Abs(rb.position.x - player.position.x);
        distToPlayerY = Mathf.Abs(rb.position.y - player.position.y);
        animator.SetFloat("distToPlayer", distToPlayer);

        if (distToPlayer <= Enemy_run.attackRange)
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.angularVelocity = 0f;
        }


    }//----------------------END: Update -----------------------------------

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (!isDead)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }else if(currentHealth < 0.4*maxHealth){
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
    }

    void dropItems(){
        
    }


    void enemyFlip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    // used in animations
    void Shake(){
        CameraShake_Cinemachine.Shake(0.3f,0.33f,10f);
    }

    public void DealDamage(int damage)
    {
        Collider2D[] thatGotHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
        if (thatGotHit.Length > 0)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
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
