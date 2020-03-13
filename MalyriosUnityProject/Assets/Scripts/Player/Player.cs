using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    bool facingRight = true;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator playerAnimator;

    [SerializeField]
    Animator backgroundAnimator;
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

    bool landing = false;

    //BackJump with Q
    bool usingPush = false;
    float usedBackJumpAt;
    public float backJumpRate= 0.5f;



    // Use this for initialization
    void Start()
    {

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
            // Get input to variables
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            speed = Mathf.Abs(horizontal);


        }

        //Climbing on Ladder
        if (ladder.verticalMovementEnabled && Input.GetAxis("Vertical") != 0)
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
            if (Input.GetAxis("Horizontal") != 0 && !usingPush)
            {
                rb.velocity = new Vector2(horizontal * horizontalSpeed, rb.velocity.y);



            }
            else if (Input.GetAxis("Horizontal") == 0 && !usingPush)
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



    }
    void ResetVelocity()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        usingPush = false;
    }
    void BackJump(bool facingRight)
    {
        usingPush = true;
        if (facingRight)
        {
            rb.AddForce(new Vector2(-30f, 0f));
        }
        else
        {
            rb.AddForce(new Vector2(30f, 0f));
        }
        rb.AddForce(new Vector2(0f, 18f));
        Invoke("ResetVelocity", 0.2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time-usedBackJumpAt >backJumpRate)
        {
            BackJump(facingRight);
            usedBackJumpAt = Time.time;
        }



        //check if player is on the Ground
        isGrunded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //Jump
        if (isGrunded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrunded = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }


        //Check if player is falling for the animation
        if (rb.velocity.y < -0.01 && !(isGrunded) && !isClimbing)
        {
            if (!isFalling)
            {
                isFalling = true;
            }

        }
        else if ((isGrunded))
        {
            isFalling = false;
            //startfallingTime = 0;
        }






        //Longer Jump on Space holding
        if (Input.GetKey(KeyCode.Space) && isJumping)
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


    }

    void flip()
    {

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

    }

    public void GiveSuperJumps()
    {
        jumpForce = 2.3f;
    }
}
