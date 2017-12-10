using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
                Debug.Log("instance 생성");
                if (_instance == null)
                {
                    Debug.LogWarning(string.Format("{0} 인스턴스를 찾을 수 없습니다.", typeof(GameManager).ToString()));
                }
            }
            return _instance;
        }
    }



    public int gameMode = 0; // 0이면 싱글, 1이면 멀티
    public int player_1 = 0; // 0이면 마틸다 
    public int player_2 = 0;
    public int map = 0; // 0이면 아포칼립스
    public int nowPlayer = 1;

    public int p1ammo = 0;
    public int p2ammo = 0;

    public int firstScore = 0;
    public int secondScore = 0;
    public int thirdScore = 0;
    public int fourthScore = 0;
    public int fifthScore = 0;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void GameOver()
    {
        gameMode = 0;
        player_1 = 0;
        player_2 = 0;
        map = 0;
        nowPlayer = 1;
    }
}
