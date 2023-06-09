using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //private int totalKey;
    public int totalKey;

    public int keyNumber;

    public int stageIndex;
    public int health;
    public PlayerMove player;

    public Dialogue dialogue;

    public GameObject[] Stages;

    //UI?? ???? ???????? ????
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    public GameObject DialoguePanel;
    public GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");


        if (playerObject != null)
        {
            playerMove = playerObject.GetComponent<PlayerMove>();
            playerAttack = playerObject.GetComponent<PlayerAttack>();
        }
        dialogue = DialoguePanel.GetComponent<Dialogue>();

    }

    void Update()
    {

        UIPoint.text = (totalKey + keyNumber).ToString();
    }
    public void NextStage()
    {
        if (playerMove.bossDie){//Game Clear
            Time.timeScale = 0;

            SceneManager.LoadScene("Ending");

            //Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            //btnText.text = "Clear!";
            //UIRestartBtn.SetActive(true);
        }

        //Change Stage
        if (stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE" + (stageIndex + 1);
            totalKey += keyNumber;
            keyNumber = 0;
        }
        else if (stageIndex == Stages.Length - 1)
        {
            totalKey += keyNumber;
            keyNumber = 0;
            SceneManager.LoadScene("Boss");
        }

        //Calculate Key
    }

    public void clear()
    {
        Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
        btnText.text = "Clear";
        UIRestartBtn.SetActive(true);

        //???? ?????? start ?????? ???????? ??????
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
            //Debug.Log("????");
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

    public void SecretRoomLog()
    {
        Debug.Log("?????????? ????");
        //DialoguePanel.SetActive(true);
        dialogue.SecretRoom();
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-35.6f, -1.7f, 0);
        player.VelocityZero();
    }

    public void BossRetry()
    {
        Time.timeScale = 1;
        Debug.Log("Å¬¸¯");
        if (SceneManager.GetActiveScene().name == "Boss_S")
            SceneManager.LoadScene("Boss_S");
        else if (SceneManager.GetActiveScene().name == "Boss_M")
            SceneManager.LoadScene("Boss_M");
        else
            SceneManager.LoadScene("Boss_L");
    }

    public void GoBadEnding()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("BadEnding");
    }
}

