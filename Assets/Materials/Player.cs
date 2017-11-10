using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public	GameObject	bullet;
	public	float		speed;
	public	int			ammo;
	
	void Awake()
	{
		speed = 20.0f;
		ammo = 0;
	}
	void Update()
	{
        /*
		 * if ( Input.GetKeyDown("1")) ammo ++;
         *
		 * float z = Input.GetAxisRaw("Vertical");
		 * float x = Input.GetAxisRaw("Horizontal");

		 * if ( x != 0 || z != 0 )
		 * {
		 *     Vector3 dir = x * Vector3.right + z * Vector3.forward;
         * 
		 *     transform.rotation = Quaternion.LookRotation(dir);
		 * 	   transform.position += new Vector3(x, 0.0f, z) * speed * Time.deltaTime;
		 * }
         */

        if(ammo >= 3)
        {
            ammo = 3;
        }

        /*
		 * if ( Input.GetKey(KeyCode.Space) && ammo == 3 )
		 * {
		 * 	   GameObject clone = Instantiate(bullet);
		 * 	   clone.GetComponent<Bullet>().Init(transform.position, transform.TransformDirection(Vector3.forward));
		 * 	   ammo -= 3;
		 * }
         */

        Movement();
        UserInputs();
    }

    void Movement()
    {
        float z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");

        if (x != 0 || z != 0)
        {
            Vector3 dir = x * Vector3.right + z * Vector3.forward;

            transform.rotation = Quaternion.LookRotation(dir);
            transform.position += new Vector3(x, 0.0f, z) * speed * Time.deltaTime;
        }
    }

    void UserInputs()
    {
        if (Input.GetButtonDown("1P_ABtn"))
        {

            Debug.Log("A Button!");
        }
        if (Input.GetButtonDown("1P_BBtn"))
        {
            Debug.Log("B Button!");
        }
        if (Input.GetButtonDown("1P_XBtn") && ammo == 3)
        {
            GameObject clone = Instantiate(bullet);
            clone.GetComponent<Bullet>().Init(transform.position, transform.TransformDirection(Vector3.forward));
            ammo -= 3;
            Debug.Log("히히 총알 발싸!");
        }
        if (Input.GetButtonDown("1P_YBtn"))
        {
            Debug.Log("Y Button!");
        }
        if (Input.GetButtonDown("1P_LBmp"))
        {
            Debug.Log("Left Bumper!");
        }
        if (Input.GetButtonDown("1P_RBmp"))
        {
            ammo++;
            Debug.Log("총알 획득!");
        }
        if (Input.GetButtonDown("1P_SelectBtn"))
        {
            Debug.Log("Back Button!");
        }
        if (Input.GetButtonDown("1P_StartBtn"))
        {
            Debug.Log("Start Button!");
        }
        if (Input.GetButtonDown("1P_LTmbStkBtn"))
        {
            Debug.Log("Left Thumbstick Button!");
        }
        if (Input.GetButtonDown("1P_RTmbStkBtn"))
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
        if (Input.GetAxis("1P_HorizontalDPad") > 0.001)
        {
            Debug.Log("Right D-PAD Button!");
        }
        if (Input.GetAxis("1P_HorizontalDPad") < 0)
        {
            Debug.Log("Left D-PAD Button!");
        }
        if (Input.GetAxis("1P_VerticalDPad") > 0.001)
        {
            Debug.Log("Up D-PAD Button!");
        }
        if (Input.GetAxis("1P_VerticalDPad") < 0)
        {
            Debug.Log("Down D-PAD Button!");
        }
    }
}

