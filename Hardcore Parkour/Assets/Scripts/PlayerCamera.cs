using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation; //character 
    public Transform playerBody;

    //camera rotation
    float rotX; 
    float rotY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   //cursor center alignment 
        Cursor.visible = false; 
    }


    private void Update() //input function
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; //gets mouse input
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        
        rotY += mouseX;
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 56f); //range of vertical look

        transform.rotation = Quaternion.Euler(rotX, rotY, 0f);      //camera rotation for X and Y 
        orientation.rotation = Quaternion.Euler(0f, rotY, 0f);       //character rotation along Y 

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
