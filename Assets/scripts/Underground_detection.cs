using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underground_detection : MonoBehaviour {

    public GameObject GameOver_Line;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Contruction_Objects")
        {
            GameOver_Line.GetComponent<Lose>().vitesse = 1;
            StartCoroutine(reset_speed());

        }
    }

    IEnumerator reset_speed()
    {
        yield return new WaitForSeconds(2);
        GameOver_Line.GetComponent<Lose>().vitesse = 0.1f;

    }
}
