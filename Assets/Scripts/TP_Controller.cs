using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Controller : MonoBehaviour {

    //public static CharacterController characterController;

    public static Rigidbody rbody;
    public float speed;
    //public static TP_Controller instance;


    // Use this for initialization
    void Awake()
    {
        //characterController = GetComponent("CharacterController") as CharacterController;
        rbody = GetComponent<Rigidbody>();

        //instance = this;
        TP_Camera.UseExistingOrCreateNewMainCamera();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    private void Update()
    {
        if (Camera.main == null)
            return;

        GetLocomotionInput();

        TP_Motor.instance.UpdateMotor();
    }
    void GetLocomotionInput()
    {
        float deadZone = 0.1f;

        TP_Motor.instance.MoveVector = Vector3.zero;
        

        if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            TP_Motor.instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));
        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            TP_Motor.instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"),0,0);
    }

}
