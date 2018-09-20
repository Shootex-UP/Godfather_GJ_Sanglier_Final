using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorsZone : MonoBehaviour {

    private float t;
    private Vector3 startPosition;
    public Transform target;
    private Vector3 targetPos;
    public float timeToReachTarget;
    public int BlockLost = 0;
    public float AddBoostTime = 1;//Add boost Time for each block lost
    public float SpeedBoost = 1;//
    public float TimeBoost = 0;//

    public bool Detected;
    private bool Lose = false;
    void Start()
    {
        startPosition = transform.position;
        targetPos = target.position;
    }
    void FixedUpdate()
    {
        //Boost Mechanics
        if (TimeBoost > 0)
        {
            t += Time.deltaTime / (timeToReachTarget/SpeedBoost);
            //
            TimeBoost -= Time.deltaTime;
            if (TimeBoost < 0)
                TimeBoost = 0;
        }
        else
        {
            t += Time.deltaTime / timeToReachTarget;
        }
        transform.position = Vector3.Lerp(startPosition, targetPos, t);

        
        Collider2D[] ObjList = Physics2D.OverlapBoxAll(transform.position, new Vector2(transform.localScale.x, transform.localScale.y), 0);
        int id = 0;
        Detected = false;
        while (id < ObjList.Length && !Detected)
        {
            if (ObjList[id].transform.tag == "landed" || ObjList[id].transform.tag == "Ground")
            {
                Detected = true;
            }
            id++;
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("stay");
        BlockInTrigger = false;
        if (other.tag == "landed" || other.tag == "Ground")
        {
            BlockInTrigger = true;
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.tag == "landed" || collision.tag == "Ground")&& !Detected)
            GameObject.Find("GameManager").GetComponent<Manager.GameManager>().Loose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "flying")
        {
            TimeBoost += AddBoostTime;
            BlockLost++;
            collision.transform.tag = "lost";
        }
    }
    /*public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        target = destination;
    }*/
}
