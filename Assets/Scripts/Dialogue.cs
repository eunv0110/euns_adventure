using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //public string character = "����";
    //private string message = "���� ��ɲ� ���̴� ������ ��� ���� ������ �� ����� \n - Good Ending- ";

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
            texts[0] = "�̷ν� ������ ������ ���� ���ȴ�.";
            texts[1] = "���� ��ɲ� ���̴� ������ ��� ���� ������ �� �����.";
            texts[2] = "- Good Ending -";
        }

        if (EndingIdx == 1)
        {
            texts[0] = "�̷ν� ������ ������ ���� ���ȴ�.";
            texts[1] = "���� ��ɲ� ���̴� ������ ��� ���� ������ �� ������ ���ߴ�.";
            texts[2] = "�׷��� ���̴� �������� �Ⱦ� ���� ���� ������, �����ϱ�� �ߴ�.";
            texts[3] = "���̴� �� ������ �ͳ��� �����߰�, ���� ��ɲ��� �ƴ� ��ην� �ι�° ���� ��� �Ǿ���.";
            texts[4] = "- Normal Ending -";
        }

        if (EndingIdx == 2)
        {
            texts[0] = "�̷ν� ������ ������ ���� ���ȴ�.";
            texts[1] = "���� ���ڸ� ��� ���� ���� �۰� �ʶ��� ���� �ϳ�����..";
            texts[2] = "�����ϴ�.... ���踦 �� �� ���� ��ƺ� �� �׷���.";
            texts[3] = "- Bad Ending -";
        }

        StartCoroutine(Typing(texts[clickCount]));
        clickCount++;

        clickCount2=1; //clickCount2=1 : ������ clickCount2=2: doubleclicked clickCount2=0: default
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickCount < texts.Length)
        {
            //Debug.Log("Ŭ��");
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
