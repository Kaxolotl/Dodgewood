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
                _instance = FindObjectOfType<UIManager>();
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

    public Image player1_portrait;
    public Image player1_ammo1;
    public Image player1_ammo2;
    public Image player1_ammo3;
    public Image player1_dash;

    public Image player2_portrait;
    public Image player2_ammo1;
    public Image player2_ammo2;
    public Image player2_ammo3;
    public Image player2_dash;



    private void Awake()
    {
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

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }


}
