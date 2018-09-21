using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class TutoNiv1 : MonoBehaviour {

    private bool active = true;
    public int firstid;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        //firstid = GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GetPlayerByControllerId(GameObject.Find("PlayerManager").transform.GetChild(0).GetComponent<Player>().ControllerId);
        firstid = GameObject.Find("PlayerManager").transform.GetChild(0).GetComponent<Player>().ControllerId;
    }
	
	// Update is called once per frame
	void Update () {

        if (active && (Input.GetButtonDown("A_"+firstid.ToString()) || Input.GetButtonDown("X_" + firstid.ToString())))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

	}
}
