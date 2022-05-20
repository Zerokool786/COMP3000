using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("References")]
    public playerMovement movePlayer;
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;


    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("Wall Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;

    private bool wallFront;

    private void Update()
    {
        StateMachine();
        WallCheck();

        if (climbing) ClimbMovement();
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
        
        if (movePlayer.isGrounded)
        {
            //reset climb timer if player is grounded
            climbTimer = maxClimbTime; 
        }
    }

    private void StateMachine()
    {

            //Wall Jump
            //if the key is pressed and the look angle is less than the maximum look angle
            if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle)
        {

            //if not climbing and have climb timer remaining call begin climbing
            if (!climbing && climbTimer > 0) BeginClimbing(); 

            //if countdown is above zero start climbing 
            if (climbTimer > 0) climbTimer -= Time.deltaTime;

            //if countdown is below zero stop climbing
            if (climbTimer < 0) StopClimbing();

        }

            else
        
        {
            if (climbing) StopClimbing(); //if player is not climbing stop all active climbs
        
        }
    
    
    }

    private void BeginClimbing()
    {
        climbing = true;
    }


    private void ClimbMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z); //set Y velocity of the player to climb speed
    }

    private void StopClimbing()
    {
        climbing = false;
    }

}
