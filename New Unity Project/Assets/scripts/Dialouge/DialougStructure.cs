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
    public string surfaceOpinionOnTopic { get; set; }
    public string agreementText { get; set; }
    public string disagreementtext  { get; set; } // contains the second body of a parg/counter point ( if two moral factors are high for a character ) uses this as a countr argument 

    public string moralFocusAgreement { get; set; }

    public List<string>  moralFocusDisagreement { get; set; }


}



