﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Dialoug   //restructure this badly....
{
    public string ButtonText; //set and used for the patern now 
    //change this so have accses to both on each node... 
    public string UnbiasedOpeningStatment;


    public string mainOpinionOnAtopic; //main thoughts ( body1 in json) --- agh old code 


    //BiasedSVOpin //PlayerAgreesWithCNPC -- AGREEMENT TEXT  //PlayerDisAgreesWithCNPC

    public string agreementText; //i.e doubling down on it. 
    public string disagreementText;
    public string moralGreementText;
    public List<string> moralDisagreementText = new List<string>();
    public string Rating;


    public List<string> hatedFacts; //will contain things the npc hates about the character 
    public string thoughtBubbleText;
    public bool Explored; //just to test something
    public  List<string> ExploredHatedFacts;
    
    public Dialoug parent;
    public List<Dialoug> children;

    public List<string> factsOnTopic = new List<string>();

    public Dialoug(string thoughtBubbletext, string initialDialougText)
    {
        thoughtBubbleText = thoughtBubbletext;
        mainOpinionOnAtopic = initialDialougText;
        Explored = false;
        factsOnTopic.Add("will randomly presrnt  a fact for the key" + thoughtBubbleText);
    }

    public string GetAFact()
    {
        return factsOnTopic[0];
    }
    public int getHeight()
    {
        int height = 1;
        Dialoug currentNode = this;
        while (currentNode.parent != null)
        {
            height++;
            currentNode = currentNode.parent;
        }
        return height;
    }
    public string getRandomHatedFact() //pilinng on 
    {
        int r = Random.Range(0, hatedFacts.Count);     
        string hatedFact = hatedFacts[r];
        hatedFacts.RemoveAt(r);
        //ExploredHatedFacts.Add(hatedFact);
        return hatedFact;
        //TODO add something to check if all hated facts are done :d //size = 0S
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