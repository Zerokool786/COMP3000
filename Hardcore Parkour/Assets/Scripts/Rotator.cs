using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotSpeed = 50f;

 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, rotSpeed * Time.deltaTime);
    }
}
