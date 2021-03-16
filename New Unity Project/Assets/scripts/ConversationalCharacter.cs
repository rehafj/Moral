using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversationalCharacter : MonoBehaviour

  
{
   

    public Dictionary<string, RatingVlaues> ConvCharacterMoralFactors = new Dictionary<string, RatingVlaues>();
    //att 2 --- 
    string[] keys = { "BTrueTYourHeart", "MoneyMaker", "Enviromentalist", "AnimalLover",
    "Teetotasler","FamilyPerson"  ,"SchoolIsCool","LoverOfRisks"   
            ,"SupportingComunities", "LandISWhereThehrtIS","CarrerAboveAll", 
        "FriendsAreTheJoyOFlife"
       , "Loner"};//removed trbd for now 

    public StrictFatherMorality FatherModel ;
    public NurturantParentMorality MotherModel;

    public string ConversationalNpcName;

    [SerializeField] string moralFocus; // will equal one of the keys 

    
    public void setMoralFocusArea(string key)
    {
        moralFocus = key;
    }

    public bool IsMoralFocus(string flag)
    {
        if (moralFocus == flag)
        {
            return true;
        }
        else return false;
    } 

    public enum emotion
    {
        happy, sad, angry, shocked, shy
    }
    public emotion CnpcEmotion;
    public enum RatingVlaues
    {
       High, Mid, Low
    }

    public RatingVlaues NpcValueFlag;
  
    public ConversationalCharacter(string name ,RatingVlaues[] thirteenValues )
    {
        ConversationalNpcName = name;
        int i = 0;
      
        foreach(string s in keys)
        {
            ConvCharacterMoralFactors.Add(s, thirteenValues[i]);
            
            i++;
        }

        FatherModel = new StrictFatherMorality();
    }

    public ConversationalCharacter(string name, RatingVlaues[] thirteenValues, string FocusArea)
    {
        ConversationalNpcName = name;
        int i = 0;

        foreach (string s in keys)
        {
            ConvCharacterMoralFactors.Add(s, thirteenValues[i]);

            i++;
        }

        moralFocus = FocusArea;
        FatherModel = new StrictFatherMorality();

    }


    //randomizing constructor /overloaded
    public ConversationalCharacter(string name)
    {
        List<string> tempHighVlaue = new List<string>();//used to set up moral focus on high value flags


        ConversationalNpcName = name;
        int i = 0;

        foreach (string s in keys)
        {
            int x = Random.Range(0, 3);
            NpcValueFlag = getRandomNPCValue(x);
            ConvCharacterMoralFactors.Add(s, NpcValueFlag);

            if(NpcValueFlag == RatingVlaues.High) //again used for moral focus - will move this into it's opwn method soon 
            {
                tempHighVlaue.Add(s);
            }
            i++;
        }
        int index = UnityEngine.Random.Range(0, tempHighVlaue.Count);
        setMoralFocusArea(tempHighVlaue[index]);
        FatherModel = new StrictFatherMorality();

    }

    private RatingVlaues getRandomNPCValue(int x)
    {
        switch (x)
        {
            case (0):
                return RatingVlaues.Low;
            case (1): return RatingVlaues.Low;
            default:
                return RatingVlaues.High;
        }
    }

    void chooseARandomModel()
    {
        FatherModel = new StrictFatherMorality(); // make it on a precentage but for now I do not have a mother model implemented 
    }
}
/*
 * 
 * 
 * lis{"BTrueTYourHeart", 0 },
        {"MoneyMaker", 0 },
        {"Enviromentalist", 0 },
        {"AnimalLover", 0 },
        {"Teetotasler", 0 },
        {"BeKind", 0 },
        {"SchoolIsCool", 0 },
        {"LoverOfRisks", 0 },
        {"SupportingComunities", 0 },
        {"LandISWhereThehrtIS", 0 },
        {"CarrerAboveAll", 0 },
        {"FriendsAreTheJoyOFlife", 0 },
        {"Loner", 0 }
 * 
 * 
 * 
 * 
 * 
 *   foreach (string s in ConvCharacterMoralFactors.Keys)
        {
            ConvCharacterMoralFactors[s] = thirteenValues[i];
            Debug.Log("???+ " + ConvCharacterMoralFactors[s]);
            i++;
        }
 * 
 * 
 * 
 * 
 * ConvCharacterMoralFactors.Add("BTrueTYourHeart", 0);
ConvCharacterMoralFactors.Add("MoneyMaker", 0);
ConvCharacterMoralFactors.Add("Enviromentalist", 0);
ConvCharacterMoralFactors.Add("AnimalLover", 0);
ConvCharacterMoralFactors.Add("Teetotasler", 0);
ConvCharacterMoralFactors.Add("BeKind", 0);
ConvCharacterMoralFactors.Add("SchoolIsCool", 0);
ConvCharacterMoralFactors.Add("LoverOfRisks", 0);
ConvCharacterMoralFactors.Add("SupportingComunities", 0);
ConvCharacterMoralFactors.Add("LandISWhereThehrtIS", 0);
ConvCharacterMoralFactors.Add("CarrerAboveAll", 0);
ConvCharacterMoralFactors.Add("FriendsAreTheJoyOFlife", 0);
ConvCharacterMoralFactors.Add("Loner", 0);*/