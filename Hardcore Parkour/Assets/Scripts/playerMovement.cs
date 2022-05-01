using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    
    bool isJumping;

    public float sprintSpeed;
    public float walkingSpeed;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public float defaultYScale;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    private float moveSpeed;
    public float groundDrag;


    [Header("Ground Check")] //check if player is grounded
    public float playerHeight;
    public LayerMask whatIsGround; //a layermask for whatIsGround tag 


    public movementState currentState;    //stores current state of the player
    public enum movementState
    {
        crouching,
        walking,
        sprinting,
        air

    }
    
    bool isGrounded;               //conditional

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        isJumping = true;
        defaultYScale = transform.localScale.y;  //saves default y scale of the rigidbody

        resetJump();
    }

    private void Update()
    {
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.9f + 0.5f, whatIsGround);  //perform ground check 
        keyInput();
        speedController();
        statesHandler();
        //apply the drag
        if (isGrounded)
            rb.drag = groundDrag;
        // not grounded then put the value to zero
        else                
            rb.drag = 0;
        
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void statesHandler()
    {
        if (Input.GetKey(crouchKey))   ///crouching if left control pressed 
        {
            currentState = movementState.crouching;   //change current state to crouching
            moveSpeed = crouchSpeed;      
        }

        if(isGrounded && Input.GetKey(sprintKey))  //Sprinting if left shift is pressed
        {
            currentState = movementState.sprinting;  //change current state to sprinting
            moveSpeed = sprintSpeed; 
        }

        else if (isGrounded)    //walking if grounded
        {
            currentState = movementState.walking; //change current state to walking
            moveSpeed = walkingSpeed;         
        }

        else      
        {
            currentState = movementState.air;    //player is air if above statements are not executed 
        }
    }
    private void keyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        //check if space is pressed and is jumping and player is grounded
        if (Input.GetKey(jumpKey) && isJumping && isGrounded)  
        {
            isJumping = false;
            
            Jump();

            // reset jump function with a delay and do a continous jump when holding down space key
            Invoke(nameof(resetJump), jumpCooldown);
        }

        //start crouching if left control is pressed 
        if (Input.GetKeyDown(crouchKey))
        {
            //shrink rb downwards to start crouching
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouching by checking if left control key is released
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, defaultYScale, transform.localScale.z);
        }
    }

    private void movePlayer()
    {
        //walking in the direcion relative to where player is looking
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if (isGrounded) // on the ground

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); //add force to the rigidbody 

        else if (!isGrounded)  //if player is in air multiply movement speed with air multiplier 

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void speedController()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit the velocity if required
        if (flatVelocity.magnitude > moveSpeed)   //if player goes faster than movement speed                    
        {
            //estimate the max velocity
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;

            //apply max velocity to the rigidbody
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }


    }
    private void Jump()
    {
        // reset y velocity to zero so to jump the exact same height 
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //apply force upwards only once
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
        
    }

    private void resetJump()
    {
        isJumping = true;
    }
}
