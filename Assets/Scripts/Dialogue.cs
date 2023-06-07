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
            texts[0] = "�̷ν� ������ ������ ���� ���ȴ�.";
            texts[1] = "���� ��ɲ� ���̴� ������ ��� ���� ������ �� �����.";
            texts[2] = "���̴� ���� �ְ��� ���� ��ɲ����ν� ���ڰ� �Ǿ� �������� ������ ���� ���� ���� �ູ�� ���ĸ� ������ �Ǿ���.";
            texts[3] = "�׷��� �ѹ� ��ɲ��� ������ ��ɲ�! \n ���̴� ���ο� ������ ��� ����, �ٽ� ������ ���� ���̴�.";

            texts[4] = "- Good Ending -";
        }

        if (EndingIdx == 1)
        {
            texts = new string[6];
            texts[0] = "�̷ν� ������ ������ ���� ���ȴ�.";
            texts[1] = "���� ��ɲ� ���̴� ������ ��� ���� ������ �� ������ ���ߴ�.";
            texts[2] = "�׷��� ���̴� �������� �Ⱦ� ���� ���� ������, �����ϱ�� �ߴ�.";
            texts[3] = "���̴� �� ������ �ͳ��� �����߰�, ���� ��ɲ��� �ƴ� ��ην� �ι�° ���� ��� �Ǿ���.";

            texts[4] = "���� �߳����� ���� ��ɲ��̾��� ���Ű� ����������," +
                "��ην��� � ��̰� �ູ�ϴ�.";

            texts[5] = "- Normal Ending -";
        }

        if (EndingIdx == 2)
        {
            texts = new string[10];
            texts[0] = "�̷ν� ������ ������ ���� ���ȴ�.";
            texts[1] = "���� ���ڸ� ��� ���� ���� �۰� �ʶ��� ���� �ϳ�����..";

            texts[2] = "�׸����� ��¥ �����̾��⿡, ���̴� �ƹ��� ���� ���� ���ߴ�.";
            texts[3] = "�Դٰ� ��¥ ������ �ȷ� �� �˷�,\n �������� �߹���ϱ���� �ߴ�.";
            texts[4] = "���̴� ���ӿ��� �������� �ذ�� ���� �������ߴ�.";
            texts[5] = "�谡 �������� ���� ���� ���������� �ƴ����� �𸣴� ������ �ֿ� �ҿ� �����Ծ���Ѵ�.";
            texts[6] = "������ ���� ��¼�� �̷��� �Ǿ�����?";
            texts[7] = "���� �Ѷ��� �߳����� ���� ��ɲ��̾��µ�...!!";

            texts[8] = "�����ϴ�.... ���踦 �� �� ���� ��ƺ� �� �׷���.";
            texts[9] = "- Bad Ending -";
        }

        StartCoroutine(Typing(texts[clickCount]));
        clickCount++;

        clickCount2=1; //clickCount2=1 : ������ clickCount2=2: doubleclicked clickCount2=0: default
    }

    public void SecretRoom()
    {
        //Debug.Log("dia ȣ��");
        DialogPanel.SetActive(true);

        texts = new string[1];
        texts[0] = "����� �濡 ���⿡ ������ ���� ������ �� ����... " +
            "\n���踦 �� ������ �ٽ� �õ��غ���.";

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
