﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] float startFreezingTime = 0.1f;
    [SerializeField] float endFreezingTime = 0.2f;
    [SerializeField] GameObject sword;
    bool enemyInDamagezone = false;
    float timeForAnimPause = 0f;
    float timeForAnimResume = 0f;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;
    public static bool isAttacking = false;



    int soundChoice;
    [SerializeField] AudioSource meeleeSound1;
    [SerializeField] AudioSource meeleeSound2;
    [SerializeField] AudioSource meeleeSound3;
    [SerializeField] AudioSource hitmarkerSound;


    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRadius = 0.5f;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] int attackDamage = 20;


    Animator playerAnimator;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] Animator swordAnimator;


    // Use this for initialization
    void Start()
    {
        sword.SetActive(false);
        playerAnimator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        //Disable sword if animation has finished (enabled in Attack(), cause if its disabled swortAttack1Beaviour is disabled aswell)
        if (swordAttack1Beahviour.swordActive)
        {
            sword.SetActive(true);
        }
        else
        {
            sword.SetActive(false);
        }

        //check if the attackrate allows the next attack
        if (Time.time >= nextAttackTime)
        {

            isAttacking = false;
            if (Player.attackInput)
            {
                Attack();
                isAttacking = true;
                nextAttackTime = Time.time + 1f / attackRate;
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


    }//Update

    void Attack()
    {
        sword.SetActive(true);
        playerAnimator.SetTrigger("Attack");
        swordAnimator.SetTrigger("Attack");
        soundChoice = Random.Range(0, 2);
        if (soundChoice == 0) { meeleeSound1.Play(); }
        else if (soundChoice == 1) { meeleeSound2.Play(); }
        else if (soundChoice == 2) { meeleeSound3.Play(); }

        //get list of all colliders in hit range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);
        //remember the gameobject of the collider, to only hit it once if it has multiple colliders
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
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                    enemiesGotHit.Add(enemy.gameObject);
                }
            }
        }

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