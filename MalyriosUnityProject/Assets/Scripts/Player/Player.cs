using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem dust = null;
    [SerializeField] Rigidbody2D rb = null;
    public static bool attackInput;
    public static bool inventoryInput;

    /*[FormerlySerializedAs("spawnPoint2")]*/
    public Vector3 closestSpawnPoint = new Vector3(0.0f, 0.0f, 0.0f);
    public float jumpForce;
    public float backJumpRate = 0.5f;

    float speed;
    float verticalInput;
    float horizontal;
    float jumpTimeCounter;
    float usedBackJumpAt;

    bool jumpInput;
    bool dodgeInput;
    bool facingRight = true;
    bool isJumping = false;
    bool isClimbing = false;
    bool usingPush = false;


    // Use this for initialization


    // void FixedUpdate()
    // {
    //
    //     if (ButtonScript.DodgeInput)
    //     {
    //         BackJump(facingRight);
    //         ResetVelocity();
    //     }
    //     
    // }
    
    
    void ResetVelocity()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        usingPush = false;
    }

    void BackJump(bool facingRight)
    {
        CreateDust();
        usingPush = true;
        if (facingRight)
        {
            rb.AddForce(new Vector2(-50f, 0f));
        }
        else
        {
            rb.AddForce(new Vector2(50f, 0f));
        }

        rb.AddForce(new Vector2(0f, 25f));
        Invoke("ResetVelocity", 0.25f);
    }

    void flip()
    {
        CreateDust();
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void CreateDust()
    {
        dust.Play();
    }


   
}