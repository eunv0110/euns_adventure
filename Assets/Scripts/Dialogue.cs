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
    public string[] texts;
    private int clickCount;
    private int clickCount2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (EndingIdx == 0)
        {
            texts[0] = "이로써 은이의 모험이 막을 내렸다.";
            texts[1] = "보물 사냥꾼 은이는 세상의 모든 귀한 보물을 다 얻었다.";
            texts[2] = "- Good Ending -";
        }

        if (EndingIdx == 1)
        {
            texts[0] = "이로써 은이의 모험이 막을 내렸다.";
            texts[1] = "보물 사냥꾼 은이는 세상의 모든 귀한 보물을 다 얻지는 못했다.";
            texts[2] = "그러나 은이는 보물들을 팔아 돈을 많이 벌었고, 만족하기로 했다.";
            texts[3] = "은이는 그 돈으로 귀농을 시작했고, 보물 사냥꾼이 아닌 농부로써 두번째 삶을 살게 되었다.";
            texts[4] = "- Normal Ending -";
        }

        if (EndingIdx == 2)
        {
            texts[0] = "이로써 은이의 모험이 막을 내렸다.";
            texts[1] = "보물 상자를 열어서 얻은 것은 작고 초라한 보석 하나였다..";
            texts[2] = "비통하다.... 열쇠를 좀 더 많이 모아볼 걸 그랬다.";
            texts[3] = "- Bad Ending -";
        }

        StartCoroutine(Typing(texts[clickCount]));
        clickCount++;

        clickCount2=1; //clickCount2=1 : 실행중 clickCount2=2: doubleclicked clickCount2=0: default
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickCount < texts.Length)
        {
            //Debug.Log("클릭");
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
                    //Debug.Log("else");
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
