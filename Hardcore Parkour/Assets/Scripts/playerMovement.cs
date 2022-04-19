using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]

    public Transform orientation;

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
        keyInput();
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
