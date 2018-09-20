using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_test : MonoBehaviour {
    public float MaxSpeed = 1;
    public float accelerationSpeed = 1;
    public float BrakeSpeed = 2;
    public int controllerId;
    private Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        try
        {
            controllerId = transform.parent.GetComponent<Manager.Player>().GetControllerId();
        }
        catch
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
        //try
        //{
        Vector2 Direction = new Vector2(Input.GetAxis("L_XAxis_" + controllerId.ToString()), -Input.GetAxis("L_YAxis_" + controllerId.ToString()));

        //body.velocity = new Vector2(, -Input.GetAxis("L_YAxis_" + controllerId.ToString())) * SpeedMovement;

        if (Direction.magnitude > 0)
        {
            if (body.velocity.magnitude > MaxSpeed)
            {
                body.velocity = body.velocity.normalized * MaxSpeed;
            }
            else
            {
                //float HorizontalAxis = Input.GetAxis("L_XAxis_" + controllerId.ToString()) * (accelerationSpeed * Time.fixedDeltaTime);
                //float VerticalAxis = -Input.GetAxis("L_YAxis_" + controllerId.ToString()) * (accelerationSpeed * Time.fixedDeltaTime);
                //Debug.Log(VerticalAxis);

                Direction *= accelerationSpeed;
                //Debug.Log(Direction.ToString());
                //body.velocity += Direction;
                body.AddForce(Direction);
                //Debug.Log(body.velocity.magnitude+"/"+MaxSpeed);
            }
        }
        else
        {
            Vector2 BrakeVector = -body.velocity.normalized * (BrakeSpeed * Time.deltaTime);
            if (body.velocity.magnitude < BrakeVector.magnitude)
                //body.velocity = new Vector2(0f, 0f);
                body.velocity = new Vector2(0f, 0f);
            else
                body.AddForce(BrakeVector);
            //body.velocity += BrakeVector;

        }
            

            if (Input.GetButtonDown("A_" + controllerId.ToString()) || Input.GetButtonDown("B_" + controllerId.ToString()))
            {
                transform.parent.GetChild(1).GetComponent<Anchor>().Drop();
            }
        /*}
        catch
        {
            //body.velocity = new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")) * SpeedMovement;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.parent.GetChild(1).GetComponent<Anchor>().Drop();
            }
        }*/
    }
}
