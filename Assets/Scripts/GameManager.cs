﻿using System.Collections;
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
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    Debug.LogWarning(string.Format("{0} 인스턴스를 찾을 수 없습니다.", typeof(GameManager).ToString()));
                }
            }
            return _instance;
        }
    }



    public int gameMode = 0;
    public int player_1 = 0;
    public int player_2 = 0;
    public int map = 0;

    public int nowPlayer = 1;

    private void OnEnable()
    {
        DontDestroyOnLoad(this);
    }
}
