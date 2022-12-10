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
        horizontalMove = joystick.Horizontal * runSpeed;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        

        //Animation
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        playerAnimator.SetBool("isFalling", rb.velocity.y < -0.1);
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