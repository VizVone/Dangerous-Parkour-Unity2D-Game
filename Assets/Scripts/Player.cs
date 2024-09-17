using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public bool playerUnlocked;
    public bool isDead;

    // Background Horizontal Scroll
    [SerializeField] private Renderer bgRenderer;
    public float speed;
   
    [Header("Speed Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMultiplier;
    [Space]
    [SerializeField] private float milestoneIncreaser;
    private float speedMilestone;

    [Header("Slide Info")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float sliderTimer;
    private float slideTimerCounter;
    private bool isSliding;


    [Header("Jump Info")]
    [SerializeField] private float jumpForce;
    private bool canDoubleJump;
    [SerializeField] private float doubleJumpForce;
    private float defaultJumpForce;


    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    private bool isGrounded;
    private bool wallDetected;
    [HideInInspector] public bool ledgeDetected;

    // Start is called before the first frame update
    void Start()
    {
        
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speedMilestone = milestoneIncreaser;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();

        AnimatorController();

        slideTimerCounter = slideTimerCounter - Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    StartCoroutine(Die());  
        //}

        if (playerUnlocked && !wallDetected)
        {
            
            Movement();
        }
        

        SpeedController();

        CheckInput();
            
        CheckForSlide();
    }

    public void Damage()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);
        playerUnlocked = false;
        yield return new WaitForSeconds(.5f);
        // yield return new WaitForSeconds(1f);
        GameManager.Instance.RestartLevel();
    }

    private void SpeedController()
    {
        if (moveSpeed == maxSpeed)
        {
            return;
        }

        if (transform.position.x > speedMilestone)
        {
            speedMilestone = speedMilestone + milestoneIncreaser;
            moveSpeed = moveSpeed * speedMultiplier;
            // milestoneIncreaser = milestoneIncreaser * speedMultiplier;
            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
    }
    private void CheckForSlide()
    {
        if (slideTimerCounter < 0)
        {
            isSliding = false; 
        }
    }

    private void Movement()
    {

        // Background Code
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);

        // Movement Code
        if (isSliding)
        {
            rb.velocity = new Vector2(slideSpeed,rb.velocity.y);
            
        }
        else
        {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
    }

    private void AnimatorController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("canDoubleJump", canDoubleJump);
        anim.SetBool("isSliding", isSliding);

    }

    private void CheckCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatIsGround);
    }

    private void CheckInput()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    playerUnlocked = true;
        //}

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            JumpButton();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SlideButton();
        }


    }

    public void SlideButton()
    {
        if (isGrounded == true)
        {
        isSliding = true;
        slideTimerCounter = sliderTimer;

        }
    }

    public void JumpButton()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (canDoubleJump)
        {
            canDoubleJump= false;
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
        }
    }
}
