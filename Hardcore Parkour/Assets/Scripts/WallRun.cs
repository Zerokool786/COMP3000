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


    [Header("ClimbJump")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;

    public KeyCode jumpKey = KeyCode.Space;

    public int climbJumps;
    private int climbJumpsLeft;


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

    private Transform lastWall;
    private Vector3 lastWallNormal;
    
    //min amount for wall normal for it to change 
    public float minWallNormalAngleChange;

   

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

        //check conditional front wall hit size is different from from the last wall OR if the wall normal has changed then compare the current normal wall and the last one
        bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWallNormalAngleChange;
        

        //if exists a new wall in front of the player and the player hits it OR the player is grounded
        if ((wallFront && newWall) || movePlayer.isGrounded)
        {
            //reset climb timer
            climbTimer = maxClimbTime;
            
            //reset climbJumpsLeft
            climbJumpsLeft = climbJumps;
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

        if (wallFront && Input.GetKeyDown(jumpKey) && climbJumpsLeft > 0) ClimbJump();
    
    
    }

    private void BeginClimbing()
    {
        climbing = true;

        //store transform of current wall
        lastWall = frontWallHit.transform;
        lastWallNormal = frontWallHit.normal;
    }


    private void ClimbMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z); //set Y velocity of the player to climb speed

    }

    private void StopClimbing()
    {
        climbing = false;
    }

    private void ClimbJump()
    {
        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        climbJumpsLeft--;
    }

}
