using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

    const int IntroButtonMax = 2;
    const int CharButtonMax = 4;

    private bool axisInUse = false;
    int selectedButton = 0;
    int thisSceneButtonMax = 0;

    List<Image> buttonList;

    private void Start()
    {
        SelectButtonByScene(SceneManager.GetActiveScene().buildIndex);
        SelectButton(selectedButton);
    }

    private void Update()
    {
        UserInputs();
    }


    void UserInputs()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || (Input.GetAxisRaw("1P_Triggers") == 1) || (Input.GetAxisRaw("1P_HorizontalDPad") == 1))
        {
            if (axisInUse == false)
            {
                axisInUse = true;

                selectedButton++;

                if (selectedButton == thisSceneButtonMax)
                    selectedButton--;

                SelectButton(selectedButton);
                StartCoroutine(ButtonSelectDelay());
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 || (Input.GetAxisRaw("1P_Triggers") == -1) || (Input.GetAxisRaw("1P_HorizontalDPad") == -1))
        {
            if (axisInUse == false)
            {
                axisInUse = true;

                selectedButton--;

                if (selectedButton < 0)
                    selectedButton++;

                SelectButton(selectedButton);
                StartCoroutine(ButtonSelectDelay());
            }
        }

        if ((Input.GetButtonDown("1P_XBtn") || Input.GetKeyDown(KeyCode.Space)))
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    //Intro씬일 때
                    if (selectedButton == 0)
                        SceneManager.LoadScene(1);
                    else
                        Application.Quit();
                    break;

                case 1:
                    //CharacterSelect씬일 때
                    //캐릭터 선택으로 수정해야함
                    if (selectedButton == 0)
                        SceneManager.LoadScene(2);
                    break;
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
        }
    }

    void SelectButton(int selectedButtonNumber)
    {
        for (int i = 0; i < thisSceneButtonMax; i++)
        {
            buttonList[i].color = Color.white;
        }
        buttonList[selectedButtonNumber].color = Color.red;
    }

    IEnumerator ButtonSelectDelay()
    {
        yield return new WaitForSeconds(0.2f);
        axisInUse = false;
    }
}
