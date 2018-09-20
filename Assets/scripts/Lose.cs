using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour {

    public GameObject Lose_line;
    public float vitesse;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Collider2D[] colll2 = Physics2D.OverlapAreaAll(new Vector2(-8.75f, Lose_line.transform.position.y), new Vector2(-1.2f, Lose_line.transform.position.y)); // Le -5.5f il faut qui bouge. 
        int contador = 0;
        for (int i = 0; i < colll2.Length; i++)
        {
            if (colll2[i].transform.tag == "Contruction_Objects")
            {
                if (colll2[i].GetComponent<Rigidbody2D>().IsSleeping() )
                {
                    contador++;
                }
            }else if (colll2[i].transform.tag == "Ground")
            {
                contador++;
            }
            
        }
        if (contador == 0)
        {
            Debug.Log("Lose");
                
        }

        float step = vitesse * Time.deltaTime;
        transform.position = transform.position + (new Vector3(0, 1, 0) * step);//Vector3.MoveTowards(transform.position, transform., step);

    }
}
