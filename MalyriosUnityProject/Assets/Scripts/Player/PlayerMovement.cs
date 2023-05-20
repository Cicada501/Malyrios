using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
//https://www.youtube.com/watch?v=dwcT-Dch0bA
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] Joystick joystick;
    private float horizontalMove = 0f;
    [SerializeField] private float runSpeed;
    [SerializeField] Animator playerAnimator = null;
    private bool jump;
    private bool isJumping;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius;
    public bool disableMovement;
    private Queue<bool> wasGrounded = new Queue<bool>(5);  // Queue to store last 5 grounded states

    //Dashing
    [SerializeField] private float dashingVelocity = 3f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float maxDashUpAngle = 20f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer trailRenderer;
    private bool dashInput;

    private Animator camAnimator;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        camAnimator = ReferencesManager.Instance.camera.GetComponent<Animator>();
        for (int i = 0; i < 5; i++)  // Initialize the Queue with 5 entries of "false"
        {
            wasGrounded.Enqueue(false);
        }

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

        
        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = joystick.Direction;
            camAnimator.SetTrigger("Landing");
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0f);
            }else
            {
                //limit dash angle, if it goes upwards to much
                float angle = Vector2.Angle(Vector2.right, dashingDir);
                if (dashingDir.y > 0 && angle > maxDashUpAngle)
                {
                    float rad = Mathf.Deg2Rad * maxDashUpAngle;
                    dashingDir = new Vector2(Mathf.Cos(rad)*transform.localScale.x, Mathf.Sin(rad)) * dashingDir.magnitude;
                }
            }
            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }
        
        if(controller.m_Grounded)
        {
            canDash = true;
        }

    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
    }



    bool IsBetween(float source, float min, float max)
    {
        if (source >= min && source < max) return true;
        return false;
    }
    
    private void FixedUpdate()
    {
        // Ground check
        isJumping = !Physics2D.OverlapCircle(groundCheckPoint.position, controller.k_GroundedRadius, groundLayer);
        if (!disableMovement)
        {
            //if jump gets true call function once with jump=true, with that AddForce() will get called 
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }

        //Animation
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        playerAnimator.SetFloat("YVelocity", rb.velocity.y);

        //Landing Moment
        var groundedStatesArray = wasGrounded.ToArray();
        if (groundedStatesArray[0] == false && groundedStatesArray[1] == false && groundedStatesArray[2] == false 
            && groundedStatesArray[3] == true && groundedStatesArray[4] == true)
        {
            Landing();
        }
        
        if (wasGrounded.Count >= 5)
        {
            wasGrounded.Dequeue();
        }
        wasGrounded.Enqueue(controller.m_Grounded);
    }

    void Landing()
    {
        camAnimator.SetTrigger("Landing");
    }

    //called when jump button pressed
    public void JumpButtonPressed()
    {
        if(isJumping) return;
        jump = true; //triggers the addforce next frame
        //isJumping = true;
        playerAnimator.SetTrigger("Jump");
        playerAnimator.SetBool("isJumping", isJumping);
    }

    public void JumpButtonReleased()
    {
    }

    public void DashButtonPressed()
    {
        dashInput = true;
    }
    public void DashButtonReleased()
    {
        dashInput = false;
    }
    
    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}