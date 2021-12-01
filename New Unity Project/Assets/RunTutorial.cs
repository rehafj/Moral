using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTutorial : MonoBehaviour
{
    public GameObject InnerMaskObject;
    public GameObject OuterMaskObject;


    bool isOn = false;


    void OnMouseDown()
    {
        if (!isOn)
        {
           
            InnerMaskObject.gameObject.SetActive(true); //start work on inner mask object / ..i.e. start the tutorial 
            //ther inner view of world should start here! 
            OuterMaskObject.gameObject.SetActive(false); //start work on inner mask object / ..i.e. start the tutorial 

            isOn = true;

        }
        else
        {
            OuterMaskObject.gameObject.SetActive(false);
            isOn = false;

        }
    }
}
