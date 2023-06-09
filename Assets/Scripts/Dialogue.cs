using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{

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

        if(SceneManager.GetActiveScene().name == "BadEnding")
            EndingIdx = 2;
        else if (SceneManager.GetActiveScene().name == "NormalEnding")
            EndingIdx = 1;
        else
            EndingIdx = 0;

        Endings[EndingIdx].SetActive(true);

    

        if (EndingIdx == 0)
        {
            texts = new string[5];
            texts[0] = "은이는 세상의 모든 보물을 모아 부자가 되었다.";
            texts[1] = "그래서 오랫동안 꿈꿔왔던 궁전을 세워 공주 놀이를 하였다.";
            texts[2] = "그러나 은이는 또 다른 보물이 생기길 기다리고 있는 중이다 트레저 헌터의 열정은 아직 사라지지 않았나보다.";
            texts[3] = "또 다른 보물이 나타나면 ... 아마 은이 또 긴 여정을 떠날 것이다.....\n (그러면 은이야 보물 나 조금만 나눠주고 가 )";

            texts[4] = "- Good Ending -";
        }

        if (EndingIdx == 1)
        {
            texts = new string[6];
            texts[0] = "은이는 세상의 모든 보물을 다 모으지는 못했지만 이 정도로 만족하기로한다..";
            texts[1] = "은이는 보물사냥꾼으로써의 삶을 끝내고, 농부로써의 두번째 삶을 시작했다.";
            texts[2] = "몬스터와 싸우고 보물을 얻을 때 만큼의 스릴은 없지만, 은이는 노동의 가치와 수확의 기쁨을 알게 되었다.";
            texts[3] = "은이는 매일 열심히 밀과 체리, 사과를 키우고 수확한다.";

            texts[4] = "은이는 농부로써의 두번째 삶에 만족한다\n" +
                "은이의 집에 놀러가면, 보물보다 귀한 농산물을 얻을 수 있을지도....";

            texts[5] = "- Normal Ending -";
        }

        if (EndingIdx == 2)
        {
            texts = new string[12];
            texts[0] = "은이는 작고 초라한 보석 하나밖에 얻지 못하였다.";
            texts[1] = "그마저도 가짜 보석이었기에 은이는 돈을 벌지 못했다...";

            texts[2] = "게다가 가짜 보석을 팔려한 죄로, 마을에서 추방당하기까지 하였다.";
            texts[3] = "은이는 춥고 배고프게 숲 속을 떠돌다, 지쳐 쓰러졌다.\n 눈물 때문에 눈 앞에 흐려져 하늘이 잘 보이지 않았다....";
            texts[4] = "은이는 배가 고파 독이 있을지도, 없을지도 모르는 독버섯을 불에 구워먹어야했다.";
            texts[5] = "춥고 배도 고프지만 잘 곳이 없어 정체를 알 수 없는 해골바가지와 함께 잠을 자야한다.";
            texts[6] = "은이의 인생은 어쩌다 이렇게 되었을까";
            texts[7] = "나도 한 때는 잘나가는 보물 사냥꾼이었는데...!!";

            texts[8] = "비통하다.... 열쇠를 조금 더 모아볼 걸 그랬다.";
            texts[9] = "...........";
            texts[10] = "모험을 다시 떠나볼까?.";
            texts[11] = "- Bad Ending -";
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
        texts[0] = "비밀의 방에 들어가기에 열쇠의 개수가 부족한 것 같다... " +
            "\n열쇠를 더 모으고 다시 도전해보자.";

        if (clickCount2==0)
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
