using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decolage_Object : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space"))
        {
            Destroy(gameObject.GetComponent<SpringJoint2D>());
            gameObject.tag = "Contruction_Objects";
            Debug.Log("Space");
        }

    }
}
