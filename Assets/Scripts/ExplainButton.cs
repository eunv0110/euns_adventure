using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExplainButton : MonoBehaviour
{
    public GameObject ExplainImage;
    public GameObject CloseButton;


    public void ExplainImageOn()
    {
        ExplainImage.SetActive(true);
        CloseButton.SetActive(true);

    }
}
