using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {

    private Transform MyT;
    private SpriteRenderer MyR;
    private bool isFacingRight = true;
    private SpringJoint2D Spring;

    private void Start()
    {
        MyR = gameObject.GetComponent<SpriteRenderer>();
        Spring = transform.parent.GetChild(1).GetComponent<SpringJoint2D>();
    }

    void Update () {

        MyT = gameObject.transform;

        if (MyT.position.x > 0 && isFacingRight) {
            isFacingRight = false;
            MyR.flipX = true;
            Spring.connectedAnchor *= -1;
        }
        if (MyT.position.x < 0 && !isFacingRight) {
            isFacingRight = true;
            MyR.flipX = false;
            Spring.connectedAnchor *= -1;
        }
    }
}
