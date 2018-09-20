using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{

    private Collider2D tmp_collider2D; // il faut faire un Array

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colll = Physics2D.OverlapAreaAll(new Vector2(-9, 1.5f), new Vector2(-1.5f, 5f));
        for (int i = 0; i < colll.Length; i++)
        {
            if(colll[i].transform.tag == "Contruction_Objects")
            {
                if (colll[i].GetComponent<Rigidbody2D>().IsSleeping())
                {
                    StartCoroutine(Check_Cons_Obj(colll[i]));
                }
            }
        }

        
    }

    IEnumerator Check_Cons_Obj(Collider2D other)
    {
        yield return new WaitForSeconds(3f);
        if (other.GetComponent<Rigidbody2D>().IsSleeping()) { // Il faut check a chaque FRAME!!
            Debug.Log("Win");
        }
    }

}
