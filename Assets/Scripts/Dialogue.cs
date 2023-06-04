using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //public string character = "����";
    private string message = "���� ��ɲ� ���̴� ������ ��� ���� ������ �� ����� \n - Good Ending- ";

    public Text EndingText;

    public string[] texts;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Typing(message));
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Typing(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            EndingText.text = message.Substring(0, i + 1);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
