using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //public string character = "????";
    //private string message = "???? ?????? ?????? ?????? ???? ???? ?????? ?? ?????? \n - Good Ending- ";

    public Text EndingText;
    public int EndingIdx;
    private string[] texts;
    private int clickCount;
    private int clickCount2 = 0;
    public GameManager gameManager;
    public GameObject[] Endings;
    public GameObject DialogPanel;

    public PlayerMove playerMove;
    public GameObject playerObject;
    // Start is called before the first frame update

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            playerMove = playerObject.GetComponent<PlayerMove>();
        }
        clickCount = 0;

        if (playerMove.bossDie) //boss???????????? ????
            PlayEnding();

    }
    private void PlayEnding()
    {

        if (gameManager.totalKey < 30)
           EndingIdx = 2;
        else if (gameManager.totalKey <50)
           EndingIdx = 1;
        else
            EndingIdx = 0;

        Endings[EndingIdx].SetActive(true);



        if (EndingIdx == 0)
        {
            texts = new string[5];
            texts[0] = "?????? ?????? ?????? ???? ??????.";
            texts[1] = "???? ?????? ?????? ?????? ???? ???? ?????? ?? ??????.";
            texts[2] = "?????? ???? ?????? ???? ???????????? ?????? ???? ???????? ?????? ???? ???? ???? ?????? ?????? ?????? ??????.";
            texts[3] = "?????? ???? ???????? ?????? ??????! \n ?????? ?????? ?????? ???? ????, ???? ?????? ???? ??????.";

            texts[4] = "- Good Ending -";
        }

        if (EndingIdx == 1)
        {
            texts = new string[6];
            texts[0] = "?????? ?????? ?????? ???? ??????.";
            texts[1] = "???? ?????? ?????? ?????? ???? ???? ?????? ?? ?????? ??????.";
            texts[2] = "?????? ?????? ???????? ???? ???? ???? ??????, ?????????? ????.";
            texts[3] = "?????? ?? ?????? ?????? ????????, ???? ???????? ???? ???????? ?????? ???? ???? ??????.";

            texts[4] = "???? ???????? ???? ???????????? ?????? ??????????," +
                "?????????? ???? ?????? ????????.";

            texts[5] = "- Normal Ending -";
        }

        if (EndingIdx == 2)
        {
            texts = new string[10];
            texts[0] = "?????? ?????? ?????? ???? ??????.";
            texts[1] = "???? ?????? ?????? ???? ???? ???? ?????? ???? ????????..";

            texts[2] = "???????? ???? ????????????, ?????? ?????? ???? ???? ??????.";
            texts[3] = "?????? ???? ?????? ???? ?? ????,\n ???????? ?????????????? ????.";
            texts[4] = "?????? ???????? ???????? ?????? ???? ??????????.";
            texts[5] = "???? ???????? ???? ???? ?????????? ???????? ?????? ?????? ???? ???? ??????????????.";
            texts[6] = "?????? ???? ?????? ?????? ?????????";
            texts[7] = "???? ?????? ???????? ???? ??????????????...!!";

            texts[8] = "????????.... ?????? ?? ?? ???? ?????? ?? ??????.";
            texts[9] = "- Bad Ending -";
        }

        StartCoroutine(Typing(texts[clickCount]));
        clickCount++;

        clickCount2=1; //clickCount2=1 : ?????? clickCount2=2: doubleclicked clickCount2=0: default
    }

    public void SecretRoom()
    {
        //Debug.Log("dia ????");
        DialogPanel.SetActive(true);

        texts = new string[1];
        texts[0] = "?????? ???? ?????????? ?????? ???? ?????? ?? ????... " +
            "\n?????? ?? ?????? ???? ??????????.";

        if(clickCount2==0)
            StartCoroutine(Typing(texts[0]));
        clickCount2 = 1;
        clickCount++;
        //DialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(texts.Length);
        if (Input.GetMouseButtonDown(0) && clickCount < texts.Length)
        {
            if (clickCount2 == 0)
            {
                StartCoroutine(Typing(texts[clickCount]));
                clickCount++;
                clickCount2=1;
            }
            else
            {
                clickCount2=2;
            }

        }else if(Input.GetMouseButtonDown(0))
        {
            if (playerMove.bossDie)
                gameManager.clear();
            else
            {
                if (clickCount == 1)
                {
                    clickCount2 = 2;
                    clickCount++;
                }
                else
                {
                    clickCount = 0;
                    DialogPanel.SetActive(false);
                }
                    
            }
        }
    }

    IEnumerator Typing(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            if (clickCount2==2)
            {
                EndingText.text = message;
                if (i == message.Length - 1)
                {
                    clickCount2 = 0;
                    //Debug.Log("clickCount2==1");
                }
                yield return null;
            }
            else
            {
                EndingText.text = message.Substring(0, i + 1);
                if (i == message.Length - 1)
                {
                    clickCount2 = 0;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }


    }

}
