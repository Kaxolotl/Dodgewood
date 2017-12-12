using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text score;

    private void Awake()
    { 
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

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
        player1_portrait.sprite = Resources.Load<Sprite>("portrait" + GameManager.Instance.player_1);
        player1 = GameObject.FindGameObjectWithTag("player_1").GetComponent<Player>();
        score.gameObject.SetActive(true);
        player2UI.gameObject.SetActive(false);

        if (GameManager.Instance.gameMode == 2)
        {
            player2_portrait.sprite = Resources.Load<Sprite>("portrait" + GameManager.Instance.player_2);
            player2 = GameObject.FindGameObjectWithTag("player_2").GetComponent<Player>();
            score.gameObject.SetActive(false);
            player2UI.gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        if (player1._canDash)
            player1_dash.gameObject.SetActive(true);
        else
            player1_dash.gameObject.SetActive(false);

        if (player2._canDash)
            player2_dash.gameObject.SetActive(true);
        else
            player2_dash.gameObject.SetActive(false);
    }

    public void ScoreUpdate()
    {

    }

    public void UIUpdate()
    {
        AmmoUpdate(1, player1.ammo);
        if(GameManager.Instance.gameMode == 2)
        {
            Debug.Log(player2.ammo);
            AmmoUpdate(2, player2.ammo);
        }
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
