using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Motor : MonoBehaviour {

    public static TP_Motor instance;

    public float moveSpeed = 10f;//10 meters persecond

    public Vector3 MoveVector { get; set; }

	// Use this for initialization
	void Awake ()
    {
        instance = this;
		
	}
	
	public void UpdateMotor ()
    {
        //ProcessMotion();
        //SnapAlignCharacterWithCamera();
    }
    void ProcessMotion()
    {
        
        //transform moveVector in world space relative to our player
        MoveVector = transform.TransformDirection(MoveVector);
        //normalize our move vector if magnitude >1
        if (MoveVector.magnitude > 1)
            MoveVector = Vector3.Normalize(MoveVector);
        //Multiply movevector by movespeed
        MoveVector *= moveSpeed;
        //multiply by delta time so it is value by second
        MoveVector *= Time.deltaTime;
        //move the character in world space
        //TP_Controller.characterController.Move(MoveVector);
        
        
         

    }
    void SnapAlignCharacterWithCamera()
    {
        if(MoveVector.x != 0 || MoveVector.z!=0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
