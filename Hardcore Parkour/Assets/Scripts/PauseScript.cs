using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1) //if not paused
                Time.timeScale = 0; // pause the game

            else
                Time.timeScale = 1; //else resume the game
        }
	}
}
