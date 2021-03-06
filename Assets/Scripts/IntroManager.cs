﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

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

    const int IntroButtonMax = 4;
    const int CharButtonMax = 4;
    const int MapButtonMax = 6;

    enum Mode { nothing = 0, single, multi}
    enum Character { matilda = 0, captain, navi, terminator}
    enum Map { apocalypse = 0, city, dungeon, militery, port, space}

    private bool axisInUse_1 = false;
    private bool axisInUse_2 = false;

    bool firstPlayer = true;

    int selectedButton_1 = 0;
    int selectedButton_2 = 1;
    int thisSceneButtonMax = 0;

    List<Image> buttonList;

    public GameObject ranking;

    private void Start()
    {
        Time.timeScale = 1f;
        SelectButtonByScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        UserInputs_1();
        if (SceneManager.GetActiveScene().buildIndex == 1)
            UserInputs_2();
        ButtonEffect();
    }


    void UserInputs_1()
    {
        if (Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Horizontal") == 1 || (Input.GetAxisRaw("1P_Triggers") == 1) || (Input.GetAxisRaw("1P_HorizontalDPad") == 1) || (Input.GetAxisRaw("1P_VerticalDPad") == -1))
        {
            if (axisInUse_1 == false)
            {
                axisInUse_1 = true;

                selectedButton_1++;

                if (selectedButton_1 == thisSceneButtonMax)
                    selectedButton_1--;

                StartCoroutine(ButtonSelectDelay());
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Horizontal") == -1 || (Input.GetAxisRaw("1P_Triggers") == -1) || (Input.GetAxisRaw("1P_HorizontalDPad") == -1) || (Input.GetAxisRaw("1P_VerticalDPad") == 1))
        {
            if (axisInUse_1 == false)
            {
                axisInUse_1 = true;

                selectedButton_1--;

                if (selectedButton_1 < 0)
                    selectedButton_1++;

                StartCoroutine(ButtonSelectDelay());
            }
        }

    
        if ((Input.GetButtonDown("1P_XBtn") || Input.GetKeyDown(KeyCode.Space)))
        {
            UIManager.Instance.PlaySelect();
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0: //Intro씬일 때
                    if (selectedButton_1 == 0)//Single
                    {
                        GameManager.Instance.gameMode = (int)Mode.single;
                        SceneManager.LoadScene(1);
                    }
                    else if (selectedButton_1 == 1)//Multi
                    {
                        GameManager.Instance.gameMode = (int)Mode.multi;
                        //2P 활성화
                        SceneManager.LoadScene(1);
                    }
                    else if (selectedButton_1 == 2)//Ranking
                    {
                        ranking.gameObject.SetActive(true);
                    } 
                    else
                    {
                        Application.Quit();
                    }
                    break;

                case 1: //CharacterSelect씬일 때

                    if (GameManager.Instance.gameMode == (int)Mode.single)
                    {
                        if (selectedButton_1 == 0)
                        {
                            GameManager.Instance.player_1 = (int)Character.matilda;
                            SceneManager.LoadScene(2);
                        }
                        if (selectedButton_1 == 1)
                        {
                            GameManager.Instance.player_1 = (int)Character.captain;
                            SceneManager.LoadScene(2);
                        }
                        if (selectedButton_1 == 2)
                        {
                            GameManager.Instance.player_1 = (int)Character.navi;
                            SceneManager.LoadScene(2);
                        }
                        if (selectedButton_1 == 3)
                        {
                            GameManager.Instance.player_1 = (int)Character.terminator;
                            SceneManager.LoadScene(2);
                        }
                    }
                    if (GameManager.Instance.gameMode == (int)Mode.multi)
                    {
                        if (selectedButton_1 == 0)
                        {
                            if (firstPlayer)
                            {
                                GameManager.Instance.player_1 = (int)Character.matilda;
                                firstPlayer = false;
                                break;
                            }
                            GameManager.Instance.player_2 = (int)Character.matilda;
                            SceneManager.LoadScene(2);
                        }
                        if (selectedButton_1 == 1)
                        {
                            if (firstPlayer)
                            {
                                GameManager.Instance.player_1 = (int)Character.captain;
                                firstPlayer = false;
                                break;
                            }
                            GameManager.Instance.player_2 = (int)Character.captain;
                            SceneManager.LoadScene(2);
                        }
                        if (selectedButton_1 == 2)
                        {
                            if (firstPlayer)
                            {
                                GameManager.Instance.player_1 = (int)Character.navi;
                                firstPlayer = false;
                                break;
                            }
                            GameManager.Instance.player_2 = (int)Character.navi;
                            SceneManager.LoadScene(2);
                        }
                        if (selectedButton_1 == 3)
                        {
                            if (firstPlayer)
                            {
                                GameManager.Instance.player_1 = (int)Character.terminator;
                                firstPlayer = false;
                                break;
                            }
                            GameManager.Instance.player_2 = (int)Character.terminator;
                            SceneManager.LoadScene(2);
                        }
                    }
                    break;
                    
                    case 2: //MapSelect씬일 때
                    
                        if (selectedButton_1 == 0)
                        {
                            GameManager.Instance.map = (int)Map.apocalypse;
                            SceneManager.LoadScene(3);
                        }
                        if (selectedButton_1 == 1)
                        {
                            GameManager.Instance.map = (int)Map.city;
                            SceneManager.LoadScene(4);
                        }
                        if (selectedButton_1 == 2)
                        {
                            GameManager.Instance.map = (int)Map.dungeon;
                            SceneManager.LoadScene(5);
                        }
                        if (selectedButton_1 == 3)
                        {
                            GameManager.Instance.map = (int)Map.militery;
                            SceneManager.LoadScene(6);
                        }
                        if (selectedButton_1 == 4)
                        {
                            GameManager.Instance.map = (int)Map.port;
                            SceneManager.LoadScene(7);
                        }
                        if (selectedButton_1 == 5)
                        {
                            GameManager.Instance.map = (int)Map.space;
                            SceneManager.LoadScene(8);
                        }
                    break;
            }
        }
    }
    
    void UserInputs_2()
    {
        if (Input.GetAxisRaw("Vertical") == 1 || (Input.GetAxisRaw("2P_Triggers") == 1) || (Input.GetAxisRaw("2P_HorizontalDPad") == 1))
        {
            if (axisInUse_2 == false)
            {
                axisInUse_2 = true;

                selectedButton_2++;

                if (selectedButton_2 == thisSceneButtonMax)
                    selectedButton_2--;

                StartCoroutine(ButtonSelectDelay2());
            }
        }
        else if (Input.GetAxisRaw("Vertical") == -1 || (Input.GetAxisRaw("2P_Triggers") == -1) || (Input.GetAxisRaw("2P_HorizontalDPad") == -1))
        {
            if (axisInUse_2 == false)
            {
                axisInUse_2 = true;

                selectedButton_2--;

                if (selectedButton_2 < 0)
                    selectedButton_2++;

                StartCoroutine(ButtonSelectDelay2());
            }
        }
    }

    void SelectButtonByScene(int sceneNumber)
    {
        buttonList = new List<Image>();
        switch (sceneNumber)
        {
            case 0:
                //Intro씬일 때
                thisSceneButtonMax = IntroButtonMax;
                
                for (int i = 0; i < IntroButtonMax; i++)
                {
                    Image button = GameObject.Find("Button" + i).GetComponent<Image>();
                    buttonList.Add(button);
                }
                break;
            case 1:
                //CharacterSelect씬일 때
                thisSceneButtonMax = CharButtonMax;

                for (int i = 0; i < CharButtonMax; i++)
                {
                    Image button = GameObject.Find("Button" + i).GetComponent<Image>();
                    buttonList.Add(button);
                }
                break;
            case 2:
                //MapSelect씬일 때
                thisSceneButtonMax = MapButtonMax;

                for (int i = 0; i < MapButtonMax; i++)
                {
                    Image button = GameObject.Find("Button" + i).GetComponent<Image>();
                    buttonList.Add(button);
                }
                break;
        }
    }
    void ButtonEffect()
    {
        for (int i = 0; i < thisSceneButtonMax; i++)
        {
           
            buttonList[i].color = Color.white;
            if(i == selectedButton_1)
            {
                buttonList[i].color = Color.red;
            }
            if (i == selectedButton_1 && !firstPlayer && SceneManager.GetActiveScene().buildIndex == 1)
            {
                buttonList[i].color = Color.blue;
            }
        }
    }

    IEnumerator ButtonSelectDelay()
    {
        yield return new WaitForSeconds(0.2f);
        axisInUse_1 = false;
    }
    IEnumerator ButtonSelectDelay2()
    {
        yield return new WaitForSeconds(0.2f);
        axisInUse_2 = false;
    }
}
