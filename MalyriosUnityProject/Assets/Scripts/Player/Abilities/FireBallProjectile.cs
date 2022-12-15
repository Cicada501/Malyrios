﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] GameObject impactEffect = null;
    [SerializeField] private int fireballDamage;
    private Rigidbody2D rigidBody;
    //private Player player;
    private bool playerFlipped;


    void Start()
    {   //rigidBody has continoous velocity forward
        rigidBody = GetComponent<Rigidbody2D>();
        

        // if player is looking to the left, throw ball to the left
        if (this.gameObject.GetComponent<Transform>().rotation.y < 0)
        {
            rigidBody.velocity = new Vector2(-projectileSpeed, -0.05f*projectileSpeed);
        }
        else
        {
            rigidBody.velocity = new Vector2(projectileSpeed, -0.05f*projectileSpeed);
        }
    }

    //On Collision of Fireball with enemy
    private void OnTriggerEnter2D(Collider2D collision){     
        if (collision.gameObject.CompareTag("Enemy"))
        {
           
            //offset x value to have imact at right position
            var position = transform.position;
            var spawnImpactPoint = new Vector3(position.x+0.4f, position.y, position.z);
            //Show impacteffect
            Instantiate(impactEffect,spawnImpactPoint,Quaternion.identity);
            //Deal damage
            collision.gameObject.GetComponent<Enemy>().TakeDamage(fireballDamage);
            //Destroy fireball
            Destroy(gameObject);
        }
    }
}
