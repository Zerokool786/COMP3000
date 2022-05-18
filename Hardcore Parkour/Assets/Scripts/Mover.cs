using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public AnimationCurve directionCurve;
    public float Scale;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * directionCurve.Evaluate(Mathf.Repeat(Time.time, 0.9f)) * Time.deltaTime * Scale; // value returned by the animcurve and scale to move object up and down
    }
}
