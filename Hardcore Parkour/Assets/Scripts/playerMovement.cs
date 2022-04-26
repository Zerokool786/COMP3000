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

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public float moveSpeed;
    public float groundDrag;


    [Header("Ground Check")] //check if player is grounded
    public float playerHeight;
    public LayerMask whatIsGround; //a layermask for whatIsGround tag 
    
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
        //isJumping = true;

        resetJump();
    }

    private void Update()
    {
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);  //perform ground check 
        keyInput();
        speedController();
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
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //apply force upwards only once
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
        
    }

    private void resetJump()
    {
        isJumping = true;
    }
}
