using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    public AudioSource source;
    public AudioClip clip;
    //public AudioSource jumpSource;
    //public AudioClip jumpClip;
    public AudioClip musicStart;
    public AudioSource musicSource;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        Debug.Log(animator);

        musicSource.PlayOneShot(musicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + clip.length);

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
            //source.PlayOneShot(clip);
            



        }

        if (isWalking && !forwardPressed) //player is not pressing w key
        {
            animator.SetBool(isWalkingHash, false);  //set isWalking to false
            //source.Stop();
            
        }

        //if player is walking and not running and presses left shift
        if (!isrunning && (forwardPressed && runPressed)) 
        {
            animator.SetBool(isRunningHash, true); //set the isRunning bool to true
            source.PlayOneShot(clip);
            source.PlayScheduled(AudioSettings.dspTime + clip.length);

        }

        //if player is running and stops running or stops walking
        if (isrunning && (!forwardPressed || !runPressed)) 
        {
            animator.SetBool(isRunningHash, false); //set the isRunning to false
            source.Stop();

        }

        if (Input.GetKey("space"))
        {
            animator.SetBool("isJumping", true);
            
        }

        if (!Input.GetKey("space"))
        {
            animator.SetBool("isJumping", false);
            
        }

        if (Input.GetKey("left ctrl"))
        {
            animator.SetBool("isCrouching", true);
        }

        if(!Input.GetKey("left ctrl"))
        {
            animator.SetBool("isCrouching", false);
        }

        if (Input.GetKey("d"))
        {
            animator.SetBool("isRunningRight", true);
        }

        if (!Input.GetKey("d"))
        {
            animator.SetBool("isRunningRight", false);
        }

        if (Input.GetKey("a"))
        {
            animator.SetBool("isRunningLeft", true);
            

        }

        if (!Input.GetKey("a"))
        {
            animator.SetBool("isRunningLeft", false);
            
        }

        if (Input.GetKey("a"))
        {
            animator.SetBool("isWalkingLeft", true);
        }

        if (!Input.GetKey("a"))
        {
            animator.SetBool("isWalkingLeft", false);
        }

        if (Input.GetKey("d"))
        {
            animator.SetBool("isWalkingRight", true);
        }

        if (!Input.GetKey("d"))
        {
            animator.SetBool("isWalkingRight", false);
        }
    }
}
