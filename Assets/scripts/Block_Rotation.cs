using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Rotation : MonoBehaviour {

    public Quaternion origianl_rotation;
    public float rotation = 0;
    private float speed_rotation = 10f;


	// Use this for initialization
	void Start () {
        origianl_rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rotation +=90;
        }

        Debug.Log(transform.rotation.z - rotation);

        if (Mathf.Abs(transform.rotation.z - rotation) > 1)
        {
            Debug.Log("Rotating");
            //GetComponent<FixedJoint2D>().enabled = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, origianl_rotation * Quaternion.Euler(0, 0, rotation), speed_rotation * Time.deltaTime);
        }

        */

    }
}
