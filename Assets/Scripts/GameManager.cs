using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalKey;
    //public int stagePoint;

    public int keyNumber;

    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    //UI?? ???? ???????? ????
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIPoint.text = (totalKey + keyNumber).ToString();
    }
    public void NextStage()
    {
        //Change Stage
        if (stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE" + (stageIndex + 1);
        }
        else
        {//Game Clear
            Time.timeScale = 0;

            SceneManager.LoadScene("Ending");

            //Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            //btnText.text = "Clear!";
            //UIRestartBtn.SetActive(true);
        }
        //Calculate Key
        totalKey += keyNumber;
        keyNumber = 0;
    }

    public void clear()
    {
        Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
        btnText.text = "Clear!";
        UIRestartBtn.SetActive(true);

        //버튼 누르면 start 씬으로 이동하게 바꾸기
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else {
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            player.OnDie();
            Debug.Log("????");
            UIRestartBtn.SetActive(true);
        }
    }

    public void HealthUp()
    {
        if (health < 3)
        {
            health++;
            //???? ????????
            UIhealth[health-1].color = new Color(1, 1, 1, 1);

        }

    }


    void PlayerReposition()
    {
        player.transform.position = new Vector3(-35.6f, -1.7f, 0);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}

