using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraController : MonoBehaviour
{

    public GameObject OutwerWorldCam;
    public GameObject InnerWorldCam;
    [SerializeField]
    public static bool canclickOnOuterWorld;
    [SerializeField]
    bool isInnerWorld = false;
    public Canvas innerWorldCanvas;


    public GameObject LeftPanel;
    public GameObject RightPanel;

    public GameObject leftLookAt;
    public GameObject rightLookAt;




    Quaternion leftQuaternion = new Quaternion(0, 170.0f, 0, 0);
    Quaternion cameraQuanterionOrigin; 
    // Start is called before the first frame update
    void Start()
    {
        isInnerWorld = false; //we start with outerworld
        cameraQuanterionOrigin = InnerWorldCam.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchCameras()
    {
        isInnerWorld = !isInnerWorld;
        if (!isInnerWorld) //inside outerworld
        {
            OutwerWorldCam.gameObject.SetActive(true);
            InnerWorldCam.gameObject.SetActive(false);
            canclickOnOuterWorld = true;
            InnerWorldCam.transform.rotation = cameraQuanterionOrigin;
               makeInnerMenuVisble();
            LeftPanel.SetActive(false);
            RightPanel.SetActive(false);
        } else
        {
            OutwerWorldCam.gameObject.SetActive(false);
            InnerWorldCam.gameObject.SetActive(true);
            canclickOnOuterWorld = false;
            makeInnerMenuVisble();
        }


    }

    private void makeInnerMenuVisble()
    {

        if (isInnerWorld)
        {
            innerWorldCanvas.gameObject.SetActive(true);

        } else
        {
            innerWorldCanvas.gameObject.SetActive(false);

        }
    }

    public void LookLeft()
    {
        InnerWorldCam.GetComponent<Animator>().Play("lookLeftCam");
        LeftPanel.SetActive(true);
        RightPanel.SetActive(false);

    }

    public void LookRight()
    {
        InnerWorldCam.GetComponent<Animator>().Play("LOOKRIGHTCAM");

        InnerWorldCam.transform.LookAt(rightLookAt.transform);
        LeftPanel.SetActive(false);
        RightPanel.SetActive(true);
    }

    /* IEnumerator animateCameraLeft(float t)
     {
       *//*  float n = 0;
         while(n < leftQuaternion.y)
         {
             n += 15;
             Quaternion q = new Quaternion(0, n, 0, 0); //
             InnerWorldCam.transform.rotation =
                         Quaternion.Slerp(InnerWorldCam.transform.rotation, q, 1f * Time.deltaTime);
             yield return null;

         }*//*

     }*/

    /*  IEnumerator animateCameraRight(float t)
      {


      }*/


}
