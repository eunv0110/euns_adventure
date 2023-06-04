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

    //UI를 담을 변수들을 생성
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

            SceneManager.LoadScene("GoodEnding");

            //Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            //btnText.text = "Clear!";
            //UIRestartBtn.SetActive(true);
        }
        //Calculate Key
        totalKey += keyNumber;
        keyNumber = 0;
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
            Debug.Log("죽음");
            UIRestartBtn.SetActive(true);
        }
    }

    public void HealthUp()
    {
        if (health < 3)
        {
            health++;
            //색깔 되돌리기
            UIhealth[health-1].color = new Color(1, 1, 1, 1);

        }
        else
        {
            //냅두기
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision);
            HealthDown();
            if (health > 0)
                PlayerReposition();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 2, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}

