using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour {
    public List<Collider2D> ObjectColliding;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int i = 0;
        foreach (var item in ObjectColliding)
        {
            if (item.tag == "landed" || item.tag == "Ground")
                i++;
        }
        if (i > 0)
            transform.tag = "landed";
        else
            transform.tag = "flying";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            transform.tag = "landed";
        if (collision.transform.tag == "landed" || collision.transform.tag == "Ground")
        {
            ObjectColliding.Add(collision.collider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "landed")
        {
            ObjectColliding.Remove(collision.collider);
        }
    }
}
