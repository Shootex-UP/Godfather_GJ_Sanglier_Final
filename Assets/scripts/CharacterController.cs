using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float MaxSpeed = 1;
    public float accelerationSpeed = 1;
    public float BrakeSpeed = 2;
    public int controllerId;
    private Rigidbody2D body;
    public Transform anchor;
    public Anchor anchorScript;
    //
    public float deadZone;
    private Vector2 Direction;
    public float currentSpeed = 0;
    private SpriteRenderer shadow;
    //Flip
    private SpriteRenderer MyR;
    public bool isFacingRight = true;
    private SpringJoint2D Spring;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        controllerId = transform.parent.GetComponent<Manager.Player>().GetControllerId();

        //flip
        MyR = gameObject.GetComponent<SpriteRenderer>();
        shadow = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anchor = transform.parent.GetChild(1);
        anchorScript = anchor.GetComponent<Anchor>();
        Spring = anchor.GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //try
        //{
        Direction = new Vector2(Input.GetAxis("L_XAxis_" + controllerId.ToString()), -Input.GetAxis("L_YAxis_" + controllerId.ToString()));

        //body.velocity = new Vector2(, -Input.GetAxis("L_YAxis_" + controllerId.ToString())) * SpeedMovement;

        if (Direction.magnitude <= deadZone)
        {
            if (currentSpeed < (BrakeSpeed * Time.deltaTime))
            {
                currentSpeed = 0;
            }
            else
                currentSpeed -= (BrakeSpeed * Time.deltaTime);

            Direction = body.velocity.normalized;
        }
        else
        {
            currentSpeed += accelerationSpeed * Time.deltaTime;
        }
        if (currentSpeed > MaxSpeed)
        {
            currentSpeed = MaxSpeed;
        }
        body.velocity = Direction * currentSpeed;
        Debug.DrawRay(transform.position, Direction.normalized, Color.blue);
        Debug.DrawRay(transform.position, body.velocity.normalized, Color.red);

        if (Input.GetButtonDown("A_" + controllerId.ToString()) || Input.GetButtonDown("B_" + controllerId.ToString()))
        {
            transform.parent.GetChild(1).GetComponent<Anchor>().Drop();
        }

        //Flip
        if (transform.position.x > 0 && isFacingRight)
        {
            isFacingRight = false;
            MyR.flipX = true;
            shadow.flipX = true;
            Spring.connectedAnchor = new Vector2(-Spring.connectedAnchor.x, Spring.connectedAnchor.y);
            /*
            if (anchorScript.Holding)
            {
                Spring.transform.position = new Vector2(-Spring.transform.position.x, Spring.transform.position.y);
            }
            */
        }
        if (transform.position.x < 0 && !isFacingRight)
        {
            isFacingRight = true;
            MyR.flipX = false;
            shadow.flipX = false;
            Spring.connectedAnchor = new Vector2(-Spring.connectedAnchor.x, Spring.connectedAnchor.y);
            /*
            if (anchorScript.Holding)
            {
                //Spring.transform.position = new Vector2(-Spring.transform.position.x, Spring.transform.position.y);
            }
            */
            
        }
    }
}
