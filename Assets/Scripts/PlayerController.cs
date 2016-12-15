using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public Rigidbody rbody;
    Animator playerAnim;//a reference for the players animations
    public float forwardSpeed;//the move speed of the player    
    float forwardInput;//member variable for the forwardInput
    float turnInput;//member variable for our turnInput
	float vel1 = 0f;
	float vel2 = 0f;
	bool cameraMoving;

	[SerializeField]
	GameObject playerCamera;

	[SerializeField]
	float smoothness;

    public Vector3 MoveVector { get; set; }// this is a carry over from a tutorial i found. it was used in the snap method I wanted to use.

	Quaternion destinationRotation;

	float totalTurn = 0f;
	float totalForward = 0f;
	bool isPlayerMoving = false;
	Quaternion lastCameraRotation = Quaternion.identity;

	float lastStoppedCameraRotation;

    // Use this for initialization
    void Awake ()
    {
        playerAnim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();

    }

	void Start(){
		lastStoppedCameraRotation = playerCamera.transform.rotation.eulerAngles.y;
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
		turnInput = Input.GetAxis("Horizontal");
		if ((Input.GetAxis ("Camera Y") != 0) || (Input.GetAxis ("Camera X") != 0)) {
			cameraMoving = true;
		} else {
			cameraMoving = false;
		}
		if (turnInput != 0f || forwardInput != 0f) {
			totalTurn = turnInput;
			totalForward = forwardInput;
			isPlayerMoving = true;
		} else {
			isPlayerMoving = false;
			lastStoppedCameraRotation = playerCamera.transform.rotation.eulerAngles.y;
		}
		if (forwardInput > 0) {
			playerAnim.SetFloat ("forwardInput", 1f);
		} else if (forwardInput < 0) {
			playerAnim.SetFloat ("forwardInput", 1f);
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
		if (cameraMoving && isPlayerMoving) {
			Quaternion lookRotation = playerCamera.transform.rotation;
			lookRotation.eulerAngles = new Vector3 (0f, Mathf.SmoothDampAngle (transform.rotation.eulerAngles.y, lookRotation.eulerAngles.y, ref vel1, smoothness), 0f);
			transform.rotation = lookRotation;
			lastCameraRotation = lookRotation;
		} else {
			if (isPlayerMoving) {
					Quaternion lookRotation = playerCamera.transform.rotation;
					lookRotation.eulerAngles = new Vector3 (0f, Mathf.SmoothDampAngle (transform.rotation.eulerAngles.y, lookRotation.eulerAngles.y+Mathf.Rad2Deg * Mathf.Atan2 (totalTurn, totalForward), ref vel1, smoothness), 0f);
					transform.rotation = lookRotation;
					if (transform.rotation.eulerAngles.y == lookRotation.eulerAngles.y) {						
						lastStoppedCameraRotation = playerCamera.transform.rotation.eulerAngles.y;
				}
			}
		}
    }

    void Move()
    {
		if (!cameraMoving) {
			Vector3 forwardMovement = transform.forward * (Mathf.Abs (forwardInput) + Mathf.Abs (turnInput)) * forwardSpeed * Time.deltaTime;
			rbody.MovePosition (rbody.position + forwardMovement);
		} else {
			Vector3 forwardMovement = transform.forward * (forwardInput) * forwardSpeed * Time.deltaTime;
			rbody.MovePosition(rbody.position + forwardMovement);
		}
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
