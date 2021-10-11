using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[Serializable]
[System.Serializable]

public class Character : MonoBehaviour
{
    public string name;
    public List<Dialoug> savedConversations;

    public enum emotionalState
    {
        angry, happy, sad, shy, normal
    }
    public emotionalState currentSate;

 
 
    
}



 /*
public string name;
    public List<Dialoug> savedConversations;

    public enum personalValue //perhaps make this have values instead of a dictionary later... 
    {
        Aultirusum, Cander, Coop, Lawful, Loyal, Trustworthy, Boldness, Calm, 
    }

    public enum emotionalState
    {
        angry, happy, sad, shy, normal
    }
    public emotionalState currentSate;

    //consider removing the dictionary me... and changing above to int values 
    public Dictionary<personalValue, float> characterValues = 
        new Dictionary<personalValue, float>();

    //a dictionary of characters this character has a relationship with. 
    public Dictionary<Character, float> thisCharacterRelationship =
       new Dictionary<Character, float>();


    public Character(string _name, float[] values) { 

        if (values.Length < Enum.GetValues(typeof(personalValue)).Length)
        {
            Debug.LogError("you missed a few core values there...");
        }
       initValueDict(values); 
       this.name = _name;
       currentSate = emotionalState.normal;


    }

    private void initValueDict(float[] values)
    {
        int i = 0;

        foreach (personalValue pvalue in Enum.GetValues(typeof(personalValue)))
        {

            characterValues.Add(pvalue, values[i]);
            i++;
        }
    }



    public void printCurrentValues()
    {
        foreach(KeyValuePair<personalValue, float> value in characterValues)
        {
            Debug.Log("the personal value of " + name + ":"+ value.Key + ": " + value.Value);
        }
    }

    //not the cleanest bit of code, but for now...
    void updateValues(personalValue valueName, float Value, bool isIncreased)
    {
        if (isIncreased)
        {
            characterValues[valueName] = characterValues[valueName] + Value;

        }
  
    }
    
    void makeAJudgmentCall(Dialoug dialoug, Character character)//thiscalSystem system
    {
        //refrence to a system of kantian, utilitarain...etc 
        //based on history that comes in - 
    }

    void updateDialougHistory(Dialoug d)
    {
        savedConversations.Add(d);
    }

    void updateRelationshipWithACharacter(Character character, float value)
    {
        thisCharacterRelationship[character] += value; 
    }
*/