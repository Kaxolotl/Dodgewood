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

    [SerializeField] Image startButton;
    [SerializeField] Image exitButton;

    private void Start()
    {
        SelectButtonByScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(Application.loadedLevelName);
        SelectButton(selectedButton);
    }

    private void Update()
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
                
            }
        }

        if (Input.GetAxisRaw("Horizontal") == 0 || (Input.GetAxisRaw("1P_Triggers") == 0) || (Input.GetAxisRaw("1P_HorizontalDPad") == 0))
        {
            axisInUse = false;
        }

        
        UserInputs();
    }
    
    
    void UserInputs()
    {
        if ((Input.GetButtonDown("1P_XBtn") || Input.GetKeyDown(KeyCode.Space)))
        {
            if (selectedButton == 0)
                SceneManager.LoadScene(1);
            else
                Application.Quit();
        }
    }

    void SelectButtonByScene(int sceneNumber)
    {
        switch (sceneNumber)
        {
            case 0:
                //Intro씬일 때
                thisSceneButtonMax = IntroButtonMax;
                buttonList = new List<Image>();
                
                for (int i = 0; i < IntroButtonMax; i++)
                {
                    Image button = GameObject.Find("Button" + i).GetComponent<Image>();
                    buttonList.Add(button);
                }
                break;
            case 1:
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
}
