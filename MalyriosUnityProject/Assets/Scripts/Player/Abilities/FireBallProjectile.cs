﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] GameObject impactEffect = null;
    [SerializeField] private int fireballDamage;
    private Rigidbody2D rigidBody;
    
    
    void Start()
    {   //rigidBody has continoous velocity forward
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(projectileSpeed, -0.05f*projectileSpeed);

    }
       
    //On Collision of Fireball with enemy
    private void OnTriggerEnter2D(Collider2D collision){     
        if (collision.gameObject.tag == "Enemy")
        {
            //Show impacteffect
            Instantiate(impactEffect,transform.position,Quaternion.identity);
            //Deal damage
            collision.gameObject.GetComponent<Enemy>().TakeDamage(fireballDamage);
            //Destroy fireball
            Destroy(gameObject);
        }
    }
}
