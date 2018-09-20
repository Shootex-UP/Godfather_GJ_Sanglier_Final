using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Principal_Controls : MonoBehaviour {

    public GameObject back_menu;
    public GameObject tint;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("??");
        gameObject.transform.GetChild(2).GetComponent<Selectable>().Select();

        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Debug.Log("B!");
            gameObject.SetActive(false);
            back_menu.SetActive(true);
            back_menu.transform.GetChild(1).transform.GetChild(1).GetComponent<Selectable>().Select();
            tint.SetActive(false);
        }
    }
}
