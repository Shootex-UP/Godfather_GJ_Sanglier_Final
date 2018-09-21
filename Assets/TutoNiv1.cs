using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoNiv1 : MonoBehaviour {

    private bool active = true;
    private bool canSkip = false;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (active && Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            gameObject.SetActive(false);
            active = false;
            Time.timeScale = 1;
        }

	}
}
