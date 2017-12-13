using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CHAR_STATE { IDLE = 0, RUN, }

public class Player : MonoBehaviour
{
    public GameManager _gameManager;
    public GameManager GameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = FindObjectOfType<GameManager>();

            return _gameManager;
        }
    }

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

    public int playerNum;
    bool _canMove;
    bool _canShoot;
    public bool _canDash;
    bool _Dashing;
    bool _gameStop;

    int whoDead;


    Animator animator;
    CHAR_STATE charState;

    private bool isWall;
	
	void Awake()
    {
        GameManager.Instance.gameStop = false;

           playerNum = 1;

        if (GameManager.Instance.nowPlayer == 2)
            playerNum = 2;

        Debug.Log(playerNum + "인 게임");

        rigid = GetComponent<Rigidbody>();
        speed = 20.0f;
		ammo = 0;
        isWall = false;
        animator = GetComponent<Animator>();
        charState = CHAR_STATE.IDLE;

        _canMove = true;
        _canShoot = true;
        _canDash = true;
        _Dashing = false;
        _gameStop = false;
    }

    private void Start()
    {
        UIManager.Instance.UIUpdate();
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
        if(playerNum == 1 && !_gameStop)
            UserInputs_1P();
        if(playerNum == 2 && !_gameStop)
            UserInputs_2P();



        Movement();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo" && ammo < 3)
        {
            ammo++;
            UIManager.Instance.PlayRooting();
            UIManager.Instance.UIUpdate();
            if(GameManager.Instance.gameMode == 1)
                UIManager.Instance.score += 50;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Bullet" && !_Dashing && GameManager.Instance.gameMode == 1)
        {
            UIManager.Instance.PlayWin();
            GameManager.Instance.nowPlayer = 1;
            GameOver();
            GameManager.Instance.GameOver();
            GameManager.Instance.gameStop = true;
            Time.timeScale = 0.01f;
        }
        else if (other.gameObject.tag == "Bullet" && !_Dashing && GameManager.Instance.gameMode == 2)
        {
            if (gameObject.name == "player_1")
            {
                whoDead = 1;
            }
            else if (gameObject.name == "player_2")
            {
                whoDead = 2;
            }

            UIManager.Instance.PlayWin();
            GameManager.Instance.nowPlayer = 1;
            GameOver();
            GameManager.Instance.GameOver();
            GameManager.Instance.gameStop = true;
            Time.timeScale = 0.01f;
        }
    }

    void Movement()
    {
        if (!_canMove)
            return;

        float z;
        float x;

        if (playerNum == 1)
        {
            //float z = Input.GetAxisRaw("Vertical");
            //float x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("1P_VerticalDPad");
            x = Input.GetAxisRaw("1P_HorizontalDPad");
        }
        else
        {
            z = Input.GetAxisRaw("2P_VerticalDPad");
            x = Input.GetAxisRaw("2P_HorizontalDPad");
        }

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

    IEnumerator Shoot()
    {
        _canShoot = false;
        _canMove = false;
        _canDash = false;

        UIManager.Instance.score += 100;
        UIManager.Instance.shootscore++;
        yield return new WaitForSeconds(0.7f);

        UIManager.Instance.PlayShoot();

        GameObject clone = Instantiate(bullet);
        clone.GetComponent<Bullet>().Init(transform.position, transform.TransformDirection(Vector3.forward));
        ammo -= 3;
        UIManager.Instance.UIUpdate();
        Debug.Log("shoot");

        yield return new WaitForSeconds(0.3f);
        _canMove = true;
        _canShoot = true;
        _canDash = true;
    }

    IEnumerator Dash()
    {
        UIManager.Instance.PlayDash();
        _canDash = false;
        _Dashing = true;
        speed = 50;
        animator.SetBool("isDash", true);
        yield return new WaitForSeconds(0.2f);

        animator.SetBool("isDash", false);
        _Dashing = false;
        speed = 20;
        yield return new WaitForSeconds(0.1f);

        _canDash = true;
    }

    void GameOver()
    {
        if (GameManager.Instance.gameMode == 1)
        {
            UIManager.Instance.result.gameObject.SetActive(true);
            UIManager.Instance.player1UI.gameObject.SetActive(false);
            UIManager.Instance.scoreUI.gameObject.SetActive(false);
        }

        if (GameManager.Instance.gameMode == 2)
        {
            if (whoDead == 1)
            {
                UIManager.Instance.Win2P.gameObject.SetActive(true);
                UIManager.Instance.player1UI.gameObject.SetActive(false);
                UIManager.Instance.player2UI.gameObject.SetActive(false);
                UIManager.Instance.scoreUI.gameObject.SetActive(false);
            }
            if(whoDead == 2)
            {
                UIManager.Instance.Win1P.gameObject.SetActive(true);
                UIManager.Instance.player1UI.gameObject.SetActive(false);
                UIManager.Instance.player2UI.gameObject.SetActive(false);
                UIManager.Instance.scoreUI.gameObject.SetActive(false);
            }
        }


    }


    void UserInputs_1P()
    {
        if (GameManager.Instance.gameStop)
        {
            if (Input.GetButtonDown("1P_StartBtn"))
            {
                UIManager.Instance.result.gameObject.SetActive(false);
                UIManager.Instance.Win1P.gameObject.SetActive(false);
                UIManager.Instance.Win2P.gameObject.SetActive(false);
                UIManager.Instance.player1UI.gameObject.SetActive(true);
                UIManager.Instance.player2UI.gameObject.SetActive(true);
                UIManager.Instance.scoreUI.gameObject.SetActive(true);
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetButtonDown("1P_ABtn"))
        {
            
        }
        if (Input.GetButtonDown("1P_BBtn"))
        {
            
        }
        if ((Input.GetButtonDown("1P_XBtn")) && _canShoot && ammo == 3)
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetButtonDown("1P_YBtn"))
        {
        }
        if (Input.GetButtonDown("1P_LBmp"))
        {
                        
        }
        if (Input.GetButtonDown("1P_RBmp"))
        {
            Debug.Log("회피!");
            if (!_canDash)
               return;
            StartCoroutine(Dash());
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
        }
        if (Input.GetAxis("1P_HorizontalDPad") < 0)
        {
        }
        if (Input.GetAxis("1P_VerticalDPad") > 0.001)
        {
        }
        if (Input.GetAxis("1P_VerticalDPad") < 0)
        {
        }
    }

    void UserInputs_2P()
    {
        if (Input.GetButtonDown("2P_ABtn"))
        {
            Debug.Log("A Button!");
        }
        if (Input.GetButtonDown("2P_BBtn"))
        {
            Debug.Log("B Button!");
        }
        if ((Input.GetButtonDown("2P_XBtn") || Input.GetKeyDown(KeyCode.Space)) && _canShoot && ammo == 3)
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetButtonDown("2P_YBtn") || Input.GetKeyDown(KeyCode.Z))
        {
            
        }
        if (Input.GetButtonDown("2P_LBmp") || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!_canDash)
                return;
            StartCoroutine(Dash());
            Debug.Log("Y Button!");
        }
        if (Input.GetButtonDown("2P_RBmp"))
        {
        }
        if (Input.GetButtonDown("2P_SelectBtn"))
        {
            Debug.Log("Back Button!");
        }
        if (Input.GetButtonDown("2P_StartBtn"))
        {
            Debug.Log("Start Button!");
        }
        if (Input.GetButtonDown("2P_LTmbStkBtn"))
        {
            Debug.Log("Left Thumbstick Button!");
        }
        if (Input.GetButtonDown("2P_RTmbStkBtn"))
        {
            Debug.Log("Right Thumbstick Button!");
        }

        if (Input.GetAxis("2P_Triggers") > 0.001)
        {
            Debug.Log("Right Trigger!");
        }
        if (Input.GetAxis("2P_Triggers") < 0)
        {
            Debug.Log("Left Trigger!");
        }
        if (Input.GetAxis("2P_HorizontalDPad") > 0.001)
        {
            Debug.Log("Right D-PAD Button!");
        }
        if (Input.GetAxis("2P_HorizontalDPad") < 0)
        {
            Debug.Log("Left D-PAD Button!");
        }
        if (Input.GetAxis("2P_VerticalDPad") > 0.001)
        {
            Debug.Log("Up D-PAD Button!");
        }
        if (Input.GetAxis("2P_VerticalDPad") < 0)
        {
            Debug.Log("Down D-PAD Button!");
        }
    }
}

