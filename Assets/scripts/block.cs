using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class block : MonoBehaviour {
    public List<Collider2D> ObjectColliding;

    private bool first_to_land = false;
    public GameObject Barre;
	// Use this for initialization
	void Start () {
        Barre = GameObject.Find("Barre Fluide");
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
        {
            transform.tag = "landed";
            if (!first_to_land)
            {
                first_to_land = true;
                Debug.Log("Cube posé");
                //Barre.GetComponent<Image>().fillAmount += 0.05f;
                if(SceneManager.GetActiveScene().name == "Niveau_2")
                if (Barre.transform.parent.gameObject.activeSelf)
                {
                    Barre.GetComponent<barre_super>().remplisage();
                }
            }
        }
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
