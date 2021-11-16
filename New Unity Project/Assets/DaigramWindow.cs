using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DaigramWindow : MonoBehaviour
{

    public GameObject CNPCUIResponceBoxPrefab;
    public RectTransform startPosition;
    public RectTransform currentPosition;
    public GameObject parentContent;
    float yPostion = -120;
    float xPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = startPosition;
        CNPCUIResponceBoxPrefab = Resources.Load<GameObject>("UIPrefabs/cnpcresponcediagrambox");
        GameObject temp = Instantiate(CNPCUIResponceBoxPrefab,  new Vector2(0,0), Quaternion.identity);
        temp.gameObject.transform.SetParent(parentContent.transform, false);
        Invoke("disableCanvas", 0.01f);
    /*    CreateAnotherBox(0, yPostion);
        CreateAnotherBox(0, yPostion);*/



    }

    void disableCanvas()
    {
        gameObject.SetActive(false);
    }
    public void CreateSingleBox(float x, float y, Dialoug node, string text)
    {

        GameObject temp = Instantiate(CNPCUIResponceBoxPrefab, new Vector2(x, yPostion) , Quaternion.identity);
        temp.gameObject.transform.SetParent(parentContent.transform, false);
        temp.gameObject.GetComponentInChildren<Text>().text = text;
        temp.gameObject.GetComponentInChildren<Text>().color = Color.black;


        yPostion += -130;
    }
    public void CreateMultipleBoxes( List<Dialoug> nodes, string text, string Selectedpattern)
    {
        //currentPosition.transform.position = new Vector2 (currentPosition.rect.x,  currentPosition.rect.y - 180);currentPosition.rect.position
        foreach(Dialoug d in nodes)
        {
            GameObject temp = Instantiate(CNPCUIResponceBoxPrefab, new Vector2(xPosition, yPostion), Quaternion.identity);
            temp.gameObject.transform.SetParent(parentContent.transform, false);
            temp.gameObject.GetComponentInChildren<Text>().text = d.mainOpinionOnAtopic;
            temp.gameObject.GetComponentInChildren<Text>().color = Color.grey;
            temp.gameObject.GetComponentInChildren<Image>().color = Color.grey;
            xPosition += 240;

            if (d.Pattern == Selectedpattern)
            {
                temp.gameObject.GetComponentInChildren<Text>().color = Color.black;
                temp.gameObject.GetComponentInChildren<Image>().color = Color.white;
                yPostion -= 130;
            }
        }
      
        
    }
    public void CreateMultipleBoxes(string[] text, string sentintext, bool isplayer)
    {
        //currentPosition.transform.position = new Vector2 (currentPosition.rect.x,  currentPosition.rect.y - 180);currentPosition.rect.position
        foreach (string s in text)
        {
            GameObject temp = Instantiate(CNPCUIResponceBoxPrefab, new Vector2(xPosition, yPostion), Quaternion.identity);
            temp.gameObject.transform.SetParent(parentContent.transform, false);
            temp.gameObject.GetComponentInChildren<Text>().text = s;
            temp.gameObject.GetComponentInChildren<Text>().color = Color.black;
            temp.gameObject.GetComponentInChildren<Image>().color = Color.grey;
            xPosition += 240;

            if(s== sentintext && isplayer)
            {
                temp.gameObject.GetComponentInChildren<Text>().color = Color.white;
                temp.gameObject.GetComponentInChildren<Image>().color = Color.blue;
                yPostion -= 130;
            } else if (s == sentintext && !isplayer)
            {
                temp.gameObject.GetComponentInChildren<Text>().color = Color.black;
                temp.gameObject.GetComponentInChildren<Image>().color = Color.white;
                yPostion -= 130;
            }
        }


    }

    private Vector3 updatePosition()
    {
        
        throw new NotImplementedException();
    }

    private void pivotObject()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
