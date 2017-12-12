using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ranking : MonoBehaviour {

    public Text firstScore;
    public Text secondScore;
    public Text thirdScore;
    public Text fourthScore;
    public Text fifthScore;

    private void OnEnable()
    {
        InvokeRepeating("Anykey", 1, 0.1f);

        firstScore.text = GameManager.Instance.firstScore.ToString();
        secondScore.text = GameManager.Instance.secondScore.ToString();
        thirdScore.text = GameManager.Instance.thirdScore.ToString();
        fourthScore.text = GameManager.Instance.fourthScore.ToString();
        fifthScore.text = GameManager.Instance.fifthScore.ToString();
    }

    void Anykey ()
    {
        if (Input.anyKey)
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
