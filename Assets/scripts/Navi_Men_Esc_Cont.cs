using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navi_Men_Esc_Cont : MonoBehaviour {

    public GameObject back_menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Debug.Log("B!");
            gameObject.SetActive(false);
            back_menu.SetActive(true);
        }
    }
}
