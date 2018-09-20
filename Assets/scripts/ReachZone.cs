using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachZone : MonoBehaviour {
    public BlocksManager blockManager;
    void Update()
    {
        /*Collider2D[] ObjList = Physics2D.OverlapBoxAll(transform.position, new Vector2(transform.localScale.x, transform.localScale.y), 0);
        int id = 0;
        bool Detected = false;
        while (id < ObjList.Length && !Detected)
        {
            if (ObjList[id].transform.tag == "landed")
            {
                if(!(ObjList[id].transform.position.y > blockManager.GetHighestValue()))
                {
                    Detected = true;
                }
            }
            id++;
        }
        if (Detected)
        {
            GameObject.Find("GameManager").GetComponent<Manager.GameManager>().Win();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "landed")
        {
            GameObject.Find("GameManager").GetComponent<Manager.GameManager>().Win();
        }
    }
}
