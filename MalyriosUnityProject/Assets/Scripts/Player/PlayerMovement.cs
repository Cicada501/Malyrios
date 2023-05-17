using System;
using System.Collections;
using UnityEngine;

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

    [SerializeField] private float dashingVelocity = 3f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float maxDashUpAngle = 20f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer trailRenderer;
    private bool dashInput;
    float initGravityScale;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
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
            initGravityScale = rb.gravityScale;
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
        isJumping = !Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (!disableMovement)
        {
            //if jump gets true call function once with jump=true, with that AddForce() will get called 
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }

        //Animation
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        playerAnimator.SetFloat("YVelocity", rb.velocity.y);
    }

    //Called in CharacterController Component of Player
    public void OnLanding()
    {
        if (rb.velocity.y < -0.1)
        {
            //isJumping = false;
            playerAnimator.SetBool("isJumping", isJumping);
        }
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