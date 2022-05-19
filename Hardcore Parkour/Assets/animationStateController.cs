using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        bool isrunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        
        if (!isWalking && forwardPressed) //player is pressing w key
        {
            animator.SetBool(isWalkingHash, true); //set isWalking to true
        }

        if (isWalking && !forwardPressed) //player is not pressing w key
        {
            animator.SetBool(isWalkingHash, false);  //set isWalking to false
        }

        //if player is walking and not running and presses left shift
        if (!isrunning && (forwardPressed && runPressed)) 
        {
            animator.SetBool(isRunningHash, true); //set the isRunning bool to true
        }

        //if player is running and stops running or stops walking
        if (isrunning && (!forwardPressed || !runPressed)) 
        {
            animator.SetBool(isRunningHash, false); //set the isRunning to false
        }
    }
}
