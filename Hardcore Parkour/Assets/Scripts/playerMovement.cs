using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]

    public Transform orientation;

    [Header("Ground Check")] //check if player is grounded
    public float playerHeight;
    public LayerMask whatIsGround; //a layermask for whatIsGround tag 
    bool grounded;                 //conditional

    public float groundDrag;

    public float moveSpeed;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.2f + 0.5f, whatIsGround);  //perform ground check 
        keyInput();

        //apply the drag
        if (grounded)
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
    }

    private void movePlayer()
    {
        //walking in the direcion relative to where player is looking
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        //add force to the rigidbody 
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
}
