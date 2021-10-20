using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transcript : MonoBehaviour
{
    public Text transcriptTextUI;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transcriptTextUI.text = DialogeManager.Instance.TranscriptString;
    }
}
