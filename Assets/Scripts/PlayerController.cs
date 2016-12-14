using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public Rigidbody rbody;
    Animator playerAnim;//a reference for the players animations

    public float forwardSpeed;//the move speed of the player
    public float turnSpeed;//how fast the player turns
    float forwardInput;//member variable for the forwardInput
    float turnInput;//member variable for our turnInput
	private float vel = 0f;

	[SerializeField]
	GameObject playerCamera;

	[SerializeField]
	float smoothness;

    public Vector3 MoveVector { get; set; }// this is a carry over from a tutorial i found. it was used in the snap method I wanted to use.

	Quaternion destinationRotation;


	bool turnLeft = false;
	bool turnRight = false;

    // Use this for initialization
    void Awake ()
    {
        playerAnim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();

    }
	// FixedUpdate is for physics interactions 
	void FixedUpdate ()
    {     
        Move();
		Turn();
    }
    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
		if (forwardInput > 0.5f) {
			forwardInput = 1f;
		} else if (forwardInput < -0.5f) {
			forwardInput = -1f;
		} else {
			forwardInput = 0f;
		}
		turnInput = Input.GetAxis("Horizontal");
		if (turnInput > 0.5f) {
			turnInput = 1f;
		} else if (turnInput < -0.5f) {
			turnInput = -1f;
		} else {
			turnInput = 0f;
		}
		if (forwardInput == 1) {
			turnLeft = false;
			turnRight = false;
		}        
		if (turnInput == 1) {
			//print ("Left turn detected");
			turnLeft = true;
			turnRight = false;
			destinationRotation = playerCamera.transform.rotation * Quaternion.Euler (0f, 90f, 0f);
		} else if (turnInput == -1) {
			//print ("Right turn detected");
			turnRight = true;
			turnLeft = false;
			destinationRotation = playerCamera.transform.rotation * Quaternion.Euler (0f, -90f, 0f);
		}
		if (forwardInput > 0) {
			playerAnim.SetFloat ("forwardInput", 1f);
		} else if (forwardInput < 0) {
			playerAnim.SetFloat ("forwardInput", -1f);
		} else {
			playerAnim.SetFloat ("forwardInput", 0f);
		}
		if (turnInput == 1 || turnInput == -1) {
			playerAnim.SetFloat("forwardInput", Mathf.Ceil(Mathf.Abs(turnInput)));
		}
        //playerAnim.SetFloat("rotInput", turnInput);
    }
    void Turn()
    {
		if (turnInput != 1 && turnInput != -1 && !turnLeft && !turnRight) {
			Quaternion lookRotation = playerCamera.transform.rotation;
			lookRotation.eulerAngles = new Vector3 (0f, Mathf.LerpAngle (transform.rotation.eulerAngles.y, lookRotation.eulerAngles.y, smoothness), 0f);
			transform.rotation = lookRotation;
		} else {
			destinationRotation.eulerAngles = new Vector3 (0f, destinationRotation.eulerAngles.y, 0f);
			rbody.MoveRotation (Quaternion.Lerp (transform.rotation, destinationRotation, smoothness));
			Vector3 movement = Vector3.zero;
			if (turnLeft) {
				movement = transform.forward * turnInput * forwardSpeed * Time.deltaTime;
			} else if (turnRight) {
				movement = transform.forward * turnInput * -forwardSpeed * Time.deltaTime;
			}
			rbody.MovePosition (rbody.position + movement);
		}
    }

    void Move()
    {
        Vector3 forwardMovement = transform.forward * forwardInput * forwardSpeed * Time.deltaTime;
		//Vector3 horizontalMovement = transform.right * turnInput * forwardSpeed * Time.deltaTime;
		rbody.MovePosition(rbody.position + forwardMovement);
    }
    /**
     * this method is suposed to snap the player to the forward vector of the camera if they move.
     * it doesnt work with the rigidbody method i changed to.
     **/
    void SnapAlignCharacterWithCamera()
    {
        if (MoveVector.x != 0 || MoveVector.z != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

}
