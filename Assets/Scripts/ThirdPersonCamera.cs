using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_Angle_MAX = 50.0f;


    public Transform lookAt;//what the camera looks at
    public Transform camTransform;

    private Camera cam;

    public float distance = 10.0f;
    private float currentX = 0f;
    private float currentY = 10.0f;

    public float heightOffset;

    private float sensitivityX = 1.0f;
    private float sensitivityY = 1.0f;



	// Use this for initialization
	void Start () {

        camTransform = transform;
        cam = Camera.main;


	}
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, heightOffset, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;

        camTransform.LookAt(lookAt.position);
    }
    // Update is called once per frame
    void Update ()
    {
        currentX += Input.GetAxis("Camera X");//right thumbstick side
        currentY += Input.GetAxis("Camera Y");//right thumbstick up

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_Angle_MAX);
    }
}
