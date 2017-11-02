using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : LivingEntity
{
    Vector3 lookDirection;
    Rigidbody myRigidbody;

    public float moveSpeed = 5;

    [SerializeField]
    Transform SPoint;
    public Transform ball;

    public float PlayerMovementSpeed = 10;
    public float PlayerRotationSpeed = 90;

    protected override void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        UserInputs();
    }

    void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal") * Time.smoothDeltaTime * PlayerMovementSpeed, 0, Input.GetAxis("Horizontal") * Time.deltaTime * PlayerMovementSpeed);
        Vector3 moveVelocity = move.normalized * PlayerRotationSpeed;
        if (move != new Vector3(0, 0, 0))
        {
            float x = Input.GetAxisRaw("Vertical");
            float z = Input.GetAxisRaw("Horizontal");

            lookDirection = x * Vector3.forward + z * Vector3.right;

            transform.rotation = Quaternion.LookRotation(lookDirection);
            transform.Translate(Vector3.forward * PlayerMovementSpeed * Time.deltaTime);
        }
    }

    void UserInputs()
    {
        if (Input.GetButtonDown("1P_AButton"))
        {
            Transform _ball = Instantiate(ball, SPoint.position, SPoint.rotation);
            Debug.Log("A Button!");
        }
        if (Input.GetButtonDown("1P_BButton"))
        {
            Debug.Log("B Button!");
        }
        if (Input.GetButtonDown("1P_XButton"))
        {
            Debug.Log("X Button!");
        }
        if (Input.GetButtonDown("1P_YButton"))
        {
            Debug.Log("Y Button!");
        }
        if (Input.GetButtonDown("1P_LBumper"))
        {
            Debug.Log("Left Bumper!");
        }
        if (Input.GetButtonDown("1P_RBumper"))
        {
            Debug.Log("Right Bumper!");
        }
        if (Input.GetButtonDown("1P_SelectButton"))
        {
            Debug.Log("Select Button!");
        }
        if (Input.GetButtonDown("1P_StartButton"))
        {
            Debug.Log("Start Button!");
        }
        if (Input.GetButtonDown("1P_LThumbstickButton"))
        {
            Debug.Log("Left Thumbstick Button!");
        }
        if (Input.GetButtonDown("1P_RThumbstickButton"))
        {
            Debug.Log("Right Thumbstick Button!");
        }

        if (Input.GetAxis("1P_Triggers") > 0.001)
        {
            Debug.Log("Right Trigger!");
        }
        if (Input.GetAxis("1P_Triggers") < 0)
        {
            Debug.Log("Left Trigger!");
        }
        if (Input.GetAxis("1P_HorizontalDPAD") > 0.001)
        {
            Debug.Log("Right D-PAD Button!");
        }
        if (Input.GetAxis("1P_HorizontalDPAD") < 0)
        {
            Debug.Log("Left D-PAD Button!");
        }
        if (Input.GetAxis("1P_VerticalDPAD") > 0.001)
        {
            Debug.Log("Up D-PAD Button!");
        }
        if (Input.GetAxis("1P_VerticalDPAD") < 0)
        {
            Debug.Log("Down D-PAD Button!");
        }
    }
}