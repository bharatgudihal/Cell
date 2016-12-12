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

    public Vector3 MoveVector { get; set; }// this is a carry over from a tutorial i found. it was used in the snap method I wanted to use.


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
        forwardInput = Input.GetAxis("Vertical");//look for input in the 'w' or left joystick

        if (forwardInput != 0f)//trigger the animation if move. this will probably change but this works for testing.
            playerAnim.SetBool("isWalking", true);
        else
            playerAnim.SetBool("isWalking", false);//end the animation 

        turnInput = Input.GetAxis("Horizontal");//look for the rotation or 'a' and 'd' or left and right on the joystick
    }
    void Turn()
    {
        float turn = turnInput * turnSpeed * Time.deltaTime;//set your turn value to be the input given multiplied by a speed and time 
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);// convert to an eluer

        rbody.MoveRotation(rbody.rotation * turnRotation);//set your new rotation. fucking math.
    }
    void Move()
    {
        Vector3 movement = transform.forward * forwardInput * forwardSpeed * Time.deltaTime;
        rbody.MovePosition(rbody.position + movement);
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
