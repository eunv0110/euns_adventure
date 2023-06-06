using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseButton : MonoBehaviour
{
    public GameObject ExplainImage;
    public GameObject Close_Button;


    public void ExplainImageOff()
    {
        ExplainImage.SetActive(false) ;
        Close_Button.SetActive(false);

    }
}
