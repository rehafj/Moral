using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDialougExpander : MonoBehaviour
{
    bool isExpanded = false;
    // Start is called before the first frame update
    void Start()
    {
        isExpanded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickedMe()
    {
        Debug.Log("woot! this works!");

        if (!isExpanded) //make the children visble and get the text from the parent node supply it in kids
        {
           
         

        }
        else //hide kids 
        {
        

        }
        isExpanded = !isExpanded;

    }
}
