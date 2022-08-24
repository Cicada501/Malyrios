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
    [SerializeField] float verticalSpeed = 1f;
    [SerializeField] float jumpTime = 0;

    public static bool isGrunded;
    public static bool isFalling = false;
    public static bool attackInput;
    public static bool interactInput;
    public static bool inventoryInput;
    public static bool ability1_Input;

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
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        print("Speed: " + speed);
        //Cant move while attacking
        if (PlayerAttack.isAttacking)
        {
            horizontal = 0;
        }
        //Apply movement to player
        else
        {
            // Get input to variables
            if (joystick.Horizontal < -0.4f)
            {
                horizontal = -1f;
            }
            else if (joystick.Horizontal > 0.4f)
            {
                horizontal = 1f;
            }
            else if (joystick.Horizontal < -0.2f && joystick.Horizontal >= -0.4f)
            {
                horizontal = -0.5f;
            }
            else if (joystick.Horizontal > 0.2f && joystick.Horizontal <= 0.4f)
            {
                horizontal = 0.5f;
            }
            else
            {
                horizontal = 0f;
            }

            verticalInput = joystick.Vertical;

            speed = Mathf.Abs(horizontal);
        }

        //Climbing on Ladder
        if (ladder.verticalMovementEnabled && verticalInput != 0)
        {
            isClimbing = true;
        }
        else isClimbing = false;

        //apply input to player (moveing left, right, on ladder up and down)
        if (isClimbing)
        {
            rb.velocity = new Vector2(horizontal * horizontalSpeed * 0.2f, verticalInput * verticalSpeed);
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
        playerAnimator.SetBool("isJumping", isJumping);
        playerAnimator.SetFloat("YVelocity", rb.velocity.y);
    }

    void Update() //#######################################################################################
    {
        //Get shorter input variable names
        attackInput = ButtonScript.receivedAttackInput;
        dodgeInput = ButtonScript.receivedDodgeInput;
        jumpInput = ButtonScript.receivedJumpInput;
        interactInput = ButtonScript.receivedInteractInput;
        inventoryInput = ButtonScript.receivedOpenInventoryInput;
        ability1_Input = ButtonScript.receivedAbility1_input;

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
            isJumping = false;
            isFalling = true;
        }
        else if (rb.velocity.y > -0.01)
        {
            isFalling = false;
        }

        cameraAnimator.ResetTrigger("Landing");

        //Landing
        if (isFalling && isGrunded)
        {
            isFalling = false;
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
    } //########################################################################
    //#########################################################################


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