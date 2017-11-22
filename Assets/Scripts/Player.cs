using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHAR_STATE { IDLE = 0, RUN, }

public class Player : MonoBehaviour
{
    const int AMMOMAX = 3;

    public LayerMask layerMask;
    public Rigidbody rigid;
    Ray ray;
    RaycastHit hit;
	public	GameObject	bullet;
	public	float		speed;
	[SerializeField] private int _ammo;
    public int ammo
    {
        get
        {
            if (_ammo > AMMOMAX)
                _ammo = AMMOMAX;
            return _ammo;
        }
        set
        {
            _ammo = value;
        }
    }

    Animator animator;
    CHAR_STATE charState;

    private bool isWall;
	
	void Awake()
	{
        rigid = GetComponent<Rigidbody>();
        speed = 20.0f;
		ammo = AMMOMAX;
        isWall = false;
        animator = GetComponent<Animator>();
        charState = CHAR_STATE.IDLE;

    }

    bool RayCheck()
    {
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 4f, layerMask))
        {
            Debug.Log(hit.collider.gameObject.name);
            
            hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            return true;
        }
        else
            return false;
    }

	void Update()
    {
        UserInputs();
        
        Movement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            ammo++;
        }
    }

    void Movement()
    {
        float z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");

        if (x != 0 || z != 0)
        {
            Vector3 dir = x * Vector3.right + z * Vector3.forward;

            transform.rotation = Quaternion.LookRotation(dir);
            if (!RayCheck())
            {
                transform.position += new Vector3(x, 0.0f, z) * speed * Time.deltaTime;
                charState = CHAR_STATE.RUN;
                animator.SetBool("isRun", true);
            }
        }
        else
        {
            charState = CHAR_STATE.IDLE;
            animator.SetBool("isRun", false); // 트리거 사용하거나 기본상태 설정해야겟
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
        if ((Input.GetButtonDown("1P_XBtn") || Input.GetKeyDown(KeyCode.Space)) && ammo == 3 && charState == 0)
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
        if (Input.GetButtonDown("1P_LBmp") || Input.GetKeyDown(KeyCode.LeftShift))
        {
            ammo++;
            Debug.Log("총알 획득!");
        }
        if (Input.GetButtonDown("1P_RBmp"))
        {
            Debug.Log("회피!");
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

