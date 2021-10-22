using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transcript : MonoBehaviour
{
    public Text transcriptTextUI;
    string s ="";
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
    }
}
