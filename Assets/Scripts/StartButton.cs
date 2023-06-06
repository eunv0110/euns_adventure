using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour
{
    public void PlayScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
