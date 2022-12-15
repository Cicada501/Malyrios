using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] Joystick joystick;
    private float horizontalMove = 0f;
    [SerializeField] private float runSpeed;
    [SerializeField] Animator playerAnimator = null;
    private bool jump = false;
    private bool jumpInput;
    private Rigidbody2D rb;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsBetween(joystick.Horizontal, -1f, -0.3f))
        {
            horizontalMove = -1f*runSpeed;
        }else if (IsBetween(joystick.Horizontal,-0.3f,-0.1f))
        {
            horizontalMove = -0.3f*runSpeed;
        }else if (IsBetween(joystick.Horizontal,-0.1f,0.1f))
        {
            horizontalMove = 0;
        }else if (IsBetween(joystick.Horizontal,0.1f,0.3f))
        {
            horizontalMove = 0.3f*runSpeed;
        }
        else
        {
            horizontalMove = 1f*runSpeed;
        }
    }

    bool IsBetween(float source, float min, float max)
    {
        if (source >= min && source < max) return true;
        return false;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;


        //Animation
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        playerAnimator.SetFloat("YVelocity", rb.velocity.y);
    }

    public void OnLanding()
    {
        if (rb.velocity.y < -0.1)
        {
            playerAnimator.SetBool("isJumping", false);
        }
    }

    //called when jump button pressed
    public void JumpButtonPressed()
    {
        jump = true;
        jumpInput = true;
        playerAnimator.SetTrigger("Jump");
        playerAnimator.SetBool("isJumping", true);
        print("JumpButtonPressed");
    }

    public void JumpButtonReleased()
    {
        jumpInput = false;
    }
}