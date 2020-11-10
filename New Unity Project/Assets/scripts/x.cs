using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x : MonoBehaviour
{
    //this is hardcoded for now but will change this structure later 
    // maybe xml, or inkl, or fungus...? 
     List<Dialoug> dialougNodes;


    //this is messy - move UI stuff to its own script me! 
    public Text GuicharacterName;
    public Text GuidialougText;
    public Text[] optionText; 


    public void Start()
    {        //for testing purp
        dialougNodes.Add( addingNodeManually( "greeting", "hello there! ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
        dialougNodes.Add(addingNodeManually("oh no", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));
        dialougNodes.Add(addingNodeManually("what", "just some random text ", new float[] { 1, 3, 5, 7, 9, 7, 4, 10 }));

    }

    public void Update()
    {
        GuidialougText.text = dialougNodes[0].GetDialougText();
    }

    public void addDialougNode(Dialoug dNode)
    {
        if (dNode == null)
            return;
        dialougNodes.Add(dNode);
    }


    //ugly!! maybe just pass in a list of floats for each of the values instead two core ones? 
    Dialoug addingNodeManually(string labe, string dText, float[] values)
      {
    
         Dialoug d = new Dialoug(
         Dialoug.DialougType.introduction,
         labe,
         dText,
         addValues(values));//need to add connected nodes --- 
       return d;
 }

    //duplicated method - unify this into value system instead of character and dialoug one 
    private List<KeyValuePair<Dialoug.DialougValue, float>> addValues(float[] values)
    {
        List<KeyValuePair<Dialoug.DialougValue, float>> listofValues =
            new List<KeyValuePair<Dialoug.DialougValue, float>>();

        int i = 0;
        foreach(Dialoug.DialougValue Dvalue in Enum.GetValues(typeof(Dialoug.DialougValue)))
        {
            listofValues.Add((new KeyValuePair<Dialoug.DialougValue, float>
                (Dvalue, values[i])));
               i++;
        }

        return listofValues;
    }

    void runNode(Dialoug node) { 

    }


    void getNextNode()
    {

    }


    //remove the node, go to next node.. 
}
//float[] values = { 10, 0, 0,0,0,0,0,70 };