using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //public string character = "은이";
    //private string message = "보물 사냥꾼 은이는 세상의 모든 귀한 보물을 다 얻었다 \n - Good Ending- ";

    public Text EndingText;
    public int EndingIdx;
    private string[] texts;
    private int clickCount;
    private int clickCount2 = 0;
    public GameManager gameManager;
    public GameObject[] Endings;
    public GameObject DialogPanel;
    // Start is called before the first frame update

    void Start()
    {

        if (gameManager.stageIndex >= gameManager.Stages.Length - 1)
            PlayEnding();

    }
    private void PlayEnding()
    {

        if (gameManager.totalKey < 3)
           EndingIdx = 2;
        else if (gameManager.totalKey < 10)
           EndingIdx = 1;
        else
           EndingIdx = 0;

        Endings[EndingIdx].SetActive(true);



        if (EndingIdx == 0)
        {
            texts = new string[5];
            texts[0] = "이로써 은이의 모험이 막을 내렸다.";
            texts[1] = "보물 사냥꾼 은이는 세상의 모든 귀한 보물을 다 얻었다.";
            texts[2] = "은이는 세계 최고의 보물 사냥꾼으로써 부자가 되어 궁전같은 집에서 돈을 펑펑 쓰며 행복한 노후를 보내게 되었다.";
            texts[3] = "그러나 한번 사냥꾼은 영원한 사냥꾼! \n 은이는 새로운 보물을 얻기 위해, 다시 여행을 떠날 것이다.";

            texts[4] = "- Good Ending -";
        }

        if (EndingIdx == 1)
        {
            texts = new string[6];
            texts[0] = "이로써 은이의 모험이 막을 내렸다.";
            texts[1] = "보물 사냥꾼 은이는 세상의 모든 귀한 보물을 다 얻지는 못했다.";
            texts[2] = "그러나 은이는 보물들을 팔아 돈을 많이 벌었고, 만족하기로 했다.";
            texts[3] = "은이는 그 돈으로 귀농을 시작했고, 보물 사냥꾼이 아닌 농부로써 두번째 삶을 살게 되었다.";

            texts[4] = "가끔 잘나가는 보물 사냥꾼이었던 과거가 생각났지만," +
                "농부로써의 삶도 즐겁고 행복하다.";

            texts[5] = "- Normal Ending -";
        }

        if (EndingIdx == 2)
        {
            texts = new string[10];
            texts[0] = "이로써 은이의 모험이 막을 내렸다.";
            texts[1] = "보물 상자를 열어서 얻은 것은 작고 초라한 보석 하나였다..";

            texts[2] = "그마저도 가짜 보석이었기에, 은이는 아무런 돈도 벌지 못했다.";
            texts[3] = "게다가 가짜 보석을 팔려 한 죄로,\n 마을에서 추방당하기까지 했다.";
            texts[4] = "은이는 숲속에서 누군가의 해골과 밤을 보내야했다.";
            texts[5] = "배가 고프지만 돈이 없어 독버섯인지 아닌지도 모르는 버섯을 주워 불에 구워먹어야한다.";
            texts[6] = "은이의 삶은 어쩌다 이렇게 되었을까?";
            texts[7] = "나도 한때는 잘나가는 보물 사냥꾼이었는데...!!";

            texts[8] = "비통하다.... 열쇠를 좀 더 많이 모아볼 걸 그랬다.";
            texts[9] = "- Bad Ending -";
        }

        StartCoroutine(Typing(texts[clickCount]));
        clickCount++;

        clickCount2=1; //clickCount2=1 : 실행중 clickCount2=2: doubleclicked clickCount2=0: default
    }

    public void SecretRoom()
    {
        //Debug.Log("dia 호출");
        DialogPanel.SetActive(true);

        texts = new string[1];
        texts[0] = "비밀의 방에 들어가기에 열쇠의 수가 부족한 것 같다... " +
            "\n열쇠를 더 모으고 다시 시도해보자.";

        if(clickCount2==0)
            StartCoroutine(Typing(texts[0]));
        clickCount2 = 1;
        clickCount++;
        //DialogPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
            if (gameManager.stageIndex >= gameManager.Stages.Length - 1)
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
