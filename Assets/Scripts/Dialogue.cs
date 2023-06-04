using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //public string character = "은이";
    private string message = "보물 사냥꾼 은이는 세상의 모든 귀한 보물을 다 얻었다 \n - Good Ending- ";

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
