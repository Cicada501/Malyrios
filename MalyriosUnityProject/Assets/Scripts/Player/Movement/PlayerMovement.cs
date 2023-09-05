using System.Collections;
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
    public bool disableMovement;

    //Dashing
    [SerializeField] private float dashingVelocity = 2f;
    [SerializeField] private float dashingTime = 0.8f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private TrailRenderer trailRenderer;
    private bool dashInput;
    private float lastImageXpos;
    private float distanceBetweenImages = 0.1f;
    
    //Sound
    [SerializeField] private AudioSource[] landingSounds;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource dashingSound;
    private AudioSource[] runSound;
    [SerializeField] private AudioSource[] runSoundStone;
    [SerializeField] private AudioSource[] runSoundGrass;
    
    private bool isRunning;
    private float soundPlayTime;
    private float soundPlayInterval = 0.3f;

    void Start()
    {
        runSound = runSoundGrass;
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        controller.OnLandEvent.AddListener(PlayLandingSound);

    }

    void Update()
    {
        if (!disableMovement)
        {
            if (IsBetween(joystick.Horizontal, -1f, -0.3f))
            {
                horizontalMove = -1f * runSpeed;
            }
            else if (IsBetween(joystick.Horizontal, -0.3f, -0.1f))
            {
                horizontalMove = -0.3f * runSpeed;
            }
            else if (IsBetween(joystick.Horizontal, -0.1f, 0.1f))
            {
                horizontalMove = 0;
            }
            else if (IsBetween(joystick.Horizontal, 0.1f, 0.3f))
            {
                horizontalMove = 0.3f * runSpeed;
            }
            else
            {
                horizontalMove = 1f * runSpeed;
            }
        }

        #region dash
        IEnumerator FreezeAndDash()
        {
            // Freeze player's position
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            disableMovement = true;
            
            yield return new WaitForSeconds(0.15f);
            
            //remove freeze again
            disableMovement = false;
            rb.gravityScale = 1;

            // Begin dashing
            isDashing = true;
            trailRenderer.emitting = true;
            dashingDir = joystick.Direction;
            //camAnimator.SetTrigger("Dash");
            playerAnimator.SetTrigger("Dash");
            dashingDir = new Vector2(transform.localScale.x, 0f); //only allow horizontal dashing
            CameraShake.Instance.ShakeCamera(duration:0.2f,intensity:0.05f);
            StartCoroutine(StopDashing());
        }
        
        
        if (dashInput && canDash)
        {
            canDash = false;
            StartCoroutine(FreezeAndDash());
            dashingSound.Play();
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
    
            //Spawn after images
            if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
            {
                PlayerAfterImagePool.Instance.GetFromPool();
                lastImageXpos = transform.position.x;
            }
        }


        if (controller.m_Grounded && !isDashing)
        {
            canDash = true;
        }
        #endregion
        
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
        
        if (Mathf.Abs(horizontalMove) > 0 && controller.m_Grounded && !isDashing && Time.time >= soundPlayTime)
        {
            if (!isRunning)
            {
                isRunning = true;
                PlayRunSound();
            }

            soundPlayTime = Time.time + soundPlayInterval;
        }
        else
        {
            isRunning = false;
        }
    }

    //called when jump button pressed
    public void JumpButtonPressed()
    {
        if (isJumping || disableMovement) return;
        jump = true; //triggers the addforce next frame
        jumpingSound.Play();
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

    void PlayLandingSound()
    {
        //print("LandingSound");
        int randomIndex = Random.Range(0, landingSounds.Length);
        landingSounds[randomIndex].Play();
    }
    
    void PlayRunSound()
    {
        // Wenn der Spieler rennt, spiele einen zufälligen Laufsound ab
        int randomIndex = Random.Range(0, runSound.Length);
        runSound[randomIndex].Play();
    }
    
    private void OnDestroy()
    {
        controller.OnLandEvent.RemoveListener(PlayLandingSound);
    }

    public void ChangeRunSound(string groundType)
    {
        print($"run sound changing to {groundType}");
        switch (groundType)
        {
            case "grass":
            case "Grass":
                runSound = runSoundGrass;
                break;
            case "stone": 
            case "Stone":
                runSound = runSoundStone;
                break;
        }
    }
}