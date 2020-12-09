using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Dialoug 
{
   public string ButtonText;
    public string dialougText;


    public Dialoug(string option, string text)
    {
        ButtonText = option;
        dialougText = text;
    }
}



/* OLD CODE: 
 * 
 * 
 *  public enum DialougType
    {
        Dilemma, introduction, informer, influintial, OneLiner
    }

    public enum DialougValue
    {
        Aultirusum, Cander, Coop, Lawful, Loyal, Trustworthy, Boldness, Calm
    } //this is shared between a character and apierce of dialoug - move it to its own place, perhaps value system 

    int ID;
    string option;// is the option that is displayed 
    string dialougText; // is the actual dialouge 
    DialougType thisDialougType;
    List<Dialoug> ConnectedResponces; //maybe do this by id instead, hold list of ints 
    List<KeyValuePair<DialougValue, float>> listOfValues;

    public Dialoug(DialougType dialougType, string displayOption,string dialougText,
        List<KeyValuePair<DialougValue, float>> listOfValues)
    {
        this.thisDialougType = dialougType;
        this.option = displayOption;
        this.dialougText = dialougText;
        this.listOfValues = listOfValues;
        ConnectedResponces = new List<Dialoug>();
    }

  
    public string GetDialougText()
    {
        return dialougText;
    }

    public void setConnectedNodes(Dialoug d1, Dialoug d2) //manual for now 
    {
        //manual for now, loop and add for whatever connected nodes there are set in constructer via id
        this.ConnectedResponces.Add(d1);
        this.ConnectedResponces.Add(d2);

    }

    public string getNodeOptiontext(int index)
    {
        return this.ConnectedResponces[index].option;
    }

    public void  addConnectedNodes(List<Dialoug> nodes)
    {

    }

    public void respond(Character character) {
    }

 */