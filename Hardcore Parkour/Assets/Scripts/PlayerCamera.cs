using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation; //character 

    //camera rotation
    float rotX; 
    float rotY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   //cursor center alignment 
        Cursor.visible = false; 
    }


    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; //gets mouse input
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        
        rotY += mouseX;
        rotX += mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 90f); //range of vertical look

        transform.rotation = Quaternion.Euler(rotX, rotY, 0);      //camera rotation for X and Y 
        orientation.rotation = Quaternion.Euler(0, rotY, 0);       //character rotation along Y 

    }
}
