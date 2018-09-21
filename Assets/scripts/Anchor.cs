using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour {
    public bool Holding = false;
    public Transform HoldingBlock;
    public float MaxForce;
    private SpringJoint2D SpringJoint;
    private Transform CharaterTransform;
    public int idcontroller;
    private Vector2 Offset;
    private Rigidbody2D body;
    private Transform StartTransform;

    // Cube Rotation
    public Quaternion origianl_rotation;
    public float rotation = 0;
    private float speed_rotation = 10f;
    // Use this for initialization
    void Start () {
        SpringJoint = GetComponent<SpringJoint2D>();
        CharaterTransform = transform.parent.GetChild(0);
        idcontroller = CharaterTransform.GetComponent<CharacterController>().controllerId;
        Offset = transform.position - CharaterTransform.position;
        body = GetComponent<Rigidbody2D>();
        SpringJoint = GetComponent<SpringJoint2D>();
        StartTransform = transform;

        //CubeRotation
        origianl_rotation = transform.rotation;
    }
    // Use this for initialization

    
	
	// Update is called once per frame
	void Update () {
        if (Holding)
        {
            body.bodyType = RigidbodyType2D.Dynamic;
            if (HoldingBlock.GetComponent<FixedJoint2D>() == null)
            {
                Holding = false;
                HoldingBlock = null;
            }
        }
        else
        {
            body.bodyType = RigidbodyType2D.Kinematic;
            body.velocity = new Vector2(0f, 0f);
            Vector2 pos = CharaterTransform.position;
            if(CharaterTransform.GetComponent<CharacterController>().isFacingRight)
                transform.position = new Vector2(CharaterTransform.position.x + Offset.x, CharaterTransform.position.y + Offset.y);
            else
                transform.position = new Vector2(CharaterTransform.position.x + -Offset.x, CharaterTransform.position.y + Offset.y);
            transform.localRotation = Quaternion.identity;
        }

        // CUBE ROTATION 
        if (Input.GetButtonDown("LB_"+idcontroller))
        {
            rotation -= 90;
        }if (Input.GetButtonDown("RB_" + idcontroller))
        {
            rotation += 90;
        }
        if (Mathf.Abs(transform.rotation.z - rotation) > 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, origianl_rotation * Quaternion.Euler(0, 0, rotation), speed_rotation * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        //Debug.Log(HoldingBlock.GetComponent<FixedJoint2D>().reactionForce.magnitude);
        /*FixedJoint2D Joint = HoldingBlock.GetComponent<FixedJoint2D>();
        if (Joint.reactionForce.magnitude > MaxForce)
            Destroy(Joint.GetComponent<FixedJoint>();*/
    }

    public void Drop()
    {
        if (Holding)
        {
            Transform folder = GameObject.Find("BlocksFolder").transform;
            HoldingBlock.parent = folder;
            HoldingBlock.tag = "flying";
            Destroy(HoldingBlock.gameObject.GetComponent<FixedJoint2D>());
            Rigidbody2D BlockBody = HoldingBlock.GetComponent<Rigidbody2D>();
            BlockBody.mass = BlockBody.mass * 2;
            Holding = false;
            HoldingBlock = null;
            //Cube Rotation
            rotation = 0;
        }
    }

    public void AnchorBlock()
    {
        FixedJoint2D joint = HoldingBlock.gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = GetComponent<Rigidbody2D>();
        joint.breakForce = MaxForce;
    }
}
