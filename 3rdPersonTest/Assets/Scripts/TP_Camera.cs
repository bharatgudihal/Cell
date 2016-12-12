using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Camera : MonoBehaviour {

    public static TP_Camera Instance;
    public Transform TargetLookAt;


    public float distance = 5f;
    public float distanceMin = 3f;
    public float distanceSmooth = .05f;
    public float distanceMax = 10f;

    public float xSensitivity = 5f;
    public float ySensitivity = 5f;


    public float xSmooth = 0.05f;
    public float ySmooth = 0.1f;
    public float yMinLimit = -40f;
    public float yMaxLimit = 80f;


    private float moveX;
    private float moveY;
    private float velX = 0f;
    private float velY = 0f;
    private float velZ = 0f;
    private float velDistance = 0f;
    private float startDistance = 0f;
    private Vector3 position = Vector3.zero;
    private Vector3 desiredPosition = Vector3.zero;
    private float desiredDistance = 0f;


    public bool invertControls;

    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start ()
    {
        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
        startDistance = distance;
        Reset();
        
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (TargetLookAt == null)
            return;

        HandlePlayerInput();
        CalculateDesiredPosition();
        UpdatePosition();

    }
    void HandlePlayerInput()
    {
        //float deadZone = 0.1f;
        if (invertControls)
            moveX += Input.GetAxis("Camera Y")* xSensitivity;//your XRotation is equal to your RTS times its sensitivity. 
        else
            moveX -= Input.GetAxis("Camera Y") * xSensitivity;//your XRotation is equal to your RTS times its sensitivity.

            moveY += Input.GetAxis("Camera X") * ySensitivity;//your YRotation is equal to your RTS Times its sensitivity.
       
            

        moveX = Mathf.Clamp(moveX, yMinLimit, yMaxLimit);//keep your Y value between a max and min.

        
    }
    void CalculateDesiredPosition()
    {
        distance = Mathf.SmoothDamp(distance, desiredDistance, ref velDistance, distanceSmooth);

        desiredPosition = CalculatePosition(moveX, moveY, distance);
    }

    Vector3 CalculatePosition(float rotationX,float rotationY,float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return TargetLookAt.position + rotation * direction;
    }
    void UpdatePosition()
    {
        float posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, xSmooth);
        float posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, ySmooth);
        float posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, xSmooth);

        position = new Vector3(posX, posY, posZ);
        transform.position = position;
        transform.LookAt(TargetLookAt);
    }
    public void Reset()
    {
        moveX = 0f;
        moveY = 10f;
        distance = startDistance;
        desiredDistance = distance;
    }
    public static void UseExistingOrCreateNewMainCamera()
    {
        /*
        GameObject tempCamera;
        GameObject targetLookAt;
        TP_Camera myCamera;

        if(Camera.main != null)
        {
            tempCamera = Camera.main.gameObject;
        }
        else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent<Camera>();
            tempCamera.tag = "MainCamera";
        }
        tempCamera.AddComponent<TP_Camera>();
        myCamera = tempCamera.GetComponent<TP_Camera>() as TP_Camera;

        targetLookAt = GameObject.Find("targetLookAt") as GameObject;

        if(targetLookAt == null)
        {
            targetLookAt = new GameObject("targetLookAt");
            targetLookAt.transform.position = Vector3.zero;
        }

        myCamera.TargetLookAt = targetLookAt.transform;
        */
    }
}
