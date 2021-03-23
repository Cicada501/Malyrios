using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] GameObject impactEffect;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * projectileSpeed;
    }
       
   

    private void OnTriggerEnter2D(Collider2D collision){
        
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(impactEffect,transform.position,Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
