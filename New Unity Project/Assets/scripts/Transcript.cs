using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Transcript : MonoBehaviour
{
    public Text transcriptTextUI;
    string s ="";
    public Text logForBuffer;
    // Start is called before the first frame update
    void Awake()
    {
       /* for(int i =0; i< 200; i++)
        {
            s += "line "  + i + "\n";
        }*/
    }

    // Update is called once per frame
    void Update()
    {
       
        //add bool on value change or make this the instance we change / save to file maybe?
        transcriptTextUI.text = DialogeManager.Instance.TranscriptString;
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("coppying to buffer");
            EditorGUIUtility.systemCopyBuffer = logForBuffer.text;
        }
    }




}

