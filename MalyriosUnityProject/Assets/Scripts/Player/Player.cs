using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI positionText;
    [SerializeField] AudioSource jumpStart = null;
    [SerializeField] AudioSource landing2 = null;
    [SerializeField] LayerMask whatIsGround = 0;
    [SerializeField] ParticleSystem dust = null;
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] Animator playerAnimator = null;
    [SerializeField] Animator cameraAnimator = null;
    [SerializeField] Transform feetPos = null;
    [SerializeField] Joystick joystick = null;
    [SerializeField] float checkRadius = 0;
    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] float climbingSpeed = 1f;
    [SerializeField] float jumpTime = 0;

    public static bool isGrunded;
    public static bool isFalling = false;
    public static bool attackInput;
    public static bool interactInput;
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


    void FixedUpdate()
    {
        

        
    }
    
    
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