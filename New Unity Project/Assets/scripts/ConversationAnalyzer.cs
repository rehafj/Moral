﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationAnalyzer : MonoBehaviour
{
    public static ConversationAnalyzer Instance;

    public Button AnalyzerButton;

    public List<GameObject> listOfUIPrefabs;

    public enum TypeOfConversation
    {
        rumor, CNPC_SV_statment, player_agreement, player_disagreement, CNPC_MM_argument
    }


    void clickedOnRumor(Dialoug node)
    {
        listOfUIPrefabs[0].GetComponent<Text>().text = node.UnbiasedOpeningStatment;
        //add methiod on click of the object itself spawns two objects underneath it 
        //listOfUIPrefabs[0].GetComponentInChildren<Text>().text.Length.ToString... along these lines and show 
        //potential mapping 
        //it seems - currentcnpc.name - made them think of --- sv 

    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }

// Update is called once per frame
void Update()
    {
        
    }


    public void clickedOnAnalze()
    {

    }
}
