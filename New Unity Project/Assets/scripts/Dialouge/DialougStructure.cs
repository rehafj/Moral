using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialougStructure 
{

    public string topic { get; set; }
    public NarrativeElements NarrativeElements { get; set; }
    


}


public class NarrativeElements
{
    public string intro { get; set; }
    public string bodyOne { get; set; }
    public string bodytwo { get; set; } // contains the second body of a parg/counter point ( if two moral factors are high for a character ) uses this as a countr argument 
}



