using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
                if (_instance == null)
                {
                    Debug.LogWarning(string.Format("{0} 인스턴스를 찾을 수 없습니다.", typeof(UIManager).ToString()));
                }
            }
            return _instance;
        }
    }

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

    public GameObject player1UI;
    public Player player1;
    public Image player1_portrait;
    public Image player1_ammo1;
    public Image player1_ammo2;
    public Image player1_ammo3;
    public Image player1_dash;

    public GameObject player2UI;
    public Player player2;
    public Image player2_portrait;
    public Image player2_ammo1;
    public Image player2_ammo2;
    public Image player2_ammo3;
    public Image player2_dash;

    public Text scoreUI;
    public int score;
    public int shootscore;

    public GameObject result;
    public Text resultScore;

    public GameObject Win1P;
    public GameObject Win2P;

    public GameObject ranking;
    public Text ranking1;
    public Text ranking2;
    public Text ranking3;
    public Text ranking4;
    public Text ranking5;

    public AudioSource _audio;

    public AudioClip _dash;
    public AudioClip _rooting;
    public AudioClip _select;
    public AudioClip _shoot;
    public AudioClip _win;


    private void Awake()
    {
        InvokeRepeating("ScoreUpdate", 0, 1f);

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        shootscore = 0;
        _audio = GetComponent<AudioSource>();

        player1_portrait = transform.Find("Canvas/PlayerUI/Portrait").GetComponent<Image>();
        player1_ammo1 = transform.Find("Canvas/PlayerUI/Ammo1").GetComponent<Image>();
        player1_ammo2 = transform.Find("Canvas/PlayerUI/Ammo2").GetComponent<Image>();
        player1_ammo3 = transform.Find("Canvas/PlayerUI/Ammo3").GetComponent<Image>();
        player1_dash = transform.Find("Canvas/PlayerUI/Dash").GetComponent<Image>();

        player2_portrait = transform.Find("Canvas/Player2UI/Portrait").GetComponent<Image>();
        player2_ammo1 = transform.Find("Canvas/Player2UI/Ammo1").GetComponent<Image>();
        player2_ammo2 = transform.Find("Canvas/Player2UI/Ammo2").GetComponent<Image>();
        player2_ammo3 = transform.Find("Canvas/Player2UI/Ammo3").GetComponent<Image>();
        player2_dash = transform.Find("Canvas/Player2UI/Dash").GetComponent<Image>();
    }
    
    public void UIInit()
    {
        player1_portrait.sprite = Resources.Load<Sprite>("portrait_" + GameManager.Instance.player_1.ToString());
        player1 = GameObject.FindGameObjectWithTag("player_1").GetComponent<Player>();
        scoreUI.gameObject.SetActive(true);
        player2UI.gameObject.SetActive(false);

        if (GameManager.Instance.gameMode == 2)
        {
            player2_portrait.sprite = Resources.Load<Sprite>("portrait_" + GameManager.Instance.player_2.ToString());
            player2 = GameObject.FindGameObjectWithTag("player_2").GetComponent<Player>();
            scoreUI.gameObject.SetActive(false);
            player2UI.gameObject.SetActive(true);
            Debug.Log("portrait_" + GameManager.Instance.player_1.ToString());
            Debug.Log("portrait_" + GameManager.Instance.player_2.ToString());
        }

    }

    private void Update()
    {
        if(GameManager.Instance.gameMode == 1 && SceneManager.GetActiveScene().buildIndex > 2)
        {
            scoreUI.text = score.ToString();
            resultScore.text = score.ToString();
        }

        if (GameManager.Instance.gameMode != 0 && SceneManager.GetActiveScene().buildIndex > 2)
        {
            if (player1._canDash)
                player1_dash.gameObject.SetActive(true);
            else
                player1_dash.gameObject.SetActive(false);
            if (GameManager.Instance.gameMode == 2)
            {
                if (player2._canDash)
                    player2_dash.gameObject.SetActive(true);
                else
                    player2_dash.gameObject.SetActive(false);
            }
        }
    }

    public void ScoreUpdate()
    {
        if (GameManager.Instance.gameMode != 0)
        {
            score += shootscore;
        }
    }

    public void UIUpdate()
    {
        AmmoUpdate(1, player1.ammo);
        if(GameManager.Instance.gameMode == 2)
        {
            AmmoUpdate(2, player2.ammo);
        }
    }

    public void PlayDash()
    {
        _audio.PlayOneShot(_dash);
    }
    public void PlayRooting()
    {
        _audio.PlayOneShot(_rooting);
    }
    public void PlaySelect()
    {
        _audio.PlayOneShot(_select);
    }
    public void PlayShoot()
    {
        _audio.PlayOneShot(_shoot);
    }
    public void PlayWin()
    {
        _audio.PlayOneShot(_win);
    }

    public void Player1_Dash()
    {
        player1_dash.gameObject.SetActive(false);
    }

    public void Player2_Dash()
    {
        player2_dash.gameObject.SetActive(false);
    }

    void AmmoUpdate(int player, int ammo)
    {
        if (player == 1)
        {
            switch (ammo)
            {
                case 0:
                    player1_ammo1.gameObject.SetActive(false);
                    player1_ammo2.gameObject.SetActive(false);
                    player1_ammo3.gameObject.SetActive(false);
                    break;
                case 1:
                    player1_ammo1.gameObject.SetActive(true);
                    player1_ammo2.gameObject.SetActive(false);
                    player1_ammo3.gameObject.SetActive(false);
                    break;
                case 2:
                    player1_ammo1.gameObject.SetActive(true);
                    player1_ammo2.gameObject.SetActive(true);
                    player1_ammo3.gameObject.SetActive(false);
                    break;
                case 3:
                    player1_ammo1.gameObject.SetActive(true);
                    player1_ammo2.gameObject.SetActive(true);
                    player1_ammo3.gameObject.SetActive(true);
                    break;
            }

        }
        if (player == 2)
        {
            switch (ammo)
            {
                case 0:
                    player2_ammo1.gameObject.SetActive(false);
                    player2_ammo2.gameObject.SetActive(false);
                    player2_ammo3.gameObject.SetActive(false);
                    break;
                case 1:
                    player2_ammo1.gameObject.SetActive(true);
                    player2_ammo2.gameObject.SetActive(false);
                    player2_ammo3.gameObject.SetActive(false);
                    break;
                case 2:
                    player2_ammo1.gameObject.SetActive(true);
                    player2_ammo2.gameObject.SetActive(true);
                    player2_ammo3.gameObject.SetActive(false);
                    break;
                case 3:
                    player2_ammo1.gameObject.SetActive(true);
                    player2_ammo2.gameObject.SetActive(true);
                    player2_ammo3.gameObject.SetActive(true);
                    break;
            }
        }
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }


}
