using System;
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
    private GameObject player;

    public Vector2 FireballDirection { get; set; }


    void Start()
    {
        //rigidBody has continoous velocity forward
        rigidBody = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player");
        // if player is looking to the left, throw ball to the left
        if (player.transform.localScale.x < 0)
        {
            //Set this vector to the vector from GUI
            rigidBody.velocity = FireballDirection*projectileSpeed; //new Vector2(-projectileSpeed, -0.05f*projectileSpeed);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            rigidBody.velocity = FireballDirection*projectileSpeed;
        }
    }


    //On Collision of Fireball with enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            ReferencesManager.Instance.fireballImpactSound.Play();
            //offset x value to have imact at right position
            var position = transform.position;
            var spawnImpactPoint = new Vector3(position.x + 0.4f, position.y, position.z);
            //Show impacteffect
            Instantiate(impactEffect, spawnImpactPoint, Quaternion.identity);
            //Deal damage
            collision.gameObject.GetComponent<Enemy>().TakeDamage(fireballDamage);
            //Destroy fireball
            Destroy(gameObject);
        }
    }
}