using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public AudioSource jumpStart;
    public AudioSource landing1;
    public AudioSource landing2;

    public ParticleSystem dust;

    [SerializeField]
    bool setAndroidMode;
    public static bool androidMode;
    bool facingRight = true;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator playerAnimator;
    [SerializeField]
    Animator cameraAnimator;

    [SerializeField]
    Rigidbody2D rb;
    float horizontal;
    float vertical;


    [SerializeField]
    float horizontalSpeed = 1f;

    [SerializeField]
    float verticalSpeed = 1f;
    float speed;

    //Variables for Jumping
    public static bool isGrunded;

    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform1;

    public float jumpForce;

    public static bool isFalling = false;
    private bool isJumping = false;
    public float jumpTime;
    private float jumpTimeCounter;


    bool isClimbing = false;


    //BackJump with Q
    bool usingPush = false;
    float usedBackJumpAt;
    public float backJumpRate = 0.5f;

    public Joystick joystick;



    bool jumpInput;
    bool dodgeInput;
    public static bool attackInput;
    public static bool interactInput;
    public static bool inventoryInput;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    // Use this for initialization
    void Start()
    {
        Invoke("PlayerToSpawnPoint", 0.05f);
        //neccecarry to use OnSceneLoaded (otherwise its not called)
        //SceneManager.sceneLoaded += OnSceneLoaded;
        androidMode = setAndroidMode;

        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (PlayerAttack.isAttacking)
        {
            horizontal = 0;
        }
        else
        {
            if (androidMode)
            {
                // Get input to variables
                if (joystick.Horizontal < -0.2f)
                {
                    horizontal = -1f;
                }
                else if (joystick.Horizontal > 0.2f)
                {
                    horizontal = 1f;
                }
                else
                {
                    horizontal = 0f;
                }
                vertical = joystick.Vertical;

            }
            else if (!androidMode)
            {
                joystick.gameObject.SetActive(false);

                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
            }
            speed = Mathf.Abs(horizontal);


        }

        //Climbing on Ladder
        if (ladder.verticalMovementEnabled && vertical != 0)
        {
            isClimbing = true;
        }
        else { isClimbing = false; }
        //apply input to player (moveing left, right, on ladder up and down)
        if (isClimbing)
        {
            rb.velocity = new Vector2(horizontal * horizontalSpeed, vertical * verticalSpeed);

        }
        else
        {
            if (horizontal != 0 && !usingPush)
            {
                rb.velocity = new Vector2(horizontal * horizontalSpeed, rb.velocity.y);



            }
            else if (horizontal == 0 && !usingPush)
            {
                ResetVelocity();
            }

        }



        //face player in the right direction
        if (horizontal > 0 && !facingRight)
        {

            flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            flip();
        }




        //Animation
        playerAnimator.SetFloat("Speed", speed);
        playerAnimator.SetBool("isGrounded", (isGrunded));
        playerAnimator.SetBool("isFalling", isFalling);
        playerAnimator.SetBool("isClimbing", isClimbing);



    }//------------------------------------------------------------------------------------------------
     //----------------------------------------------------------------------------------------------------


    //####################################################################################################
    void Update()//#######################################################################################
    {


        if (androidMode)
        {
            attackInput = ButtonScript.receivedAttackInput;
            dodgeInput = ButtonScript.receivedDodgeInput;
            jumpInput = ButtonScript.receivedJumpInput;
            interactInput = ButtonScript.receivedInteractInput;
            inventoryInput = ButtonScript.receivedOpenInventoryInput;
        }
        else
        {
            inventoryInput = Input.GetKey(KeyCode.I);
            attackInput = Input.GetMouseButtonDown(0);
            dodgeInput = Input.GetKey(KeyCode.Q);
            jumpInput = Input.GetKey(KeyCode.Space);
            interactInput = Input.GetKey(KeyCode.E);
        }


        if (dodgeInput && Time.time - usedBackJumpAt > backJumpRate)
        {
            BackJump(facingRight);
            usedBackJumpAt = Time.time;
        }


        //check if player is on the Ground
        isGrunded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);


        //Jump
        if (isGrunded && jumpInput)
        {
            jumpStart.Play();
            CreateDust();
            isGrunded = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }


        //Check if player is falling for the animation
        if (rb.velocity.y < 0 && !(isGrunded) && !isClimbing)
        {
            if (!isFalling)
            {
                isFalling = true;
            }

            //Catch the moment, when player is falling and groundet => landing
        }
        else if (rb.velocity.y > -0.01)
        {
            isFalling = false;
        }
        cameraAnimator.ResetTrigger("Landing");
        if (isFalling && isGrunded)
        {
            cameraAnimator.SetTrigger("Landing");
            landing2.Play(); 

        }



        //Longer Jump on Space holding
        if (jumpInput && isJumping)
        {

            if (jumpTimeCounter > 0)
            {

                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }


    }//########################################################################
     //#########################################################################






    void PlayerToSpawnPoint()
    {
        transform.position = StaticData.spawnPoint;
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

    public void GiveSuperJumps()
    {

    }

    void CreateDust()
    {
        dust.Play();
    }
}
